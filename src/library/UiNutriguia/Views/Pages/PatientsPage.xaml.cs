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
using UiNutriguia.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace UiNutriguia.Views.Pages
{
    /// <summary>
    /// Lógica de interacción para PatientsPage.xaml
    /// </summary>
    public partial class PatientsPage : INavigableView<PatientsViewModel>
    {
        public PatientsViewModel ViewModel {  get; }

        public PatientsPage(PatientsViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
