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
    None = 0
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

public static class NesConfigFactory
{
    private static readonly uint[] DefaultPalette =
    [
        0xFF666666, 0xFF002A88, 0xFF1412A7, 0xFF3B00A4,
        0xFF5C007E, 0xFF6E0040, 0xFF6C0600, 0xFF561D00,
        0xFF333500, 0xFF0B4800, 0xFF005200, 0xFF004F08,
        0xFF00404D, 0xFF000000, 0xFF000000, 0xFF000000,

        0xFFADADAD, 0xFF155FD9, 0xFF4240FF, 0xFF7527FE,
        0xFFA01ACC, 0xFFB71E7B, 0xFFB53120, 0xFF994E00,
        0xFF6B6D00, 0xFF388700, 0xFF0C9300, 0xFF008F32,
        0xFF007C8D, 0xFF000000, 0xFF000000, 0xFF000000,

        0xFFFFFEFF, 0xFF64B0FF, 0xFF9290FF, 0xFFC676FF,
        0xFFF36AFF, 0xFFFE6ECC, 0xFFFE8170, 0xFFEA9E22,
        0xFFBCBE00, 0xFF88D800, 0xFF5CE430, 0xFF45E082,
        0xFF48CDDE, 0xFF4F4F4F, 0xFF000000, 0xFF000000,

        0xFFFFFEFF, 0xFFC0DFFF, 0xFFD3D2FF, 0xFFE8C8FF,
        0xFFFBC2FF, 0xFFFEC4EA, 0xFFFECCC5, 0xFFF7D8A5,
        0xFFE4E594, 0xFFCFEF96, 0xFFBDF4AB, 0xFFB3F3CC,
        0xFFB5EBF2, 0xFFB8B8B8, 0xFF000000, 0xFF000000
    ];

    public static InteropNesConfig CreateDefault()
    {
        var palette = new uint[512];

        Array.Copy(
            DefaultPalette,
            palette,
            DefaultPalette.Length);

        return new InteropNesConfig
        {
            Port1 = CreateEmptyController(),
            Port2 = CreateEmptyController(),
            ExpPort = CreateEmptyController(),

            Port1A = CreateEmptyController(),
            Port1B = CreateEmptyController(),
            Port1C = CreateEmptyController(),
            Port1D = CreateEmptyController(),

            ExpPortA = CreateEmptyController(),
            ExpPortB = CreateEmptyController(),
            ExpPortC = CreateEmptyController(),
            ExpPortD = CreateEmptyController(),

            MapperInput = CreateEmptyController(),

            AutoConfigureInput = true,

            Region = ConsoleRegion.Auto,

            EnableHdPacks = true,

            FdsAutoLoadDisk = true,

            VsDualVideoOutput =
                VsDualOutputOption.Both,

            VsDualAudioOutput =
                VsDualOutputOption.Both,

            SpritesEnabled = true,
            BackgroundEnabled = true,

            ConsoleType =
                NesConsoleType.Nes001,

            RamPowerOnState =
                RamState.AllZeros,

            InputScanline = 241,

            IsFullColorPalette = false,

            UserPalette = palette,

            Square1Volume = 100,
            Square2Volume = 100,
            TriangleVolume = 100,
            NoiseVolume = 100,
            DmcVolume = 100,
            FdsVolume = 100,
            Mmc5Volume = 100,
            Vrc6Volume = 100,
            Vrc7Volume = 100,
            Namco163Volume = 100,
            Sunsoft5bVolume = 100,
            EpsmVolume = 100,

            StereoFilter =
                StereoFilter.None,

            StereoDelay = 15,
            StereoPanningAngle = 15,
            StereoCombFilterDelay = 5,
            StereoCombFilterStrength = 100
        };
    }

    private static InteropControllerConfig
        CreateEmptyController()
    {
        return new InteropControllerConfig
        {
            Type = ControllerType.None,

            Keys = new InteropKeyMappingSet
            {
                Mapping1 = CreateEmptyMapping(),
                Mapping2 = CreateEmptyMapping(),
                Mapping3 = CreateEmptyMapping(),
                Mapping4 = CreateEmptyMapping()
            }
        };
    }

    private static InteropKeyMapping
        CreateEmptyMapping()
    {
        return new InteropKeyMapping
        {
            CustomKeys = new ushort[100]
        };
    }
}