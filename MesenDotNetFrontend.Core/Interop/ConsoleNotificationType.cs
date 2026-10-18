namespace MesenDotNetFrontend.Core.Interop;

public enum ConsoleNotificationType
{
    GameLoaded,
    StateLoaded,
    GameReset,
    GamePaused,
    GameResumed,
    CodeBreak,
    DebuggerResumed,
    PpuFrameDone,
    ResolutionChanged,
    ExecuteShortcut,
    ReleaseShortcut,
    EmulationStopped,
    BeforeEmulationStop,
    ViewerRefresh,
    EventViewerRefresh,
    MissingFirmware,
    SufamiTurboFilePrompt,
    BeforeGameUnload,
    BeforeGameLoad,
    GameLoadFailed,
    CheatsChanged,
    RequestConfigChange,
    RefreshSoftwareRenderer,
    AfterInitConsole,
    NetplayStopped
}