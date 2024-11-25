using System.Windows.Controls;
using UiNutriguia.ViewModels.Dialogs;
using Wpf.Ui.Controls;

namespace UiNutriguia.Views.Dialogs;

public partial class PatientsAddDialog : ContentDialog
{
    public PatientsAddViewModel ViewModel { get; set; }
    public PatientsAddDialog(ContentPresenter? contentPresenter, PatientsAddViewModel viewModel)
        : base(contentPresenter)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();

        viewModel.CloseDialog = () => this.Hide();
    }

}
