using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media;
using UiNutriguia.Models;
using UiNutriguia.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace UiNutriguia.Views.Pages
{
    /// <summary>
    /// Lógica de interacción para PatientsAddPage.xaml
    /// </summary>
    public partial class PatientsAddPage : Page
    {
        private int errorCount;
        public PatientsViewModel ViewModel { get; set; }
        public PatientsAddPage(PatientsViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }

        private void SavePatient(object sender, RoutedEventArgs e)
        {
            if ((!string.IsNullOrEmpty(tbName.Text) || !string.IsNullOrEmpty(tbLastNameP.Text) ||
                !string.IsNullOrEmpty(tbEmail.Text) || !string.IsNullOrEmpty(tbCellphone.Text) ||
                !string.IsNullOrEmpty(tbBirthDate.Text)) && errorCount < 1)
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
                        SecondName = string.IsNullOrEmpty(tbSecondName.Text) ? null : tbSecondName.Text.Trim(),
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
                catch (Exception ex)
                {
                    resultMessage = ex.Message;
                    Console.WriteLine("Error retrieving patients: " + ex.Message);
                }

                //TODO : DISPLAY SQL RESULT MESSAGE IN SNACKBAR
                //ViewModel.OpenSnackBar(result, resultMessage);
            }
            else
            { 
                foreach(var child in GridPatient.Children)
                {
                    if (child is StackPanel stackPanel)
                    {
                        SetChildInStackPanelRed(stackPanel);
                    }
                }
            }
        }

        private void SetChildInStackPanelRed(StackPanel stackPanel)
        {
            foreach (var child in stackPanel.Children)
            {
                if (child is Wpf.Ui.Controls.TextBox textBox)
                {
                    if (textBox.Text.IsNullOrEmpty())
                    {
                        errorCount++;
                        textBox.BorderBrush = Brushes.Red;
                    }
                }
                else if (child is DatePicker datePicker)
                {
                    if (datePicker.Text.IsNullOrEmpty())
                    {
                        errorCount++;
                        datePicker.BorderBrush = Brushes.Red;
                    }
                }
                else if (child is NumberBox numBox)
                {
                    if (numBox.Text.IsNullOrEmpty())
                    {
                        errorCount++;
                        numBox.BorderBrush = Brushes.Red;
                    }
                }
            }
        }

        private void tb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Wpf.Ui.Controls.TextBox textBox = (Wpf.Ui.Controls.TextBox)sender;
            
            if(textBox.Text.Length < 2)
            {
                if (textBox.BorderBrush != Brushes.Red)
                    errorCount++;
                textBox.BorderBrush = Brushes.Red;
            }
            else if (textBox.BorderBrush == Brushes.Red)
            {
                errorCount--;
                textBox.BorderBrush = null;
            }
        }

        private void tbEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            Wpf.Ui.Controls.TextBox textBox = (Wpf.Ui.Controls.TextBox)sender;
            if (!IsValidEmailAddress(textBox.Text))
            {
                if (textBox.BorderBrush != Brushes.Red)
                    errorCount++;
                textBox.BorderBrush = Brushes.Red;
            }
            else if (textBox.BorderBrush == Brushes.Red)
            {
                errorCount--;
                textBox.BorderBrush = null;
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

        private void dp_DateChanged(object sender, SelectionChangedEventArgs e)
        {
            DatePicker datePicker = (DatePicker)sender;
            if (datePicker.BorderBrush == Brushes.Red)
            {
                errorCount--;
                datePicker.BorderBrush = null;
            }
        }

        public static bool IsValidEmailAddress(string s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }

    }
}
