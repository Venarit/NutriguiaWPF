using UiNutriguia.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace UiNutriguia.Views.Pages
{
    public partial class FoodPage : INavigableView<FoodViewModel>
    {
        public FoodViewModel ViewModel { get; }
        public FoodPage(FoodViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
