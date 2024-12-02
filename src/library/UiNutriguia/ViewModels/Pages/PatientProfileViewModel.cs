using Wpf.Ui.Controls;
using Wpf.Ui;
using Nutriguia.Model.DataAccess;
using UiNutriguia.Models;
using LiveCharts;
using LiveCharts.Wpf;
using LiveCharts.Defaults;
using UiNutriguia.ViewModels.Dialogs;
using UiNutriguia.Views.Dialogs;

namespace UiNutriguia.ViewModels.Pages;

public partial class PatientProfileViewModel(INavigationService navigationService, IContentDialogService dialogService) : ObservableObject, INavigationAware
{
    private bool _isInitialized = false;
    private DataAccess dataAccess;
    private AxesCollection XAxis;

    [ObservableProperty] private PatientModel _patient;
    [ObservableProperty] private List<PatientMeasurementModel> _patientMeasurements;
    [ObservableProperty] private List<AppointmentModel> _patientAppointments;
    [ObservableProperty] private AppointmentModel _patientNextAppointment;
    [ObservableProperty] private AppointmentModel _patientLastAppointment;
    [ObservableProperty] private List<FoodModel> _patientDislikedFoods;
    [ObservableProperty] private FoodModel _selectedFood;
    [ObservableProperty] private int _patientAge;
    [ObservableProperty] private double _lowBodyFat;
    [ObservableProperty] private double _normalBodyFat;
    [ObservableProperty] private double _highBodyFat;
    [ObservableProperty] private double _veryHighBodyFat;
    [ObservableProperty] private Visibility _maleVisibility;
    [ObservableProperty] private Visibility _femaleVisibility;

    public PatientAddDislikedFoodViewModel PatientAddDislikedFoodViewModel { get; set; }
    public SeriesCollection SeriesCollection {  get; set; }
    public SeriesCollection PieSeriesCollection {  get; set; }
    public List<string> XAxisLabels { get; private set; }
    public string[] Labels { get; set; }

    [RelayCommand]
    private void GotoPage(Type type)
    {
        _ = navigationService.Navigate(type);
    }

    public void OnNavigatedTo()
    {
        if (NavigationContext.Parameter is PatientModel model)
        {
            Patient = model;
            NavigationContext.Parameter = null;
        }

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

        SeriesCollection = new SeriesCollection();
        PieSeriesCollection = new SeriesCollection();
        XAxisLabels = new List<string>();

        PatientMeasurements = new List<PatientMeasurementModel>();
        PatientMeasurements = dataAccess.GetPatientMeasurements(Patient.IdPatient);

        PatientAppointments = new List<AppointmentModel>();
        PatientAppointments = dataAccess.GetPatientAppointments(Patient.IdPatient);

        PatientDislikedFoods = new List<FoodModel>();
        PatientDislikedFoods = dataAccess.GetPatientDislikedFoods(Patient.IdPatient);

        GetBodyFatValues();
        GetMeasurementsChart();
        GetNextLastAppointment();
        GetMacronutrientChart();

        _isInitialized = true;
    }

    [RelayCommand]
    private async Task AddDislikedFoods()
    {
        var patient = Patient;

        PatientAddDislikedFoodViewModel = new PatientAddDislikedFoodViewModel(patient)
        {
            Refresh = () => RefreshDislikedFoods()
        };

        var patientDislikedFoodDialog = new PatientsAddDislikedFoodDialog(dialogService.GetDialogHost(), PatientAddDislikedFoodViewModel);
            
        _ = await patientDislikedFoodDialog.ShowAsync();
    }

    [RelayCommand]
    public void DeleteDislikedFood(FoodModel foodModel)
    {
        this.dataAccess.SetPatientDislikedFood(Patient.IdPatient, foodModel.IdFood);
        RefreshDislikedFoods();
    }

    public void RefreshDislikedFoods()
    {
        PatientDislikedFoods = dataAccess.GetPatientDislikedFoods(Patient.IdPatient);
    }

    public void GetBodyFatValues()
    {
        if (Patient.NutritionalProfile != null)
        {
            // Getting patient age
            var dob = DateTime.ParseExact(Patient.BirthDate, "MM/dd/yyyy HH:mm:ss", null);
            var age = DateTime.Today.Year - dob.Year;
            if (dob.Date > DateTime.Today.AddYears(-age)) age--;

            if (Patient.NutritionalProfile.Sex.Equals("M"))
            {
                MaleVisibility = Visibility.Visible;
                FemaleVisibility = Visibility.Collapsed;

                if (age >= 20 && age <= 39)
                {
                    LowBodyFat = 7.9;
                    NormalBodyFat = 8.0;
                    HighBodyFat = 20.0;
                    VeryHighBodyFat = 25;
                }
                else if (age >= 40 && age <= 59)
                {
                    LowBodyFat = 10.9;
                    NormalBodyFat = 11;
                    HighBodyFat = 22;
                    VeryHighBodyFat = 28;
                }
                else
                {
                    LowBodyFat = 12.9;
                    NormalBodyFat = 13;
                    HighBodyFat = 25;
                    VeryHighBodyFat = 30;
                }
            }
            else if (Patient.NutritionalProfile.Sex.Equals("F"))
            {
                MaleVisibility = Visibility.Collapsed;
                FemaleVisibility = Visibility.Visible;

                if (age >= 20 && age <= 39)
                {
                    LowBodyFat = 20.9;
                    NormalBodyFat = 21;
                    HighBodyFat = 33;
                    VeryHighBodyFat = 39;
                }
                else if (age >= 40 && age <= 59)
                {
                    LowBodyFat = 22.9;
                    NormalBodyFat = 23;
                    HighBodyFat = 34;
                    VeryHighBodyFat = 40;
                }
                else
                {
                    LowBodyFat = 23.9;
                    NormalBodyFat = 24;
                    HighBodyFat = 36;
                    VeryHighBodyFat = 42;
                }
            }
        }
    }

    public void GetMeasurementsChart()
    {
        var weightValues = new ChartValues<double>();
        var bodyfatValues = new ChartValues<double>();
        XAxisLabels = new List<string>();

        foreach (var measurement in PatientMeasurements)
        {
            weightValues.Add((double)measurement.Weight);
            bodyfatValues.Add((double)measurement.BodyFat);
            var datetime = DateTime.Parse(measurement.InsDateTime.ToString());
            XAxisLabels.Add((DateOnly.FromDateTime(datetime)).ToString());
        }

        SeriesCollection = new SeriesCollection
        {
            new LineSeries { Title = "Peso", Values = weightValues },
            new LineSeries { Title = "Grasa Corporal", Values = bodyfatValues }
        };

        OnPropertyChanged(nameof(SeriesCollection));
        OnPropertyChanged(nameof(XAxisLabels));
    }

    public void GetMacronutrientChart()
    {
        var tdee = Patient.NutritionalProfile.PatientMeasurement.TDEE;
        var hco = Patient.NutritionalProfile.Macronutrient.Hco;
        var protein = Patient.NutritionalProfile.Macronutrient.Protein;
        var lipids = Patient.NutritionalProfile.Macronutrient.Lipids;

        PieSeriesCollection = new SeriesCollection
        {
            new PieSeries
            {
                Title = "Carbohidratos",
                Values = new ChartValues<ObservableValue> { new ObservableValue((double)(tdee * hco)) },
                DataLabels = true
            },
            new PieSeries
            {
                Title = "Proteina",
                Values = new ChartValues<ObservableValue> { new ObservableValue((double)(tdee * protein)) },
                DataLabels = true
            },
            new PieSeries
            {
                Title = "Grasas",
                Values = new ChartValues<ObservableValue> { new ObservableValue((double)(tdee * lipids)) },
                DataLabels = true
            }
        };
    }

    public void GetNextLastAppointment()
    {
        if (PatientAppointments != null)
        {
            if (PatientAppointments.Any())
            {
                var sortedAppointments = PatientAppointments.OrderBy(a => a.StartDateTime).ToList();
                PatientLastAppointment = sortedAppointments.LastOrDefault(a => a.StartDateTime <= DateTime.Now);
                PatientNextAppointment = sortedAppointments.FirstOrDefault(a => a.StartDateTime > DateTime.Now);
            }
        }
    }
}
