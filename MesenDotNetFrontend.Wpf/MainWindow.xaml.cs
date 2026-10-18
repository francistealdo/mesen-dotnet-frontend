using Microsoft.Win32;
using MesenDotNetFrontend.Core.Emulation;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Interop;

namespace MesenDotNetFrontend.Wpf;

public partial class MainWindow : Window
{
    private readonly MesenEmulator _emulator = new();
    private WriteableBitmap? _bitmap;

    public MainWindow()
    {
        InitializeComponent();

        SourceInitialized += MainWindow_SourceInitialized;
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

    private void MainWindow_SourceInitialized(
    object? sender,
    EventArgs e)
    {
        try
        {
            var helper =
                new WindowInteropHelper(this);

            var handle = helper.Handle;

            _emulator.Initialize(
                handle,
                handle);

            _emulator.FrameReady += OnFrameReady;

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

    private void OnFrameReady(
    byte[] frame,
    int width,
    int height)
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

            NoRomText.Visibility =
                Visibility.Collapsed;
        });
    }

    private void RendererContainer_SizeChanged(
    object sender,
    SizeChangedEventArgs e)
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

    protected override void OnClosed(EventArgs e)
    {
        _emulator.Dispose();

        base.OnClosed(e);
    }
}