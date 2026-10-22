using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace MesenDotNetFrontend.Wpf.SaveStates;

public partial class SaveStateWindow : Window
{
    public string SaveName => NameTextBox.Text.Trim();

    public SaveStateWindow(byte[] screenshotData, string defaultName)
    {
        InitializeComponent();

        NameTextBox.Text = defaultName;

        PreviewImage.Source = CreateBitmap(screenshotData);

        Loaded += (_, _) =>
        {
            NameTextBox.Focus();
            NameTextBox.SelectAll();
        };
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

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(NameTextBox.Text))
        {
            MessageBox.Show(
                "Enter a name for the save state.",
                "Save State",
                MessageBoxButton.OK,
                MessageBoxImage.Information);

            NameTextBox.Focus();

            return;
        }

        DialogResult = true;
    }    
}