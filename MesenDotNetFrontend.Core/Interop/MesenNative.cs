using System.Runtime.InteropServices;

namespace MesenDotNetFrontend.Core.Interop;

public static class MesenNative
{
    private const string DllName = "MesenCore.dll";

    [DllImport(DllName)]
    public static extern void InitDll();

    [DllImport(DllName)]
    public static extern void InitializeEmu(
        [MarshalAs(UnmanagedType.LPUTF8Str)] string homeFolder,
        IntPtr windowHandle,
        IntPtr dxViewerHandle,
        [MarshalAs(UnmanagedType.I1)] bool useSoftwareRenderer,
        [MarshalAs(UnmanagedType.I1)] bool noAudio,
        [MarshalAs(UnmanagedType.I1)] bool noVideo,
        [MarshalAs(UnmanagedType.I1)] bool noInput);

    [DllImport(DllName)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool LoadRom(
        [MarshalAs(UnmanagedType.LPUTF8Str)] string filepath,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string? patchFile = null);

    [DllImport(DllName)]
    public static extern void Pause();

    [DllImport(DllName)]
    public static extern void Resume();

    [DllImport(DllName)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool IsRunning();

    [DllImport(DllName)]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool IsPaused();

    [DllImport(DllName)]
    public static extern void Stop();

    [DllImport(DllName)]
    public static extern void Release();
}