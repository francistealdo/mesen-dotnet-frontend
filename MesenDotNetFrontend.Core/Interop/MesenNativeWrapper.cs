namespace MesenDotNetFrontend.Core.Interop
{
    public static class MesenNativeWrapper
    {
        public static byte[]? GetSaveStatePreview(string saveStatePath)
        {
            if (!File.Exists(saveStatePath))
            {
                return null;
            }

            var buffer = new byte[512 * 478 * 4];

            var size = MesenNative.GetSaveStatePreviewNative(saveStatePath, buffer);

            if (size <= 0)
            {
                return null;
            }

            Array.Resize(
                ref buffer,
                size);

            return buffer;
        }
    }
}
