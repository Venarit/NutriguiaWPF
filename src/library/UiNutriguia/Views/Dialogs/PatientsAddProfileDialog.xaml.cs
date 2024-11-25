using System.Windows.Controls;
using UiNutriguia.ViewModels.Dialogs;
using Wpf.Ui.Controls;

namespace UiNutriguia.Views.Dialogs;
public partial class PatientsAddProfileDialog : ContentDialog
{
    public PatientsAddProfileViewModel ViewModel { get; set; }
    public PatientsAddProfileDialog(ContentPresenter? contentPresenter, PatientsAddProfileViewModel viewModel)
        : base(contentPresenter)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();

        viewModel.CloseDialog = () => this.Hide();
    }
}
