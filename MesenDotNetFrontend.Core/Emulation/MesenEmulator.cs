using MesenDotNetFrontend.Core.Interop;
using System.Runtime.InteropServices;

namespace MesenDotNetFrontend.Core.Emulation;

public sealed class MesenEmulator : IDisposable
{
    private bool _initialized;
    private MesenNative.NotificationCallback? _notificationCallback;
    private IntPtr _notificationListener;

    public bool IsRunning => _initialized && MesenNative.IsRunning();

    public event Action<byte[], int, int>? FrameReady;
    public event Action? EmulationStopped;

    public void Initialize(
    IntPtr windowHandle,
    IntPtr viewerHandle)
    {
        if (_initialized)
        {
            return;
        }

        var homeFolder = Path.Combine(
            AppContext.BaseDirectory,
            "MesenData");

        Directory.CreateDirectory(homeFolder);

        MesenNative.InitDll();

        MesenNative.InitializeEmu(
    homeFolder,
    windowHandle,
    viewerHandle,
    useSoftwareRenderer: true,
    noAudio: false,
    noVideo: false,
    noInput: false);

        var audioConfig =
    AudioConfigFactory.CreateDefault();

        MesenNative.SetAudioConfig(
            audioConfig);

        var nesConfig =
    NesConfigFactory.CreateDefault();

        MesenNative.SetNesConfig(
            nesConfig);

        _notificationCallback = OnNotification;

        _notificationListener =
            MesenNative.RegisterNotificationCallback(
                _notificationCallback);

        _initialized = true;
    }

    public bool LoadRom(string path)
    {
        if (!_initialized)
        {
            throw new InvalidOperationException(
                "The emulator has not been initialized.");
        }

        if (!File.Exists(path))
        {
            throw new FileNotFoundException(
                "ROM file was not found.",
                path);
        }

        MesenNative.Pause();

        return MesenNative.LoadRom(path);
    }

    public void Start()
    {
        EnsureInitialized();

        MesenNative.Resume();
    }

    public void Pause()
    {
        EnsureInitialized();

        MesenNative.Pause();
    }

    public void Stop()
    {
        if (!_initialized)
        {
            return;
        }

        MesenNative.Stop();
    }

    public void CloseRom()
    {
        if (!_initialized)
        {
            return;
        }

        MesenNative.Stop();
    }

    public void SetRendererSize(
    uint width,
    uint height)
    {
        if (!_initialized)
        {
            return;
        }

        MesenNative.SetRendererSize(width, height);
    }

    private void EnsureInitialized()
    {
        if (!_initialized)
        {
            throw new InvalidOperationException(
                "The emulator has not been initialized.");
        }
    }

    public void Dispose()
    {
        if (!_initialized)
        {
            return;
        }

        MesenNative.Stop();

        if (_notificationListener != IntPtr.Zero)
        {
            MesenNative.UnregisterNotificationCallback(
                _notificationListener);

            _notificationCallback = null;

            _notificationListener = IntPtr.Zero;
        }

        MesenNative.Release();

        _initialized = false;
    }

    private void OnNotification(int type, IntPtr parameter)
    {
        var notificationType =
        (ConsoleNotificationType)type;

        if (notificationType ==
            ConsoleNotificationType.EmulationStopped)
        {
            EmulationStopped?.Invoke();
            return;
        }

        if (notificationType !=
            ConsoleNotificationType.RefreshSoftwareRenderer)
        {
            return;
        }

        if (parameter == IntPtr.Zero)
        {
            return;
        }

        var frameInfo =
            Marshal.PtrToStructure<SoftwareRendererFrame>(
                parameter);

        var frame = frameInfo.Frame;

        if (frame.FrameBuffer == IntPtr.Zero ||
            frame.Width == 0 ||
            frame.Height == 0)
        {
            return;
        }

        var width = checked((int)frame.Width);
        var height = checked((int)frame.Height);

        var size =
            checked(width * height * 4);

        var buffer = new byte[size];

        Marshal.Copy(
            frame.FrameBuffer,
            buffer,
            0,
            size);

        FrameReady?.Invoke(
            buffer,
            width,
            height);
    }

    public void TakeScreenshot()
    {
        EnsureInitialized();
        MesenNative.TakeScreenshot();
    }

    public (uint Width, uint Height) GetBaseScreenSize()
    {
        EnsureInitialized();

        var size = MesenNative.GetBaseScreenSize();

        return (size.Width, size.Height);
    }

    public bool IsPaused => _initialized && MesenNative.IsPaused();

    public void Reset()
    {
        EnsureInitialized();

        MesenNative.ExecuteShortcut(
            new MesenNative.ExecuteShortcutParams
            {
                Shortcut =
                    MesenNative.EmulatorShortcut.ExecReset,

                Param = 0,
                ParamPtr = IntPtr.Zero
            });
    }
}