using UiNutriguia.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace UiNutriguia.Views.Pages;

public partial class SchedulerPage : INavigableView<SchedulerViewModel>
{
    public SchedulerViewModel ViewModel { get; }

    public SchedulerPage(SchedulerViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }
}
