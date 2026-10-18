using MesenDotNetFrontend.Core.Interop;
using System.Windows.Input;

namespace MesenDotNetFrontend.Wpf.Input;

public sealed class NesKeyboardInput
{
    public void KeyDown(Key key)
    {
        SetKeyState(key, true);
    }

    public void KeyUp(Key key)
    {
        SetKeyState(key, false);
    }

    public void Reset()
    {
        MesenInputNative.ResetKeyState();
    }

    private static void SetKeyState(
        Key key,
        bool pressed)
    {
        var keyName = GetMesenKeyName(key);

        if (keyName == null)
        {
            return;
        }

        var keyCode =
            MesenInputNative.GetKeyCode(keyName);

        if (keyCode == 0)
        {
            return;
        }

        MesenInputNative.SetKeyState(
            keyCode,
            pressed);
    }

    private static string? GetMesenKeyName(
        Key key)
    {
        return key switch
        {
            Key.Up => "Up Arrow",
            Key.Down => "Down Arrow",
            Key.Left => "Left Arrow",
            Key.Right => "Right Arrow",

            Key.A => "A",
            Key.S => "S",
            Key.Q => "Q",
            Key.W => "W",

            _ => null
        };
    }
}