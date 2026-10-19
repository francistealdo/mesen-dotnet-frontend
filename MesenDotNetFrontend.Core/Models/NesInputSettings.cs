namespace MesenDotNetFrontend.Core.Models;

public sealed class NesInputSettings
{
    public string Up { get; set; } = "Up Arrow";
    public string Down { get; set; } = "Down Arrow";
    public string Left { get; set; } = "Left Arrow";
    public string Right { get; set; } = "Right Arrow";

    public string A { get; set; } = "S";
    public string B { get; set; } = "A";

    public string Start { get; set; } = "W";
    public string Select { get; set; } = "Q";
    public string FastForward { get; set; } = "Tab";
}