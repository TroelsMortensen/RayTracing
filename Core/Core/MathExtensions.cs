namespace Core;

public static class MathExtensions
{
    public static double Square(this double d) => d * d;
    
    // ReSharper disable once InconsistentNaming
    public static IEnumerable<(int X, int Y)> GenerateXYCoordinates(int width, int height) =>
        Enumerable.Range(0, height)
            .SelectMany(y =>
                Enumerable.Range(0, width)
                    .Select(x => (X: x, Y: y))
            );
}