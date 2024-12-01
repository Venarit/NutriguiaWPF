using System.ComponentModel;
using System.Windows.Controls;
using UiNutriguia.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace UiNutriguia.Views.Pages;

public partial class PatientProfilePage : INavigableView<PatientProfileViewModel>
{
    public PatientProfileViewModel ViewModel { get; }
    public PatientProfilePage(PatientProfileViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }
}
