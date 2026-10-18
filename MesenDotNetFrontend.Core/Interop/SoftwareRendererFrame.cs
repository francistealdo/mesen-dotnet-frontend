using System.Runtime.InteropServices;

namespace MesenDotNetFrontend.Core.Interop;

[StructLayout(LayoutKind.Sequential)]
public struct SoftwareRendererSurface
{
    public IntPtr FrameBuffer;
    public uint Width;
    public uint Height;

    [MarshalAs(UnmanagedType.I1)]
    public bool IsDirty;
}

[StructLayout(LayoutKind.Sequential)]
public struct SoftwareRendererFrame
{
    public SoftwareRendererSurface Frame;
    public SoftwareRendererSurface EmuHud;
    public SoftwareRendererSurface ScriptHud;
}