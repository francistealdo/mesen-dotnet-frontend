using System.Runtime.InteropServices;

namespace MesenDotNetFrontend.Core.Interop;

public static class MesenInputNative
{
    private const string DllName = "MesenCore.dll";

    [DllImport(DllName)]
    public static extern void SetKeyState(
        ushort scanCode,
        [MarshalAs(UnmanagedType.I1)] bool pressed);

    [DllImport(DllName)]
    public static extern void ResetKeyState();

    [DllImport(DllName)]
    public static extern ushort GetKeyCode(
        [MarshalAs(UnmanagedType.LPUTF8Str)]
        string keyName);
}