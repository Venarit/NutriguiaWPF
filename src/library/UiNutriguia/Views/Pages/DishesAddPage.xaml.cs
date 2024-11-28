using UiNutriguia.ViewModels.Pages;
using Wpf.Ui.Controls;

namespace UiNutriguia.Views.Pages;

public partial class DishesAddPage : INavigableView<DishesAddViewModel>
{
    public DishesAddViewModel ViewModel { get; set; }
    public DishesAddPage(DishesAddViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = this;

        InitializeComponent();
    }
}
