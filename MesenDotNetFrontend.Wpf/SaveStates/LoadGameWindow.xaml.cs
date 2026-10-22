using MesenDotNetFrontend.Infrastructure.Persistence;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MesenDotNetFrontend.Wpf.SaveStates;

public partial class LoadGameWindow : Window
{
    private readonly string _romName;

    public long? SelectedSaveStateId { get; private set; }

    public LoadGameWindow(string romName)
    {
        InitializeComponent();

        _romName = romName;

        LoadSaveStates();
    }

    private void LoadSaveStates()
    {
        var saves =
            SaveStateRepository
                .GetByRom(_romName)
                .Select(
                    x => new SaveStateViewModel
                    {
                        Id = x.Id,
                        RomName = x.RomName,
                        Name = string.IsNullOrWhiteSpace(x.Name) ? "(Unnamed)" : x.Name,
                        CreatedAtDisplay = x.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"),
                        Screenshot = x.ScreenshotData == null ? null : CreateBitmap(x.ScreenshotData)
                    })
                .ToList();

        SaveStatesList.ItemsSource = saves;
    }

    private static BitmapImage CreateBitmap(byte[] data)
    {
        using var stream = new MemoryStream(data);

        var bitmap = new BitmapImage();

        bitmap.BeginInit();

        bitmap.CacheOption = BitmapCacheOption.OnLoad;

        bitmap.StreamSource = stream;

        bitmap.EndInit();
        bitmap.Freeze();

        return bitmap;
    }

    private void Load_Click(object sender, RoutedEventArgs e)
    {
        LoadSelected();
    }

    private void LoadSelected()
    {
        if (SaveStatesList.SelectedItem is not SaveStateViewModel selected)
        {
            return;
        }

        SelectedSaveStateId = selected.Id;

        DialogResult = true;
    }

    private void SaveStatesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        LoadSelected();
    }

    private void Delete_Click(object sender, RoutedEventArgs e)
    {
        if (SaveStatesList.SelectedItem is not SaveStateViewModel selected)
        {
            return;
        }

        var result =
            MessageBox.Show(
                $"Delete \"{selected.Name}\"?",
                "Delete Save State",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

        if (result != MessageBoxResult.Yes)
        {
            return;
        }

        SaveStateRepository.Delete(selected.Id);

        LoadSaveStates();
    }
}

internal sealed class SaveStateViewModel
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string RomName { get; set; } = string.Empty;

    public string CreatedAtDisplay { get; set; } = string.Empty;

    public BitmapImage? Screenshot { get; set; }
}