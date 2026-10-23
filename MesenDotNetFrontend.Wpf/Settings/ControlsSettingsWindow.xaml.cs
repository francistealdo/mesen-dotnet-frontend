using MesenDotNetFrontend.Infrastructure.Persistence;
using MesenDotNetFrontend.Wpf.Input;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MesenDotNetFrontend.Wpf.Settings;

public partial class ControlsSettingsWindow : Window
{
    private readonly Dictionary<string, string> _settings;
    private Button? _waitingButton;
    public event Action? SettingsSaved;

    public ControlsSettingsWindow()
    {
        InitializeComponent();

        _settings = InputSettingsRepository.GetAll();

        LoadSettings();

        PreviewKeyDown += ControlsSettingsWindow_PreviewKeyDown;
    }

    private void LoadSettings()
    {
        SetButtonValue(UpButton, "Up");

        SetButtonValue(DownButton, "Down");

        SetButtonValue(LeftButton, "Left");

        SetButtonValue(RightButton, "Right");

        SetButtonValue(AButton, "A");

        SetButtonValue(BButton, "B");

        SetButtonValue(TurboAButton, "TurboA");

        SetButtonValue(TurboBButton, "TurboB");

        SetButtonValue(StartButton, "Start");

        SetButtonValue(SelectButton, "Select");

        SetButtonValue(FastForwardButton, "FastForward");
    }

    private void SetButtonValue(Button button, string action)
    {
        if (_settings.TryGetValue(
            action,
            out var key))
        {
            button.Content = key;
        }
        else
        {
            button.Content = "-";
        }
    }

    private void KeyButton_Click(object sender, RoutedEventArgs e)
    {
        if (sender is not Button button)
        {
            return;
        }

        _waitingButton = button;

        button.Content = "Press a key...";
    }

    private void ControlsSettingsWindow_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (_waitingButton == null)
        {
            return;
        }

        var key = e.Key == Key.System
                ? e.SystemKey
                : e.Key;

        var keyName = MesenKeyNameMapper.FromWpfKey(key);

        if (keyName == null)
        {
            return;
        }

        var action = _waitingButton.Tag?.ToString();

        if (string.IsNullOrWhiteSpace(action))
        {
            return;
        }

        _settings[action] = keyName;

        _waitingButton.Content = keyName;

        _waitingButton = null;

        e.Handled = true;
    }

    private void Save_Click(object sender, RoutedEventArgs e)
    {
        InputSettingsRepository.SaveAll(_settings);

        SettingsSaved?.Invoke();

        DialogResult = true;

        Close();
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
        DialogResult = false;

        Close();
    }
}