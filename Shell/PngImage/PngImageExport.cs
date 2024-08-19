using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using Core;
using Tooling;
using static System.Drawing.Imaging.ImageFormat;
using Image = Core.Image;
// ReSharper disable InconsistentNaming

namespace PngImage;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
public static class PngImageExport
{
    public static void ExportImageToPngFile(Image image, string path) =>
        CreateEmptyBitmap(image)
            .Then(BuildBitmapFrom(image))
            .Finally(SaveBitmapToPngAt(path));

    private static Func<Bitmap, Bitmap> BuildBitmapFrom(Image image) =>
        bitmap => TransferPixelsFromImageToBitMap(image, bitmap);

    private static Action<Bitmap> SaveBitmapToPngAt(string path) =>
        bitmap => bitmap.Save(path, Png);

    private static Bitmap CreateEmptyBitmap(Image image) =>
        new(image.Width, image.Height);

    private static Bitmap TransferPixelsFromImageToBitMap(Image image, Bitmap bmp)
    {
        GenerateXYCoordinateTuples(image)
            .Select(AddColorToTuple(image))
            .ForEach(SetColorOnBitmap(bmp));
        return bmp;
    }

    private static Action<(int X, int Y, Color Color)> SetColorOnBitmap(Bitmap bmp)
        => xyColorTuple => bmp.SetPixel(xyColorTuple.X, xyColorTuple.Y, xyColorTuple.Color);

    private static Func<(int X, int Y), (int X, int Y, Color Color)> AddColorToTuple(Image image) =>
        xyTuple => (xyTuple.X, xyTuple.Y, Color: PixelToColor(image[xyTuple.X, xyTuple.Y]));

    private static IEnumerable<(int X, int Y)> GenerateXYCoordinateTuples(Image image)
        => Enumerable.Range(0, image.Width)
            .SelectMany(x => Enumerable.Range(0, image.Height)
                .Select(y => (X: x, Y: y))
            );

    private static Color PixelToColor(Pixel pixel) =>
        Color.FromArgb(
            (int)(pixel.R * 255.999f),
            (int)(pixel.G * 255.999f),
            (int)(pixel.B * 255.999f)
        );
}