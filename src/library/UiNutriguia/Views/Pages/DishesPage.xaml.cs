using UiNutriguia.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace UiNutriguia.Views.Pages
{
    public partial class DishesPage : INavigableView<DishesViewModel>
    {
        public DishesViewModel ViewModel { get; }
        public DishesPage(DishesViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
