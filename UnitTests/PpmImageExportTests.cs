using Core;
using Xunit.Abstractions;
using static PpmImage.PpmImageExport;

namespace UnitTests;

public class PpmImageExportTests(ITestOutputHelper testOutputHelper)
{
    private readonly ITestOutputHelper testOutputHelper = testOutputHelper;

    [Fact]
    public void ExportImageToStringShouldProduceCorrectPpmFormat()
    {
        Image image = new(2, 2);
        image[0, 0] = new Color(1, 0, 0);
        image[1, 0] = new Color(0, 1, 0);
        image[0, 1] = new Color(0, 0, 1);
        image[1, 1] = new Color(1, 1, 1);

        string actualResult = ImageToPpmStringFormat(image);

        const string expectedResult = """
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
        const string expectedResult = """
                                      255 0 0
                                      0 255 0
                                      0 0 255
                                      255 255 255
                                      """;

        Image image = new(2, 2);
        image[0, 0] = new Color(1, 0, 0);
        image[1, 0] = new Color(0, 1, 0);
        image[0, 1] = new Color(0, 0, 1);
        image[1, 1] = new Color(1, 1, 1);

        string actualResult = ConvertPixelsToPpmStringFormat(image.Colors);
        Assert.Equal(expectedResult, actualResult);
    }
}