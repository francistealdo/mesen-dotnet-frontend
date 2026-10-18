using System.Runtime.InteropServices;

namespace MesenDotNetFrontend.Core.Interop;

public enum ConsoleRegion
{
    Auto = 0,
    Ntsc = 1,
    Pal = 2,
    Dendy = 3,
    NtscJapan = 4
}

public enum RamState
{
    Random = 0,
    AllZeros = 1,
    AllOnes = 2
}

public enum NesConsoleType
{
    Nes001 = 0,
    Nes101 = 1,
    Hvc001 = 2,
    Hvc101 = 3
}

public enum VsDualOutputOption
{
    Both = 0,
    MainSystemOnly = 1,
    SubSystemOnly = 2
}

public enum StereoFilter
{
    None = 0,
    Delay = 1,
    Panning = 2,
    CombFilter = 3
}

public enum ControllerType
{
    None = 0,
    NesController = 9
}

[StructLayout(LayoutKind.Sequential)]
public struct InteropOverscanDimensions
{
    public uint Left;
    public uint Right;
    public uint Top;
    public uint Bottom;
}

[StructLayout(LayoutKind.Sequential)]
public struct InteropKeyMapping
{
    public ushort A;
    public ushort B;
    public ushort X;
    public ushort Y;
    public ushort L;
    public ushort R;

    public ushort Up;
    public ushort Down;
    public ushort Left;
    public ushort Right;

    public ushort Start;
    public ushort Select;

    public ushort U;
    public ushort D;

    public ushort TurboA;
    public ushort TurboB;
    public ushort TurboX;
    public ushort TurboY;
    public ushort TurboL;
    public ushort TurboR;
    public ushort TurboSelect;
    public ushort TurboStart;

    public ushort GenericKey1;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
    public ushort[] CustomKeys;
}

[StructLayout(LayoutKind.Sequential)]
public struct InteropKeyMappingSet
{
    public InteropKeyMapping Mapping1;
    public InteropKeyMapping Mapping2;
    public InteropKeyMapping Mapping3;
    public InteropKeyMapping Mapping4;

    public uint TurboSpeed;
}

[StructLayout(LayoutKind.Sequential)]
public struct InteropControllerConfig
{
    public InteropKeyMappingSet Keys;
    public ControllerType Type;
}

[StructLayout(LayoutKind.Sequential)]
public struct InteropNesConfig
{
    public InteropControllerConfig Port1;
    public InteropControllerConfig Port2;
    public InteropControllerConfig ExpPort;

    public InteropControllerConfig Port1A;
    public InteropControllerConfig Port1B;
    public InteropControllerConfig Port1C;
    public InteropControllerConfig Port1D;

    public InteropControllerConfig ExpPortA;
    public InteropControllerConfig ExpPortB;
    public InteropControllerConfig ExpPortC;
    public InteropControllerConfig ExpPortD;

    public InteropControllerConfig MapperInput;

    public uint LightDetectionRadius;

    [MarshalAs(UnmanagedType.I1)]
    public bool AutoConfigureInput;

    public ConsoleRegion Region;

    [MarshalAs(UnmanagedType.I1)]
    public bool EnableHdPacks;

    [MarshalAs(UnmanagedType.I1)]
    public bool DisableGameDatabase;

    [MarshalAs(UnmanagedType.I1)]
    public bool FdsAutoLoadDisk;

    [MarshalAs(UnmanagedType.I1)]
    public bool FdsFastForwardOnLoad;

    [MarshalAs(UnmanagedType.I1)]
    public bool FdsAutoInsertDisk;

    public VsDualOutputOption VsDualVideoOutput;
    public VsDualOutputOption VsDualAudioOutput;

    [MarshalAs(UnmanagedType.I1)]
    public bool SpritesEnabled;

    [MarshalAs(UnmanagedType.I1)]
    public bool BackgroundEnabled;

    [MarshalAs(UnmanagedType.I1)]
    public bool ForceBackgroundFirstColumn;

    [MarshalAs(UnmanagedType.I1)]
    public bool ForceSpritesFirstColumn;

    [MarshalAs(UnmanagedType.I1)]
    public bool RemoveSpriteLimit;

    [MarshalAs(UnmanagedType.I1)]
    public bool AdaptiveSpriteLimit;

    [MarshalAs(UnmanagedType.I1)]
    public bool EnablePalBorders;

    [MarshalAs(UnmanagedType.I1)]
    public bool UseCustomVsPalette;

    public InteropOverscanDimensions NtscOverscan;
    public InteropOverscanDimensions PalOverscan;

    public NesConsoleType ConsoleType;

    [MarshalAs(UnmanagedType.I1)]
    public bool DisablePpuReset;

    [MarshalAs(UnmanagedType.I1)]
    public bool AllowInvalidInput;

    [MarshalAs(UnmanagedType.I1)]
    public bool DisableGameGenieBusConflicts;

    [MarshalAs(UnmanagedType.I1)]
    public bool DisableFlashSaves;

    [MarshalAs(UnmanagedType.I1)]
    public bool OverwriteOriginalRom;

    [MarshalAs(UnmanagedType.I1)]
    public bool EnableOamDecay;

    [MarshalAs(UnmanagedType.I1)]
    public bool EnablePpuOamRowCorruption;

    [MarshalAs(UnmanagedType.I1)]
    public bool EnablePpuSpriteEvalBug;

    [MarshalAs(UnmanagedType.I1)]
    public bool DisableOamAddrBug;

    [MarshalAs(UnmanagedType.I1)]
    public bool DisablePaletteRead;

    [MarshalAs(UnmanagedType.I1)]
    public bool DisablePpu2004Reads;

    [MarshalAs(UnmanagedType.I1)]
    public bool EnablePpu2000ScrollGlitch;

    [MarshalAs(UnmanagedType.I1)]
    public bool EnablePpu2006ScrollGlitch;

    [MarshalAs(UnmanagedType.I1)]
    public bool RestrictPpuAccessOnFirstFrame;

    [MarshalAs(UnmanagedType.I1)]
    public bool EnableDmcSampleDuplicationGlitch;

    [MarshalAs(UnmanagedType.I1)]
    public bool EnableCpuTestMode;

    [MarshalAs(UnmanagedType.I1)]
    public bool RandomizeMapperPowerOnState;

    [MarshalAs(UnmanagedType.I1)]
    public bool RandomizeCpuPpuAlignment;

    public RamState RamPowerOnState;

    public uint PpuExtraScanlinesBeforeNmi;
    public uint PpuExtraScanlinesAfterNmi;

    [MarshalAs(UnmanagedType.I1)]
    public bool DisableNoiseModeFlag;

    [MarshalAs(UnmanagedType.I1)]
    public bool ReduceDmcPopping;

    [MarshalAs(UnmanagedType.I1)]
    public bool SilenceTriangleHighFreq;

    [MarshalAs(UnmanagedType.I1)]
    public bool SwapDutyCycles;

    [MarshalAs(UnmanagedType.I1)]
    public bool ReverseDpcmBitOrder;

    [MarshalAs(UnmanagedType.I1)]
    public bool BreakOnCrash;

    public int InputScanline;

    [MarshalAs(UnmanagedType.I1)]
    public bool IsFullColorPalette;

    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
    public uint[] UserPalette;

    public uint Square1Volume;
    public uint Square2Volume;
    public uint TriangleVolume;
    public uint NoiseVolume;
    public uint DmcVolume;
    public uint FdsVolume;
    public uint Mmc5Volume;
    public uint Vrc6Volume;
    public uint Vrc7Volume;
    public uint Namco163Volume;
    public uint Sunsoft5bVolume;
    public uint EpsmVolume;

    public int Square1Panning;
    public int Square2Panning;
    public int TrianglePanning;
    public int NoisePanning;
    public int DmcPanning;
    public int FdsPanning;
    public int Mmc5Panning;
    public int Vrc6Panning;
    public int Vrc7Panning;
    public int Namco163Panning;
    public int Sunsoft5bPanning;

    public StereoFilter StereoFilter;

    public int StereoDelay;
    public int StereoPanningAngle;
    public int StereoCombFilterDelay;
    public int StereoCombFilterStrength;
}