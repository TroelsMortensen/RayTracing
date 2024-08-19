using System.Text;
using Core;
using Tooling;

namespace PpmImage;

public static class PpmImageExport
{
    private const string MaxColorValue = "255";

    public static void ExportImageToPpmFile(Image image, string path) =>
        image
            .Then(ImageToPpmStringFormat)
            .Finally(SaveToFile(path));

    private static Action<string> SaveToFile(string path) =>
        s => File.WriteAllText(path, s);

    public static string ImageToPpmStringFormat(Image image) =>
        new StringBuilder()
            .AppendLine("P3")
            .AppendLine(ImageWidthByHeight(image))
            .AppendLine(MaxColorValue)
            .Append(ConvertPixelsToPpmStringFormat(image.Colors))
            .ToString();


    private static string ImageWidthByHeight(Image image) =>
        $"{image.Width} {image.Height}";

    public static string ConvertPixelsToPpmStringFormat(IEnumerable<Color> colors) =>
        colors
            .Select(ColorToStringRepresentation)
            .StringJoin("\r\n");

    private static string ColorToStringRepresentation(Color arg) =>
        $"{(int)(arg.R * 255.999f)} {(int)(arg.G * 255.999f)} {(int)(arg.B * 255.999f)}";
}