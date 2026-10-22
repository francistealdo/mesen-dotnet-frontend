using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Navigation;

namespace MesenDotNetFrontend.Wpf.About;

public partial class AboutWindow : Window
{
    public AboutWindow()
    {
        InitializeComponent();

        var version = Assembly
                .GetExecutingAssembly()
                .GetName()
                .Version;

        VersionText.Text =$"Version {version?.Major}.{version?.Minor}.{version?.Build}";
    }

    private void Ok_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }

    private void GitHubLink_RequestNavigate(object sender, RequestNavigateEventArgs e)
    {
        Process.Start(new ProcessStartInfo
            {
                FileName = e.Uri.AbsoluteUri,
                UseShellExecute = true
            });

        e.Handled = true;
    }
}