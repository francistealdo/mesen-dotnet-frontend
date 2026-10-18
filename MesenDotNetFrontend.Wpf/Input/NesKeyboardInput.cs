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

    private static void SetKeyState(Key key, bool pressed)
    {
        var keyName = MesenKeyNameMapper.FromWpfKey(key);

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
}