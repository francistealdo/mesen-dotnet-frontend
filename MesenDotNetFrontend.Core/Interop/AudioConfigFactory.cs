namespace MesenDotNetFrontend.Core.Interop;

public static class AudioConfigFactory
{
    public static InteropAudioConfig CreateDefault()
    {
        return new InteropAudioConfig
        {
            AudioDevice = string.Empty,

            EnableAudio = true,
            DisableDynamicSampleRate = false,

            MasterVolume = 100,
            SampleRate = 48000,
            AudioLatency = 60,

            MuteSoundInBackground = false,
            ReduceSoundInBackground = true,
            ReduceSoundInFastForward = false,
            VolumeReduction = 75,

            ReverbEnabled = false,
            ReverbStrength = 5,
            ReverbDelay = 10,

            CrossFeedEnabled = false,
            CrossFeedRatio = 0,

            EnableEqualizer = false,

            Band1Gain = 0,
            Band2Gain = 0,
            Band3Gain = 0,
            Band4Gain = 0,
            Band5Gain = 0,
            Band6Gain = 0,
            Band7Gain = 0,
            Band8Gain = 0,
            Band9Gain = 0,
            Band10Gain = 0,
            Band11Gain = 0,
            Band12Gain = 0,
            Band13Gain = 0,
            Band14Gain = 0,
            Band15Gain = 0,
            Band16Gain = 0,
            Band17Gain = 0,
            Band18Gain = 0,
            Band19Gain = 0,
            Band20Gain = 0,

            AudioPlayerEnableTrackLength = true,
            AudioPlayerTrackLength = 120,
            AudioPlayerAutoDetectSilence = true,
            AudioPlayerSilenceDelay = 3
        };
    }
}