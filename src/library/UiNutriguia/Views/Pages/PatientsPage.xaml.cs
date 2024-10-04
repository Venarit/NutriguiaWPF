using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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
                
                if (this.SelectedPatient.NutritionalProfileModel != null)
                {   
                    // TODO : IMPROVE
                    tb_nullNutritionalProfile.Visibility = Visibility.Collapsed;
                    if (this.SelectedPatient.NutritionalProfileModel.PatientMeasurement != null)
                    {
                        tb_nullPatientMeasurement.Visibility = Visibility.Collapsed;
                    }
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
