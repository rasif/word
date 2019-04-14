using System.Windows;
using LabaDSV.Helpers;
using LabaDSV.Service;
using LabaDSV.View;
using LabaDSV.ViewModel;

namespace LabaDSV
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = new MainView();

            mainWindow.DataContext = new MainViewModel(new RelayCommandFactory(), new WindowService());

            mainWindow.Show();
        }
    }
}
