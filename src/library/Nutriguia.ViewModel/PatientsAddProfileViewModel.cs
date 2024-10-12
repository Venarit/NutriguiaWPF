using Nutriguia.Model.Models;
using Wpf.Ui;
using Wpf.Ui.Controls;
using Nutriguia.Model.DataAccess;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using Prism.Mvvm;
using Prism.Commands;
using Prism.Events;

namespace Nutriguia.ViewModel
{
    public class PatientsAddProfileViewModel : PageViewModelBase
    {
        #region Private Members

        private ControlAppearance _snackbarAppearance = ControlAppearance.Secondary;
        private DataAccess dataAccess;

        private readonly BackgroundWorker backgroundWorkerLoad;
        private readonly BackgroundWorker backgroundWorkerSave;

        private ObservableCollection<ActivityModel> activities;
        private ObservableCollection<ObjectiveModel> objectives;
        private ObservableCollection<MacronutrientModel> macronutrients;

        private PatientModel patient;
        private ActivityModel selectedActivity;
        private ObjectiveModel selectedObjective;
        private MacronutrientModel selectedMacronutrient;
        private string selectedSex;
        private int height;

        private bool enableSaveCommand;
        private bool isAvailable = true;

        #endregion

        public PatientsAddProfileViewModel(IRegionManager regionManager) : base(regionManager)
        {
            this.Activities = new ObservableCollection<ActivityModel>();
            this.Objectives = new ObservableCollection<ObjectiveModel>();
            this.Macronutrients = new ObservableCollection<MacronutrientModel>();

            //Load
            this.backgroundWorkerLoad = new BackgroundWorker();
            this.backgroundWorkerLoad.DoWork += this.BackgroundWorkerLoadDoWork;
            this.backgroundWorkerLoad.RunWorkerCompleted += this.BackgroundWorkerLoadCompleted;

            //Save
            this.backgroundWorkerSave = new BackgroundWorker();
            this.backgroundWorkerSave.DoWork += this.BackgroundWorkerSaveDoWork;
            this.backgroundWorkerSave.RunWorkerCompleted += this.BackgroundWorkerSaveCompleted;

            this.enableSaveCommand = false;

            this.SaveCommand = new DelegateCommand(this.Save, () => this.enableSaveCommand);
        }

        #region BW Save
        private void Save()
        {
            this.backgroundWorkerSave.RunWorkerAsync();
        }

        private void BackgroundWorkerSaveDoWork(object sender, DoWorkEventArgs e)
        {
            this.IsAvailable = false;
            DataAccess.Instance.SetPatientNutritionalProfile(new NutritionalProfileModel
            {
                Id = 0,
                IdPatient = this.Patient.Id,
                Height = this.Height,
                Sex = this.SelectedSex,
                IdActivity = this.SelectedActivity.Id,
                IdObjective = this.SelectedObjective.Id,
                IdMacronutrient = this.SelectedMacronutrient.Id,
                Activity = this.SelectedActivity,
                Objective = this.SelectedObjective,
                Macronutrient = this.SelectedMacronutrient
            });
        }

        private void BackgroundWorkerSaveCompleted(object sender, RunWorkerCompletedEventArgs e) 
        { 
            this.IsAvailable = true;
        }
        #endregion

        #region BW Load
        private void Load()
        {
            this.backgroundWorkerLoad.RunWorkerAsync();
        }

        private void BackgroundWorkerLoadDoWork(object sender, DoWorkEventArgs e)
        {
            this.IsAvailable = false;

            var activities = DataAccess.Instance.GetActivities();
            this.activities.Clear();
            foreach (var activity in activities)
            {
                this.activities.Add(activity);
            }

            var objectives = DataAccess.Instance.GetObjectives();
            this.objectives.Clear();
            foreach (var objective in objectives)
            {
                this.objectives.Add(objective);
            }

            var macros = DataAccess.Instance.GetMacronutrients();
            this.macronutrients.Clear();
            foreach(var macro in macros)
            {
                this.macronutrients.Add(macro);
            }

        }

        private void BackgroundWorkerLoadCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.IsAvailable = true;
        }
        #endregion

        #region Properties
        public ObservableCollection<ActivityModel> Activities
        {
            get { return this.activities; }
            set
            {
                SetProperty(ref this.activities, value);
            }
        }
        public ObservableCollection<ObjectiveModel> Objectives
        {
            get { return this.objectives; }
            set
            {
                SetProperty(ref this.objectives, value);
            }
        }
        public ObservableCollection<MacronutrientModel> Macronutrients
        {
            get { return this.macronutrients; }
            set
            {
                SetProperty(ref this.macronutrients, value);
            }
        }
        public PatientModel Patient
        {
            get { return this.patient; }
            set
            {
                SetProperty(ref this.patient, value);
            }
        }
        public ActivityModel SelectedActivity
        {
            get { return this.selectedActivity; }
            set
            {
                SetProperty(ref this.selectedActivity, value);
            }
        }
        public ObjectiveModel SelectedObjective
        {
            get { return this.selectedObjective; }
            set
            {
                SetProperty(ref this.selectedObjective, value);
            }
        }
        public MacronutrientModel SelectedMacronutrient
        {
            get { return this.selectedMacronutrient; }
            set
            {
                SetProperty(ref this.selectedMacronutrient, value);
            }

        }
        public string SelectedSex
        {
            get { return this.selectedSex; }
            set
            {
                SetProperty(ref this.selectedSex, value);
            }
        }
        public int Height
        {
            get { return this.height; }
            set
            {
                SetProperty(ref this.height, value);
            }
        }
        public bool IsAvailable
        {
            get
            {
                return this.isAvailable;
            }
            set
            {
                SetProperty(ref this.isAvailable, value);
            }
        }

        public DelegateCommand SaveCommand
        {
            get;
            set;
        }
        #endregion


    }
}
