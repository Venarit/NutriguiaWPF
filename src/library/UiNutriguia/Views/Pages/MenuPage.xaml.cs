using UiNutriguia.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace UiNutriguia.Views.Pages
{
    /// <summary>
    /// Lógica de interacción para MenuPage.xaml
    /// </summary>
    public partial class MenuPage : INavigableView<MenuViewModel>
    {
        public MenuViewModel ViewModel { get; }
        public MenuPage(MenuViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
