using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using UiNutriguia.Models;
using UiNutriguia.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace UiNutriguia.Views.Pages
{
    /// <summary>
    /// Lógica de interacción para PatientsPage.xaml
    /// </summary>
    public partial class PatientsPage : INavigableView<PatientsViewModel>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public PatientsViewModel ViewModel {  get; }

        #region private members
        
        private PatientModel selectedPatient;
        private string selectedPatientName = string.Empty;

        #endregion

        #region Properties

        public PatientModel SelectedPatient 
        {  
            get { return this.selectedPatient; }
            set
            {
                if (this.selectedPatient != value)
                {
                    this.selectedPatient = value;
                    OnPropertyChanged("SelectedPatient");
                }
            }
        }

        public string SelectedPatientName
        {
            get { return this.selectedPatientName; }
            set
            {
                if (this.selectedPatientName != value)
                {
                    this.selectedPatientName = value;
                    OnPropertyChanged("SelectedPatientName");
                }
            }
        }

        #endregion

        public PatientsPage(PatientsViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Wpf.Ui.Controls.ListView listView = (Wpf.Ui.Controls.ListView)sender;
            
            if (listView.SelectedItem != null)
            {
                this.SelectedPatient = (PatientModel)listView.SelectedItem;
                this.SelectedPatientName = String.Concat((this.SelectedPatient.Name + " "), this.SelectedPatient.SecondName != null? (this.SelectedPatient.SecondName + " ") : "", (this.selectedPatient.LastNameP + " "), this.selectedPatient.LastNameM);
                
                if (this.SelectedPatient.NutritionalProfile != null)
                {   
                    tb_nullNutritionalProfile.Visibility = Visibility.Collapsed;
                    gd_NutritionalProfile.Visibility = Visibility.Visible;

                    if (this.SelectedPatient.NutritionalProfile.PatientMeasurement != null)
                    {
                        gd_PatientMeasurements.Visibility = Visibility.Visible;
                        tb_nullPatientMeasurement.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        gd_PatientMeasurements.Visibility = Visibility.Collapsed;
                        tb_nullPatientMeasurement.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    gd_NutritionalProfile.Visibility = Visibility.Collapsed;
                    gd_PatientMeasurements.Visibility = Visibility.Collapsed;
                    tb_nullNutritionalProfile.Visibility = Visibility.Visible;
                    tb_nullPatientMeasurement.Visibility = Visibility.Visible;
                }

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
