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

    public delegate void NotificationCallback(int type, IntPtr parameter);

    [DllImport(DllName)]
    public static extern IntPtr RegisterNotificationCallback(
        NotificationCallback callback);

    [DllImport(DllName)]
    public static extern void UnregisterNotificationCallback(
        IntPtr notificationListener);

    [DllImport(DllName)]
    public static extern void SetRendererSize(
    uint width,
    uint height);

    [DllImport(DllName)]
    public static extern void TakeScreenshot();

    [StructLayout(LayoutKind.Sequential)]
    public struct FrameInfo
    {
        public uint Width;
        public uint Height;
    }

    [DllImport(DllName)]
    public static extern FrameInfo GetBaseScreenSize();

    [DllImport(DllName)]
    public static extern void SetNesConfig(
    InteropNesConfig config);

    [DllImport(DllName)]
    public static extern void SetAudioConfig(
    InteropAudioConfig config);

    [StructLayout(LayoutKind.Sequential)]
    public struct ExecuteShortcutParams
    {
        public EmulatorShortcut Shortcut;
        public uint Param;
        public IntPtr ParamPtr;
    }

    public enum EmulatorShortcut
    {
        FastForward,
        Rewind,
        RewindTenSecs,
        RewindOneMin,

        SelectSaveSlot1,
        SelectSaveSlot2,
        SelectSaveSlot3,
        SelectSaveSlot4,
        SelectSaveSlot5,
        SelectSaveSlot6,
        SelectSaveSlot7,
        SelectSaveSlot8,
        SelectSaveSlot9,
        SelectSaveSlot10,
        MoveToNextStateSlot,
        MoveToPreviousStateSlot,
        SaveState,
        LoadState,

        ToggleCheats,
        ToggleFastForward,
        ToggleRewind,

        RunSingleFrame,

        TakeScreenshot,

        ToggleRecordVideo,
        ToggleRecordAudio,
        ToggleRecordMovie,

        IncreaseSpeed,
        DecreaseSpeed,
        MaxSpeed,

        Pause,
        Reset,
        PowerCycle,
        ReloadRom,
        PowerOff,
        Exit,

        ExecReset,
        ExecPowerCycle,
        ExecReloadRom,
        ExecPowerOff
    }

    [DllImport(DllName)]
    public static extern void ExecuteShortcut(
    ExecuteShortcutParams p);
}