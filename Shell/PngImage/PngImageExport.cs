using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using Core;
using Tooling;
using static System.Drawing.Imaging.ImageFormat;
using Image = Core.Image;

namespace PngImage;

[SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
public static class PngImageExport
{
    public static void ExportImageToPngFile(Image image, string path) =>
        CreateEmptyBitmap(image)
            .Then(MapImageOntoBitmap(image))
            .Finally(SaveBitmapToPng(path));

    private static Func<Bitmap, Bitmap> MapImageOntoBitmap(Image image) =>
        bmp => TransferPixelsFromImageToBitMap(image, bmp);

    private static Action<Bitmap> SaveBitmapToPng(string path) =>
        bmp => bmp.Save(path, Png);

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
        => c => bmp.SetPixel(c.X, c.Y, c.Color);

    private static Func<(int X, int Y), (int X, int Y, Color Color)> AddColorToTuple(Image image) =>
        t => (t.X, t.Y, Color: PixelToColor(image[t.X, t.Y]));

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