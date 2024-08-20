namespace Core;

public static class MathExtensions
{
    public static double Square(this double d) => d * d;
    
    // ReSharper disable once InconsistentNaming
    public static IEnumerable<(int X, int Y)> GenerateXYCoordinates(int width, int height) =>
        Enumerable.Range(0, width)
            .SelectMany(x =>
                Enumerable.Range(0, height)
                    .Select(y => (X: x, Y: y))
            );
}