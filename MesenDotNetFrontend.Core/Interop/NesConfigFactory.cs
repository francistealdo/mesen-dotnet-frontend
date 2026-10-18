using MesenDotNetFrontend.Core.Models;

namespace MesenDotNetFrontend.Core.Interop;

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

    public static InteropNesConfig CreateDefault(NesInputSettings inputSettings)
    {
        var palette = new uint[512];

        Array.Copy(
            DefaultPalette,
            palette,
            DefaultPalette.Length);

        return new InteropNesConfig
        {
            Port1 = CreateNesController(inputSettings),
            Port2 = CreateController(),
            ExpPort = CreateController(),

            Port1A = CreateController(),
            Port1B = CreateController(),
            Port1C = CreateController(),
            Port1D = CreateController(),

            ExpPortA = CreateController(),
            ExpPortB = CreateController(),
            ExpPortC = CreateController(),
            ExpPortD = CreateController(),

            MapperInput = CreateController(),

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
    CreateController(
        ControllerType type = ControllerType.None)
    {
        return new InteropControllerConfig
        {
            Type = type,

            Keys = new InteropKeyMappingSet
            {
                Mapping1 = CreateEmptyMapping(),
                Mapping2 = CreateEmptyMapping(),
                Mapping3 = CreateEmptyMapping(),
                Mapping4 = CreateEmptyMapping(),
                TurboSpeed = 2
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

    private static InteropControllerConfig CreateNesController(
    NesInputSettings settings)
    {
        var mapping = CreateEmptyMapping();

        mapping.A =
            MesenInputNative.GetKeyCode(
                settings.A);

        mapping.B =
            MesenInputNative.GetKeyCode(
                settings.B);

        mapping.Up =
            MesenInputNative.GetKeyCode(
                settings.Up);

        mapping.Down =
            MesenInputNative.GetKeyCode(
                settings.Down);

        mapping.Left =
            MesenInputNative.GetKeyCode(
                settings.Left);

        mapping.Right =
            MesenInputNative.GetKeyCode(
                settings.Right);

        mapping.Select =
            MesenInputNative.GetKeyCode(
                settings.Select);

        mapping.Start =
            MesenInputNative.GetKeyCode(
                settings.Start);

        return new InteropControllerConfig
        {
            Type = ControllerType.NesController,

            Keys = new InteropKeyMappingSet
            {
                Mapping1 = mapping,
                Mapping2 = CreateEmptyMapping(),
                Mapping3 = CreateEmptyMapping(),
                Mapping4 = CreateEmptyMapping(),
                TurboSpeed = 2
            }
        };
    }
}