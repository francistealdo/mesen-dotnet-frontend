using System.Runtime.InteropServices;

namespace MesenDotNetFrontend.Core.Interop;

[StructLayout(LayoutKind.Sequential)]
public struct InteropAudioConfig
{
    [MarshalAs(UnmanagedType.LPStr)]
    public string AudioDevice;

    [MarshalAs(UnmanagedType.I1)]
    public bool EnableAudio;

    [MarshalAs(UnmanagedType.I1)]
    public bool DisableDynamicSampleRate;

    public uint MasterVolume;
    public uint SampleRate;
    public uint AudioLatency;

    [MarshalAs(UnmanagedType.I1)]
    public bool MuteSoundInBackground;

    [MarshalAs(UnmanagedType.I1)]
    public bool ReduceSoundInBackground;

    [MarshalAs(UnmanagedType.I1)]
    public bool ReduceSoundInFastForward;

    public int VolumeReduction;

    [MarshalAs(UnmanagedType.I1)]
    public bool ReverbEnabled;

    public uint ReverbStrength;
    public uint ReverbDelay;

    [MarshalAs(UnmanagedType.I1)]
    public bool CrossFeedEnabled;

    public uint CrossFeedRatio;

    [MarshalAs(UnmanagedType.I1)]
    public bool EnableEqualizer;

    public double Band1Gain;
    public double Band2Gain;
    public double Band3Gain;
    public double Band4Gain;
    public double Band5Gain;
    public double Band6Gain;
    public double Band7Gain;
    public double Band8Gain;
    public double Band9Gain;
    public double Band10Gain;
    public double Band11Gain;
    public double Band12Gain;
    public double Band13Gain;
    public double Band14Gain;
    public double Band15Gain;
    public double Band16Gain;
    public double Band17Gain;
    public double Band18Gain;
    public double Band19Gain;
    public double Band20Gain;

    [MarshalAs(UnmanagedType.I1)]
    public bool AudioPlayerEnableTrackLength;

    public uint AudioPlayerTrackLength;

    [MarshalAs(UnmanagedType.I1)]
    public bool AudioPlayerAutoDetectSilence;

    public uint AudioPlayerSilenceDelay;
}