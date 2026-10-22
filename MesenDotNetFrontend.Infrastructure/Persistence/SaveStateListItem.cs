namespace MesenDotNetFrontend.Infrastructure.Persistence
{
    public sealed class SaveStateListItem
    {
        public long Id { get; set; }

        public string RomName { get; set; } = string.Empty;

        public string? Name { get; set; }

        public byte[]? ScreenshotData { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
