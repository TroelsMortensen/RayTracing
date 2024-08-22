using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using Core;
using Tooling;
using static System.Drawing.Imaging.ImageFormat;
using Color = Core.Color;
using Image = Core.Image;

// ReSharper disable InconsistentNaming

namespace PngImage;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
public static class PngImageExport
{
    public static void ExportImageToPngFile(Image image, string path) =>
        CreateEmptyBitmapWithSize(image.Width, image.Height)
            .Then(ColourBitmapFrom(image))
            .Finally(SaveBitmapToPngAt(path));

    private static Func<Bitmap, Bitmap> ColourBitmapFrom(Image image) =>
        bitmap => TransferColorsFromImageToBitMap(image, bitmap);

    private static Action<Bitmap> SaveBitmapToPngAt(string path) =>
        bitmap => bitmap.Save(path, Png);

    private static Bitmap CreateEmptyBitmapWithSize(int width, int height) =>
        new(width, height);

    private static Bitmap TransferColorsFromImageToBitMap(Image image, Bitmap bmp)
    {
        GenerateXYCoordinateTuples(image)
            .Select(AddColorToTuple(image))
            .ForEach(SetColorOnBitmap(bmp));
        return bmp;
    }

    private static Action<(int X, int Y, System.Drawing.Color Color)> SetColorOnBitmap(Bitmap bitmap)
        => xyColorTuple => bitmap.SetPixel(xyColorTuple.X, xyColorTuple.Y, xyColorTuple.Color);

    private static Func<(int X, int Y), (int X, int Y, System.Drawing.Color Color)> AddColorToTuple(Image image) =>
        xyTuple => (xyTuple.X, xyTuple.Y, Color: ColorToBitmapColor(image[xyTuple.X, xyTuple.Y]));

    private static IEnumerable<(int X, int Y)> GenerateXYCoordinateTuples(Image image)
        => Enumerable.Range(0, image.Width)
            .SelectMany(x => Enumerable.Range(0, image.Height)
                .Select(y => (X: x, Y: y))
            );

    private static System.Drawing.Color ColorToBitmapColor(Color color) =>
        System.Drawing.Color.FromArgb(
            (int)(color.R * 255.999f),
            (int)(color.G * 255.999f),
            (int)(color.B * 255.999f)
        );
}