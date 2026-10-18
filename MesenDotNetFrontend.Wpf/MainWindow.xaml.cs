using MesenDotNetFrontend.Core.Emulation;
using MesenDotNetFrontend.Infrastructure.Persistence;
using MesenDotNetFrontend.Wpf.Input;
using MesenDotNetFrontend.Wpf.Settings;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MesenDotNetFrontend.Wpf;

public partial class MainWindow : Window
{
    private readonly MesenEmulator _emulator = new();
    private WriteableBitmap? _bitmap;
    private readonly NesKeyboardInput _keyboardInput = new();

    public MainWindow()
    {
        InitializeComponent();

        SourceInitialized += MainWindow_SourceInitialized;

        PreviewKeyDown += MainWindow_PreviewKeyDown;
        PreviewKeyUp += MainWindow_PreviewKeyUp;
    }

    #region private methods

    private void MainWindow_SourceInitialized(object? sender, EventArgs e)
    {
        try
        {
            var helper = new WindowInteropHelper(this);

            var handle = helper.Handle;

            var inputSettings = InputSettingsLoader.Load();

            _emulator.Initialize(handle, handle, inputSettings);

            _emulator.FrameReady += OnFrameReady;

            _emulator.EmulationStopped += OnEmulationStopped;

            _emulator.SetRendererSize(
                (uint)Math.Max(
                    1,
                    RendererContainer.ActualWidth),
                (uint)Math.Max(
                    1,
                    RendererContainer.ActualHeight));
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

    private void OnFrameReady(byte[] frame, int width, int height)
    {
        Dispatcher.BeginInvoke(() =>
        {
            if (_bitmap == null ||
                _bitmap.PixelWidth != width ||
                _bitmap.PixelHeight != height)
            {
                _bitmap = new WriteableBitmap(
                    width,
                    height,
                    96,
                    96,
                    PixelFormats.Bgra32,
                    null);

                EmulatorScreen.Source = _bitmap;
            }

            var stride = width * 4;

            _bitmap.WritePixels(
                new Int32Rect(
                    0,
                    0,
                    width,
                    height),
                frame,
                stride,
                0);

            NoRomText.Visibility = Visibility.Collapsed;
        });
    }

    private void RendererContainer_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (e.NewSize.Width <= 0 ||
            e.NewSize.Height <= 0)
        {
            return;
        }

        _emulator.SetRendererSize(
            (uint)Math.Round(e.NewSize.Width),
            (uint)Math.Round(e.NewSize.Height));
    }

    private void OnEmulationStopped()
    {
        Dispatcher.BeginInvoke(() =>
        {
            _bitmap = null;

            EmulatorScreen.Source = null;

            NoRomText.Visibility = Visibility.Visible;

            PauseResumeMenuItem.Header = "_Pause";

            _keyboardInput.Reset();
        });
    }

    #endregion

    #region private methods OnClick

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
            if (!_emulator.LoadRom(dialog.FileName))
            {
                MessageBox.Show(
                    "Mesen was unable to load the ROM.",
                    "Load ROM",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                return;
            }

            _emulator.Start();

            PauseResumeMenuItem.Header = "_Pause";

            AppLogRepository.Write(rom: _emulator.CurrentRomName, category: "Rom", description: "Open ROM");
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

    private void CloseRom_Click(object sender, RoutedEventArgs e)
    {
        if (!_emulator.IsRunning)
        {
            return;
        }

        _emulator.CloseRom();

        AppLogRepository.Write(rom: _emulator.CurrentRomName, category: "Rom", description: "Close ROM");
    }

    private void PauseResume_Click(object sender, RoutedEventArgs e)
    {
        if (!_emulator.IsRunning)
        {
            return;
        }

        if (_emulator.IsPaused)
        {
            _emulator.Start();

            PauseResumeMenuItem.Header =
                "_Pause";

            AppLogRepository.Write(rom: _emulator.CurrentRomName, category: "Emulation", description: "Resume");
        }
        else
        {
            _emulator.Pause();

            PauseResumeMenuItem.Header =
                "_Resume";

            AppLogRepository.Write(rom: _emulator.CurrentRomName, category: "Emulation", description: "Pause");
        }
    }

    private void Reset_Click(object sender, RoutedEventArgs e)
    {
        if (!_emulator.IsRunning)
        {
            return;
        }

        _emulator.Reset();

        AppLogRepository.Write(rom: _emulator.CurrentRomName, category: "Rom", description: "Reset");
    }

    private void Exit_Click(object sender, RoutedEventArgs e)
    {
        AppLogRepository.Write(category: "System", description: "Exit");

        Close();
    }

    private void ControlsSettings_Click(object sender, RoutedEventArgs e)
    {
        var window = new ControlsSettingsWindow
            {
                Owner = this
            };

        window.SettingsSaved += () =>
        {
            var inputSettings = InputSettingsLoader.Load();

            _emulator.ApplyInputSettings(inputSettings);
        };

        window.ShowDialog();
    }

    #endregion

    #region private methods PreviewKey

    private void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        _keyboardInput.KeyDown(e.Key);
    }

    private void MainWindow_PreviewKeyUp(object sender, KeyEventArgs e)
    {
        _keyboardInput.KeyUp(e.Key);
    }

    #endregion

    #region protected methods

    protected override void OnDeactivated(EventArgs e)
    {
        _keyboardInput.Reset();

        base.OnDeactivated(e);
    }

    protected override void OnClosed(EventArgs e)
    {
        _emulator.FrameReady -= OnFrameReady;
        _emulator.EmulationStopped -= OnEmulationStopped;

        _emulator.Dispose();

        base.OnClosed(e);
    }

    #endregion
}