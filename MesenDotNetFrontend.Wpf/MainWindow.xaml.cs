using Microsoft.Win32;
using MesenDotNetFrontend.Core.Emulation;
using System.Windows;

namespace MesenDotNetFrontend.Wpf;

public partial class MainWindow : Window
{
    private readonly MesenEmulator _emulator = new();

    public MainWindow()
    {
        InitializeComponent();

        try
        {
            _emulator.Initialize();
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.ToString(),
                "Mesen initialization error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    private void OpenRom_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog
        {
            Title = "Open NES ROM",
            Filter = "NES ROM (*.nes)|*.nes|All files (*.*)|*.*"
        };

        if (dialog.ShowDialog() != true)
        {
            return;
        }

        try
        {
            var loaded = _emulator.LoadRom(dialog.FileName);

            if (!loaded)
            {
                MessageBox.Show(
                    "Mesen was unable to load the ROM.",
                    "Load ROM",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                return;
            }

            _emulator.Start();

            MessageBox.Show(
                $"ROM loaded successfully.\n\n" +
                $"Running: {_emulator.IsRunning}",
                "Mesen",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                ex.ToString(),
                "ROM loading error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }

    protected override void OnClosed(EventArgs e)
    {
        _emulator.Dispose();

        base.OnClosed(e);
    }
}