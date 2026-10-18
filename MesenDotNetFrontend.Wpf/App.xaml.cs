using MesenDotNetFrontend.Infrastructure.Persistence;
using System.Windows;

namespace MesenDotNetFrontend.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(
        StartupEventArgs e)
        {
            AppDatabase.Initialize();

            AppLogRepository.Write(category: "System", description: "Application started");

            base.OnStartup(e);
        }
    }
}
