using UiNutriguia.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace UiNutriguia.Views.Pages
{
    public partial class PlansPage : INavigableView<PlansViewModel>
    {
        public PlansViewModel ViewModel { get; }
        public PlansPage(PlansViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
