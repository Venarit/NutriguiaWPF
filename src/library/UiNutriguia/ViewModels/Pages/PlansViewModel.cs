using Wpf.Ui.Controls;
using Wpf.Ui;
using Nutriguia.Model.DataAccess;
using UiNutriguia.Models;
using UiNutriguia.Views.Pages;
using System.Diagnostics;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.Reflection.Metadata;
using static System.Runtime.InteropServices.JavaScript.JSType;
using PdfSharp.UniversalAccessibility.Drawing;
using Microsoft.VisualBasic;
using PdfSharp.Drawing.Layout;

namespace UiNutriguia.ViewModels.Pages;

public partial class PlansViewModel(INavigationService navigationService) : ObservableObject, INavigationAware
{
    private bool _isInitialized = false;
    private DataAccess dataAccess;

    [ObservableProperty] private Visibility _gridVisibility = Visibility.Hidden;
    [ObservableProperty] private Visibility _messageVisibility = Visibility.Hidden;
    [ObservableProperty] private Visibility _chooseMessageVisibility = Visibility.Visible;
    [ObservableProperty] private List<PatientModel> _patients;
    
    private PlanPatientModel _planPatient;
    public PlanPatientModel PlanPatient
    {
        get => _planPatient;
        set
        {
            SetProperty(ref _planPatient, value);
            if (value != null)
            {
                GridVisibility = Visibility.Visible;
                MessageVisibility = Visibility.Hidden;
            }
        }
    }

    private PatientModel _selectedPatient;
    public PatientModel SelectedPatient
    {
        get => _selectedPatient;
        set
        {
            SetProperty(ref _selectedPatient, value);
            GetNutritionalPlan();
        }
    }
    private string _filterText;
    public string FilterText
    {
        get => _filterText;
        set
        {
            SetProperty(ref _filterText, value);
            ApplyFilter();
        }
    }

    [RelayCommand]
    private void GotoPage(Type type)
    {
        _ = navigationService.Navigate(type);
    }

    [RelayCommand]
    private void GotoAddPlanOption()
    {
        if (PlanPatient != null && SelectedPatient != null)
        {
            if (PlanPatient.PlanModel.PlanOptionModel1.Active == null || PlanPatient.PlanModel.PlanOptionModel2.Active == null
                || PlanPatient.PlanModel.PlanOptionModel3.Active == null)
            {
                NavigationContext.Parameter = SelectedPatient;
                NavigationContext.Parameter2 = PlanPatient.IdPlan;
                _ = navigationService.Navigate(typeof(PlansAddPage));
            }
        }
    }

    [RelayCommand]
    private void GetPdf()
    {
        if (PlanPatient.PlanModel.PlanOptionModel1.Active != null || PlanPatient.PlanModel.PlanOptionModel2.Active != null
            || PlanPatient.PlanModel.PlanOptionModel3.Active != null)
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter tf = new XTextFormatter(gfx);

            DateTime today = DateTime.Now;

            #region Fonts
            XFont titleFont = new XFont("Verdana", 8, XFontStyleEx.Bold);
            XFont contentFont = new XFont("Verdana", 7);
            XFont ingFont = new XFont("Verdana", 6);
            XFont headerFont = new XFont("Verdana", 10, XFontStyleEx.Bold);
            XFont planFont = new XFont("Verdana", 6, XFontStyleEx.Bold);
            #endregion

            XPen pen = new XPen(XColors.LightGray, 0.05);

            XImage image = XImage.FromFile("../../../Assets/nutriguia.png");

            gfx.DrawImage(image, 505, 10, 70, 65);

            gfx.DrawString("Fecha", titleFont, XBrushes.Gray, new XRect(10, 10, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString(today.ToString("yyyy/MM/dd"), contentFont, XBrushes.Gray, new XRect(10, 20, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString("Nombre Paciente", titleFont, XBrushes.Gray, new XRect(10, 30, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString(SelectedPatient.FullName, contentFont, XBrushes.Gray, new XRect(10, 40, page.Width, page.Height), XStringFormats.TopLeft);

            gfx.DrawString("Plan Nutricional", headerFont, XBrushes.Gray, new XRect(10, 65, page.Width, page.Height), XStringFormats.TopLeft);

            #region Column 0
            gfx.DrawRoundedRectangle(XBrushes.LightGoldenrodYellow, 10, 85, 50, 180, 5, 5);
            gfx.DrawString("Desayuno", planFont, XBrushes.Gray, new XRect(16, 170, page.Width, page.Height), XStringFormats.TopLeft);

            gfx.DrawRoundedRectangle(XBrushes.Bisque, 10, 275, 50, 70, 5, 5);
            gfx.DrawString("Colación", planFont, XBrushes.Gray, new XRect(16, 305, page.Width, page.Height), XStringFormats.TopLeft);

            gfx.DrawRoundedRectangle(XBrushes.LightCyan, 10, 355, 50, 180, 5, 5);
            gfx.DrawString("Comida", planFont, XBrushes.Gray, new XRect(16, 440, page.Width, page.Height), XStringFormats.TopLeft);

            gfx.DrawRoundedRectangle(XBrushes.LavenderBlush, 10, 545, 50, 70, 5, 5);
            gfx.DrawString("Colación", planFont, XBrushes.Gray, new XRect(16, 575, page.Width, page.Height), XStringFormats.TopLeft);

            gfx.DrawRoundedRectangle(XBrushes.MistyRose, 10, 625, 50, 180, 5, 5);
            gfx.DrawString("Cena", planFont, XBrushes.Gray, new XRect(16, 710, page.Width, page.Height), XStringFormats.TopLeft);
            #endregion

            #region Breakfast
            gfx.DrawString("Opción 1", titleFont, XBrushes.Gray, new XRect(70, 90, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawRoundedRectangle(pen, 70, 105, 150, 160, 5, 5);
            XRect textArea = new XRect(80, 115, 140, 150);

            string text = $"{PlanPatient.PlanModel.PlanOptionModel1.BreakfastModel.Dish.Name}:\n\n";
            foreach (var ingredient in PlanPatient.PlanModel.PlanOptionModel1.BreakfastModel.Dish.DishFoodModel)
            {
                text += $"- {ingredient.Quantity} {ingredient.Food.Unit.Name} {ingredient.Food.Name}\n\n";
            }
            tf.DrawString(text, ingFont, XBrushes.Gray, textArea, XStringFormats.TopLeft);


            gfx.DrawString("Opción 2", titleFont, XBrushes.Gray, new XRect(245, 90, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawRoundedRectangle(pen, 245, 105, 150, 160, 5, 5);
            textArea = new XRect(255, 115, 140, 150);

            text = $"{PlanPatient.PlanModel.PlanOptionModel2.BreakfastModel.Dish.Name}:\n\n";
            foreach (var ingredient in PlanPatient.PlanModel.PlanOptionModel2.BreakfastModel.Dish.DishFoodModel)
            {
                text += $"- {ingredient.Quantity} {ingredient.Food.Unit.Name} {ingredient.Food.Name}\n\n";
            }
            tf.DrawString(text, ingFont, XBrushes.Gray, textArea, XStringFormats.TopLeft);


            gfx.DrawString("Opción 3", titleFont, XBrushes.Gray, new XRect(420, 90, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawRoundedRectangle(pen, 420, 105, 150, 160, 5, 5);
            textArea = new XRect(430, 115, 140, 150);

            text = $"{PlanPatient.PlanModel.PlanOptionModel3.BreakfastModel.Dish.Name}:\n\n";
            foreach (var ingredient in PlanPatient.PlanModel.PlanOptionModel3.BreakfastModel.Dish.DishFoodModel)
            {
                text += $"- {ingredient.Quantity} {ingredient.Food.Unit.Name} {ingredient.Food.Name}\n\n";
            }
            tf.DrawString(text, ingFont, XBrushes.Gray, textArea, XStringFormats.TopLeft);
            #endregion

            #region Collation1
            gfx.DrawRoundedRectangle(pen, 70, 275, 150, 70, 5, 5);
            textArea = new XRect(80, 285, 140, 60);

            text = $"{PlanPatient.PlanModel.PlanOptionModel1.Collation1Model.Dish.Name}:\n\n";
            foreach (var ingredient in PlanPatient.PlanModel.PlanOptionModel1.Collation1Model.Dish.DishFoodModel)
            {
                text += $"- {ingredient.Quantity} {ingredient.Food.Unit.Name} {ingredient.Food.Name}\n\n";
            }
            tf.DrawString(text, ingFont, XBrushes.Gray, textArea, XStringFormats.TopLeft);


            gfx.DrawRoundedRectangle(pen, 245, 275, 150, 70, 5, 5);
            textArea = new XRect(255, 285, 140, 60);

            text = $"{PlanPatient.PlanModel.PlanOptionModel2.Collation1Model.Dish.Name}:\n\n";
            foreach (var ingredient in PlanPatient.PlanModel.PlanOptionModel2.Collation1Model.Dish.DishFoodModel)
            {
                text += $"- {ingredient.Quantity} {ingredient.Food.Unit.Name} {ingredient.Food.Name}\n\n";
            }
            tf.DrawString(text, ingFont, XBrushes.Gray, textArea, XStringFormats.TopLeft);


            gfx.DrawRoundedRectangle(pen, 420, 275, 150, 70, 5, 5);
            textArea = new XRect(430, 285, 140, 60);

            text = $"{PlanPatient.PlanModel.PlanOptionModel3.Collation1Model.Dish.Name}:\n\n";
            foreach (var ingredient in PlanPatient.PlanModel.PlanOptionModel3.Collation1Model.Dish.DishFoodModel)
            {
                text += $"- {ingredient.Quantity} {ingredient.Food.Unit.Name} {ingredient.Food.Name}\n\n";
            }
            tf.DrawString(text, ingFont, XBrushes.Gray, textArea, XStringFormats.TopLeft);
            #endregion

            #region Meal
            gfx.DrawRoundedRectangle(pen, 70, 355, 150, 180, 5, 5);
            textArea = new XRect(80, 365, 140, 170);

            text = $"{PlanPatient.PlanModel.PlanOptionModel1.MealModel.Dish.Name}:\n\n";
            foreach (var ingredient in PlanPatient.PlanModel.PlanOptionModel1.MealModel.Dish.DishFoodModel)
            {
                text += $"- {ingredient.Quantity} {ingredient.Food.Unit.Name} {ingredient.Food.Name}\n\n";
            }
            tf.DrawString(text, ingFont, XBrushes.Gray, textArea, XStringFormats.TopLeft);


            gfx.DrawRoundedRectangle(pen, 245, 355, 150, 180, 5, 5);
            textArea = new XRect(255, 365, 140, 170);

            text = $"{PlanPatient.PlanModel.PlanOptionModel2.MealModel.Dish.Name}:\n\n";
            foreach (var ingredient in PlanPatient.PlanModel.PlanOptionModel2.MealModel.Dish.DishFoodModel)
            {
                text += $"- {ingredient.Quantity} {ingredient.Food.Unit.Name} {ingredient.Food.Name}\n\n";
            }
            tf.DrawString(text, ingFont, XBrushes.Gray, textArea, XStringFormats.TopLeft);


            gfx.DrawRoundedRectangle(pen, 420, 355, 150, 180, 5, 5);
            textArea = new XRect(430, 365, 140, 170);

            text = $"{PlanPatient.PlanModel.PlanOptionModel3.MealModel.Dish.Name}:\n\n";
            foreach (var ingredient in PlanPatient.PlanModel.PlanOptionModel3.MealModel.Dish.DishFoodModel)
            {
                text += $"- {ingredient.Quantity} {ingredient.Food.Unit.Name} {ingredient.Food.Name}\n\n";
            }
            tf.DrawString(text, ingFont, XBrushes.Gray, textArea, XStringFormats.TopLeft);


            #endregion

            #region Collation2
            gfx.DrawRoundedRectangle(pen, 70, 545, 150, 70, 5, 5);
            textArea = new XRect(80, 555, 140, 60);

            text = $"{PlanPatient.PlanModel.PlanOptionModel1.Collation2Model.Dish.Name}:\n\n";
            foreach (var ingredient in PlanPatient.PlanModel.PlanOptionModel1.Collation2Model.Dish.DishFoodModel)
            {
                text += $"- {ingredient.Quantity} {ingredient.Food.Unit.Name} {ingredient.Food.Name}\n\n";
            }
            tf.DrawString(text, ingFont, XBrushes.Gray, textArea, XStringFormats.TopLeft);


            gfx.DrawRoundedRectangle(pen, 245, 545, 150, 70, 5, 5);
            textArea = new XRect(255, 555, 140, 60);

            text = $"{PlanPatient.PlanModel.PlanOptionModel2.Collation2Model.Dish.Name}:\n\n";
            foreach (var ingredient in PlanPatient.PlanModel.PlanOptionModel2.Collation2Model.Dish.DishFoodModel)
            {
                text += $"- {ingredient.Quantity} {ingredient.Food.Unit.Name} {ingredient.Food.Name}\n\n";
            }
            tf.DrawString(text, ingFont, XBrushes.Gray, textArea, XStringFormats.TopLeft);
        

            gfx.DrawRoundedRectangle(pen, 420, 545, 150, 70, 5, 5);
            textArea = new XRect(430, 555, 140, 60);

            text = $"{PlanPatient.PlanModel.PlanOptionModel3.Collation2Model.Dish.Name}:\n\n";
            foreach (var ingredient in PlanPatient.PlanModel.PlanOptionModel3.Collation2Model.Dish.DishFoodModel)
            {
                text += $"- {ingredient.Quantity} {ingredient.Food.Unit.Name} {ingredient.Food.Name}\n\n";
            }
            tf.DrawString(text, ingFont, XBrushes.Gray, textArea, XStringFormats.TopLeft);
            #endregion

            #region Dinner
            gfx.DrawRoundedRectangle(pen, 70, 625, 150, 180, 5, 5);
            textArea = new XRect(80, 635, 140, 170);

            text = $"{PlanPatient.PlanModel.PlanOptionModel1.DinnerModel.Dish.Name}:\n\n";
            foreach (var ingredient in PlanPatient.PlanModel.PlanOptionModel1.DinnerModel.Dish.DishFoodModel)
            {
                text += $"- {ingredient.Quantity} {ingredient.Food.Unit.Name} {ingredient.Food.Name}\n\n";
            }
            tf.DrawString(text, ingFont, XBrushes.Gray, textArea, XStringFormats.TopLeft);


            gfx.DrawRoundedRectangle(pen, 245, 625, 150, 180, 5, 5);
            textArea = new XRect(255, 635, 140, 170);

            text = $"{PlanPatient.PlanModel.PlanOptionModel2.DinnerModel.Dish.Name}:\n\n";
            foreach (var ingredient in PlanPatient.PlanModel.PlanOptionModel2.DinnerModel.Dish.DishFoodModel)
            {
                text += $"- {ingredient.Quantity} {ingredient.Food.Unit.Name} {ingredient.Food.Name}\n\n";
            }
            tf.DrawString(text, ingFont, XBrushes.Gray, textArea, XStringFormats.TopLeft);


            gfx.DrawRoundedRectangle(pen, 420, 625, 150, 180, 5, 5);
            textArea = new XRect(430, 635, 140, 170);

            text = $"{PlanPatient.PlanModel.PlanOptionModel3.DinnerModel.Dish.Name}:\n\n";
            foreach (var ingredient in PlanPatient.PlanModel.PlanOptionModel3.DinnerModel.Dish.DishFoodModel)
            {
                text += $"- {ingredient.Quantity} {ingredient.Food.Unit.Name} {ingredient.Food.Name}\n\n";
            }
            tf.DrawString(text, ingFont, XBrushes.Gray, textArea, XStringFormats.TopLeft);


            #endregion


            string filename = SelectedPatient.LastNameP + "_" + SelectedPatient.Name + "_" + today.ToString("yyyy-MM-dd");
            document.Save(filename);

            Process.Start(new ProcessStartInfo(filename) { UseShellExecute = true });
        }
    }

    [RelayCommand]
    private void AddNewPlan()
    {
        if (SelectedPatient != null)
        {
            PlanPatient = new PlanPatientModel
            {
                IdPlan = 0,
                IdPatient = SelectedPatient.IdPatient,
                PatientModel = SelectedPatient,
                PlanModel = new PlanModel
                {
                    PlanOptionModel1 = new PlanOptionModel(),
                    PlanOptionModel2 = new PlanOptionModel(),
                    PlanOptionModel3 = new PlanOptionModel(),
                }
            };
        }
    }

    public void OnNavigatedTo()
    {
        if (!_isInitialized)
            InitializeViewModel();
    }

    public void OnNavigatedFrom()
    {
        _isInitialized = false;
    }

    public void InitializeViewModel()
    {
        this.dataAccess = new DataAccess();

        SelectedPatient = new PatientModel();

        Patients = new List<PatientModel>();
        Patients = this.dataAccess.GetPatients();

        _isInitialized = true;
    }

    public void Reload()
    {

    }

    public void ApplyFilter()
    {
        if (!string.IsNullOrEmpty(FilterText))
        {
            SelectedPatient = Patients.Where(p => p.FullName.Equals(FilterText)).FirstOrDefault();
        }
    }

    public void GetNutritionalPlan()
    { 
        if (SelectedPatient != null)
        {
            ChooseMessageVisibility = Visibility.Hidden;

            var planPatients = this.dataAccess.GetPlanPatient(SelectedPatient.IdPatient);
            if (planPatients.Any())
            {
                PlanPatient = planPatients.First();
            }
            else
            {
                PlanPatient = null;
            }

            if (PlanPatient != null)
            {
                GridVisibility = Visibility.Visible;
                MessageVisibility = Visibility.Hidden;
            }
            else
            {
                MessageVisibility = Visibility.Visible;
                GridVisibility = Visibility.Hidden;
            }
        }
    }

}
