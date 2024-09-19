using Nutriguia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using Wpf.Ui.Controls;

namespace Nutriguia
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
            ShowLoginDialog();
        }

        private async Task ShowLoginDialog()
        {
            try
            {
                Login loginDialog = new Login();

                bool? result = loginDialog.ShowDialog();

                if (result == true)
                {
                    //TODO: User Login logic
                }
                else 
                {
                    Application.Current.Shutdown();
                }
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.Message);
            }
            
        }

    }
}
