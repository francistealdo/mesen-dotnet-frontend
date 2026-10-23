using MesenDotNetFrontend.Core.Models;
using MesenDotNetFrontend.Infrastructure.Persistence;

namespace MesenDotNetFrontend.Wpf.Input;

public static class InputSettingsLoader
{
    public static NesInputSettings Load()
    {
        var values = InputSettingsRepository.GetAll();

        return new NesInputSettings
        {
            Up = GetValue(
                values,
                "Up",
                "Up Arrow"),

            Down = GetValue(
                values,
                "Down",
                "Down Arrow"),

            Left = GetValue(
                values,
                "Left",
                "Left Arrow"),

            Right = GetValue(
                values,
                "Right",
                "Right Arrow"),

            A = GetValue(
                values,
                "A",
                "S"),

            B = GetValue(
                values,
                "B",
                "A"),

            TurboA = GetValue(
                values,
                "TurboA",
                "D"),

            TurboB = GetValue(
                values,
                "TurboB",
                "F"),

            Start = GetValue(
                values,
                "Start",
                "W"),

            Select = GetValue(
                values,
                "Select",
                "Q"),

            FastForward = GetValue(
                values,
                "FastForward",
                "Tab")
        };
    }

    private static string GetValue(
        Dictionary<string, string> values,
        string action,
        string defaultValue)
    {
        return values.TryGetValue(
            action,
            out var value)
            ? value
            : defaultValue;
    }
}