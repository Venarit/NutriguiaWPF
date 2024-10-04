using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UiNutriguia.Models;
using UiNutriguia.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace UiNutriguia.Views.Pages
{
    /// <summary>
    /// Lógica de interacción para PatientsAddProfilePage.xaml
    /// </summary>
    public partial class PatientsAddProfilePage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public PatientsViewModel ViewModel { get; set; }

        #region private members
        private ActivityModel selectedActivity;
        private ObjectiveModel selectedObjective;
        private MacronutrientModel selectedMacronutrient;
        private string selectedSex;
        private int errorCount;
        #endregion
        #region Properties
        public ActivityModel SelectedActivity
        {
            get { return this.selectedActivity; }
            set
            {
                if (this.selectedActivity != value)
                {
                    this.selectedActivity = value;
                    OnPropertyChanged("SelectedActivity");
                }
            }
        }
        public ObjectiveModel SelectedObjective
        {
            get { return this.selectedObjective; }
            set
            {
                if (this.selectedObjective != value)
                {
                    this.selectedObjective = value;
                    OnPropertyChanged("SelectedObjective");
                }
            }
        }
        public MacronutrientModel SelectedMacronutrient
        {
            get { return this.selectedMacronutrient; }
            set
            {
                if (this.selectedMacronutrient != value)
                {
                    this.selectedMacronutrient = value;
                    OnPropertyChanged("SelectedMacronutrient");
                }
            }

        }
        public string SelectedSex
        {
            get { return this.selectedSex; }
            set
            {
                if (this.selectedSex != value)
                {
                    this.selectedSex = value;
                    OnPropertyChanged("SelectedSex");
                }
            }
        }

        public PatientModel Patient { get; set; }
        #endregion

        public PatientsAddProfilePage(PatientsViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            //Patient = patient;

            InitializeComponent();
        }

        private void SaveProfile(object sender, RoutedEventArgs e)
        {
            if (nbHeight.Value == null || SelectedSex == null || SelectedObjective == null || SelectedMacronutrient == null
                || SelectedActivity == null)
            {
                return;
            }

            try
            {
                NutritionalProfileModel nutritionalProfile = new NutritionalProfileModel
                {
                    Height = (int)nbHeight.Value,
                    Sex = SelectedSex,
                    Activity = SelectedActivity,
                    Objective = SelectedObjective,
                    Macronutrient = SelectedMacronutrient,
                };

                ViewModel.AddProfile(nutritionalProfile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void nB_TextChanged(object sender, TextChangedEventArgs e)
        {
            NumberBox numBox = (NumberBox)sender;
            if (numBox.Text.Length < 10)
            {
                if (numBox.BorderBrush != Brushes.Red)
                    errorCount++;
                numBox.BorderBrush = Brushes.Red;
            }
            else if (numBox.BorderBrush == Brushes.Red)
            {
                errorCount--;
                numBox.BorderBrush = null;
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
