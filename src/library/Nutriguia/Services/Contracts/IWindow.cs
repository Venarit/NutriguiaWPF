using System.Windows;

namespace Nutriguia.Services.Contracts
{
    public interface IWindow
    {
        event RoutedEventHandler Loaded;

        void Show();
    }

}
