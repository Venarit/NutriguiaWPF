using System.Windows.Controls;
using UiNutriguia.ViewModels.Dialogs;
using Wpf.Ui.Controls;

namespace UiNutriguia.Views.Dialogs;

public partial class PatientsAddDislikedFoodDialog : ContentDialog
{
    public PatientAddDislikedFoodViewModel ViewModel { get; set; }
    public PatientsAddDislikedFoodDialog(ContentPresenter? contentPresenter, PatientAddDislikedFoodViewModel viewModel)
        : base(contentPresenter)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();

        viewModel.CloseDialog = () => this.Hide();
    }

}
