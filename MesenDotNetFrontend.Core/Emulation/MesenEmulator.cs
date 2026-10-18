using MesenDotNetFrontend.Core.Interop;

namespace MesenDotNetFrontend.Core.Emulation;

public sealed class MesenEmulator : IDisposable
{
    private bool _initialized;

    public bool IsRunning => _initialized && MesenNative.IsRunning();

    public void Initialize()
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
            IntPtr.Zero,
            IntPtr.Zero,

            useSoftwareRenderer: true,

            noAudio: true,
            noVideo: false,
            noInput: true);

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
        MesenNative.Release();

        _initialized = false;
    }
}