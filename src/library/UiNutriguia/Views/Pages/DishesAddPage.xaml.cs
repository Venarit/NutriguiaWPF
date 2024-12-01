using System.Windows.Controls;
using System.Windows.Navigation;
using UiNutriguia.Models;
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

    private void Grid_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        if (sender is Grid grid && grid.DataContext is DishFoodModel dishFood)
        {
            lv_DishFood.SelectedItem = dishFood;
        }
    }

    private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        if (!string.IsNullOrEmpty(sender.Text))
        {
            ViewModel.ApplyFood(sender.Text);
        }
    }
}
