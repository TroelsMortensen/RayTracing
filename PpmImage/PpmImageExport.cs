using System.Text;
using Core;
using static Tooling.BaseExtensions;

namespace PpmImage;

public static class PpmImageExport
{
    private const string MaxPixelValue = "255";

    public static string ImageToPpmStringFormat(Image image) =>
        new StringBuilder()
            .AppendLine("P3")
            .AppendLine(ImageWidthByHeight(image))
            .AppendLine(MaxPixelValue)
            .Append(ConvertPixelsToPpmStringFormat(image.Pixels))
            .ToString();

    private static string ImageWidthByHeight(Image image) =>
        $"{image.Width} {image.Height}";

    public static string ConvertPixelsToPpmStringFormat(IEnumerable<Pixel> pixels) =>
        pixels
            .Select(PixelToString)
            .StringJoin("\r\n");

    private static string PixelToString(Pixel arg) =>
        $"{(int)(arg.R * 255.999f)} {(int)(arg.G * 255.999f)} {(int)(arg.B * 255.999f)}";
}