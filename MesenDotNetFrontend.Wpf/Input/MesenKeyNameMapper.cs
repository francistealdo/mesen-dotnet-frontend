using System.Windows.Input;

namespace MesenDotNetFrontend.Wpf.Input;

public static class MesenKeyNameMapper
{
    public static string? FromWpfKey(Key key)
    {
        return key switch
        {
            Key.Up => "Up Arrow",
            Key.Down => "Down Arrow",
            Key.Left => "Left Arrow",
            Key.Right => "Right Arrow",

            Key.LeftShift => "Left Shift",
            Key.RightShift => "Right Shift",

            Key.LeftCtrl => "Left Ctrl",
            Key.RightCtrl => "Right Ctrl",

            Key.LeftAlt => "Left Alt",
            Key.RightAlt => "Right Alt",

            Key.Enter => "Enter",
            Key.Space => "Space",
            Key.Back => "Backspace",

            >= Key.A and <= Key.Z =>
                key.ToString(),

            >= Key.D0 and <= Key.D9 =>
                key.ToString()[1..],

            _ => null
        };
    }
}