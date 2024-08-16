using Core;
using Xunit.Abstractions;
using static PpmImage.PpmImageExport;

namespace UnitTests;

public class PpmImageExportTests
{
    private readonly ITestOutputHelper testOutputHelper;

    public PpmImageExportTests(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void ExportImageToStringShouldProduceCorrectPpmFormat()
    {
        string actualResult = "";

        var image = new Image(2, 2);
        image.SetPixel(new Pixel(1, 0, 0), 0, 0);
        image.SetPixel(new Pixel(0, 1, 0), 1, 0);
        image.SetPixel(new Pixel(0, 0, 1), 0, 1);
        image.SetPixel(new Pixel(1, 1, 1), 1, 1);

        ExportPpm(image, s => actualResult = s);

        string expectedResult = """
                                P3
                                2 2
                                255
                                255 0 0
                                0 255 0
                                0 0 255
                                255 255 255
                                """;

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void PixelsToSingleStringShouldNotHaveTrailingLineBreaks()
    {
        string expectedResult = """
                                255 0 0
                                0 255 0
                                0 0 255
                                255 255 255
                                """;
        
        var image = new Image(2, 2);
        image.SetPixel(new Pixel(1, 0, 0), 0, 0);
        image.SetPixel(new Pixel(0, 1, 0), 1, 0);
        image.SetPixel(new Pixel(0, 0, 1), 0, 1);
        image.SetPixel(new Pixel(1, 1, 1), 1, 1);

        string actualResult = ConvertPixelsToPpmStringFormat(image.Pixels);
        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void GenerateXYWithLinq()
    {
        const int width = 4;
        const int height = 4;
        Image image = new(width, height);

        var list = Enumerable.Range(0, width).SelectMany(x => Enumerable.Range(0, height).Select(y => (x, y)))
            .ToList();
        ;
        // .ForEach(tuples => testOutputHelper.WriteLine(tuples.ToString()));
    }
}