using System.Windows.Controls;
using UiNutriguia.ViewModels.Dialogs;
using UiNutriguia.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace UiNutriguia.Views.Dialogs;

public partial class SchedulerAddDialog : ContentDialog
{
    public SchedulerAddViewModel ViewModel { get; }
    public SchedulerAddDialog(ContentPresenter? contentPresenter, SchedulerAddViewModel viewModel)
        : base(contentPresenter)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }
}
