using System.Windows.Controls;
using UiNutriguia.ViewModels.Dialogs;
using Wpf.Ui.Controls;

namespace UiNutriguia.Views.Dialogs;
public partial class PatientsAddMeasurementDialog : ContentDialog
{
    public PatientsAddMeasurementViewModel ViewModel { get; set; }
    public PatientsAddMeasurementDialog(ContentPresenter? contentPresenter, PatientsAddMeasurementViewModel viewModel)
        : base(contentPresenter)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();

        viewModel.CloseDialog = () => this.Hide();
    }
}
