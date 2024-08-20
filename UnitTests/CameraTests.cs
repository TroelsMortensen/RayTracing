using Core;
using static Core.ColorExts;

namespace UnitTests;

public class CameraTests
{
    [Fact]
    public void ShouldNotCauseStackOverflow()
    {
        Func<double,Color> func = CameraExts.CalculateBlueGradientBasedOnGradient();
        Color color = func(0.5);
    }

    [Fact]
    public void ShouldNotCauseStackOverflowEither()
    {
        Color color = Test(0.5);
    }

    [Fact]
    public void WTF()
    {
        Color color = new Color(1.0, 1.0, 1.0);
    }

    [Fact]
    public void CanCreateColorFromFunc()
    {
        Color color = Color(1.0, 1.0, 1.0);
    }
    
    private Color Test(double d)
    {
        return (1.0 - d) * Color(1.0, 1.0, 1.0) + d * Color(0.5, 0.7, 1.0);
    }
}