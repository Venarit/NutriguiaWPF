using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
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

namespace UiNutriguia.Views.Pages
{
    /// <summary>
    /// Lógica de interacción para PatientsAddPage.xaml
    /// </summary>
    public partial class PatientsAddPage : Page
    {
        public PatientsViewModel ViewModel { get; set; }
        public PatientsAddPage(PatientsViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }

        private void SavePatient(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tbName.Text) || !string.IsNullOrEmpty(tbLastNameP.Text) ||
                !string.IsNullOrEmpty(tbEmail.Text) || !string.IsNullOrEmpty(tbCellphone.Text) || 
                !string.IsNullOrEmpty(tbBirthDate.Text))
            {
                string resultMessage = string.Empty;
                bool result = false;

                try
                {
                    DateTime dt = DateTime.Parse(tbBirthDate.Text);
                    string dateBirth = dt.Year.ToString() + "-" + dt.Month.ToString("00") + "-" + dt.Day.ToString("00");

                    PatientModel patient = new PatientModel
                    {
                        Name = tbName.Text.Trim(),
                        SecondName = string.IsNullOrEmpty(tbSecondName.Text)? null : tbSecondName.Text.Trim(),
                        LastNameP = tbLastNameP.Text.Trim(),
                        LastNameM = string.IsNullOrEmpty(tbLastNameM.Text) ? null : tbLastNameM.Text.Trim(),
                        Email = tbEmail.Text.Trim(),
                        Cellphone = tbCellphone.Text.Trim(),
                        BirthDate = dateBirth
                    };

                    ViewModel.AddPatient(patient);
                    resultMessage = "Paciente registrado con éxito en la base de datos";
                    result = true;
                }
                catch(Exception ex)
                {
                    resultMessage = ex.Message;
                    Console.WriteLine("Error retrieving patients: " + ex.Message);
                }
                
                ViewModel.OpenSnackBar(result, resultMessage);
            }
        }

    }
}
