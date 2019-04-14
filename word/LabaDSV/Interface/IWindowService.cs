using System.Windows;

namespace LabaDSV.Interface
{
    public interface IWindowService
    {
        void Show<TWindow>(object viewModel) where TWindow : Window, new();
    }
}
