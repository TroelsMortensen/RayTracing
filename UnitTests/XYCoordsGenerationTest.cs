using System.Text;
using Xunit.Abstractions;

namespace UnitTests;

// ReSharper disable once InconsistentNaming
public class XYCoordsGenerationTest
{
    private readonly ITestOutputHelper testOutputHelper;

    public XYCoordsGenerationTest(ITestOutputHelper testOutputHelper)
    {
        this.testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void SelectWithSelectManyGeneratesSameTuples()
    {
        const int width = 4;
        const int height = 3;
        List<(int x, int y)> xyCoordsNormal = NormalForloops(width, height);
        List<(int x, int y)> xyCoords = Core.MathExtensions.GenerateXYCoordinates(width, height).ToList();
        testOutputHelper.WriteLine("Normal: " + GenerateString(xyCoordsNormal));
        testOutputHelper.WriteLine("LINQ:   " + GenerateString(xyCoords));
        Assert.Equal(xyCoordsNormal, xyCoords);
    }

    private static string? GenerateString(List<(int x, int y)> xyCoordsNormal)
    {
        StringBuilder sb = new StringBuilder();
        foreach ((int x, int y) tuple in xyCoordsNormal)
        {
            sb.Append($"({tuple.x}, {tuple.y}) ");
        }

        return sb.ToString();
    }

    [Fact]
    public void AddingIndexShouldBeSame()
    {
        const int width = 4;
        const int height = 3;
        List<(int x, int y, int index)> xyCoordsNormal = NormalForloopsWithIndex(width, height);
        List<(int x, int y)> xyCoords = Core.MathExtensions.GenerateXYCoordinates(width, height).ToList();
        List<(int x, int y, int index)> xyCoordsWithIndex = xyCoords
            .Select((xy, index) => (xy.x, xy.y, index))
            .ToList();
        Assert.Equal(xyCoordsNormal, xyCoordsWithIndex);
    }

    private List<(int x, int y, int index)> NormalForloopsWithIndex(int width, int height)
    {
        List<(int x, int y, int index)> xyCoords = new();
        int index = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                xyCoords.Add((x, y, index));
                index++;
            }
        }

        return xyCoords;
    }


    private List<(int x, int y)> NormalForloops(int width, int height)
    {
        List<(int x, int y)> xyCoords = new();
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                xyCoords.Add((x, y));
            }
        }

        return xyCoords;
    }
}