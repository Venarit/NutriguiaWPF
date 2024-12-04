using UiNutriguia.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace UiNutriguia.Views.Pages;

public partial class PlansAddPage : INavigableView<PlansAddViewModel>
{
    public PlansAddViewModel ViewModel { get; set; }
    public PlansAddPage(PlansAddViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }
}
