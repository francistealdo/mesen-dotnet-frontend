using MesenDotNetFrontend.Core.Emulation;
using MesenDotNetFrontend.Core.Interop;
using MesenDotNetFrontend.Core.Models;
using System.Windows.Input;

namespace MesenDotNetFrontend.Wpf.Input;

public sealed class NesKeyboardInput
{
    private readonly MesenEmulator _emulator;
    private NesInputSettings _settings;

    public NesKeyboardInput(MesenEmulator emulator, NesInputSettings settings)
    {
        _emulator = emulator;
        _settings = settings;
    }

    public void UpdateSettings(NesInputSettings settings)
    {
        _settings = settings;
    }

    public void KeyDown(Key key)
    {
        var keyName = MesenKeyNameMapper.FromWpfKey(key);

        if (keyName == null)
        {
            return;
        }

        if (keyName == _settings.FastForward)
        {
            _emulator.SetFastForward(true);
            return;
        }

        SetKeyState(keyName, true);
    }

    public void KeyUp(Key key)
    {
        var keyName =
            MesenKeyNameMapper.FromWpfKey(key);

        if (keyName == null)
        {
            return;
        }

        if (keyName == _settings.FastForward)
        {
            _emulator.SetFastForward(false);
            return;
        }

        SetKeyState(keyName, false);
    }

    public void Reset()
    {
        _emulator.SetFastForward(false);
        MesenInputNative.ResetKeyState();
    }

    private static void SetKeyState(string keyName, bool pressed)
    {
        var keyCode = MesenInputNative.GetKeyCode(keyName);

        if (keyCode == 0)
        {
            return;
        }

        MesenInputNative.SetKeyState(keyCode, pressed);
    }
}