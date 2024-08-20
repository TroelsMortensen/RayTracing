using Core;
using Tooling;
using static Core.MathExtensions;

namespace PathTracing;

public static class PathTracer
{
    public static void Render(Image image, Camera camera)
    {
        GenerateXYCoordinates(image.Width, image.Height).Then((tuple, index) => (X: tuple.X, Y: tuple.Y, Index: index))
            .Then(tuple =>
            {
                Color color = GetColorAtPixel(camera, tuple.X, tuple.Y);
                image[tuple.Index] = color;
                return color;
            })
            .Then((color, i) => (Color: color, Index: i))
            .ForEach(c => image[c.Index] = c.Color);
    }

    private static Color GetColorAtPixel(Camera camera, int i, int j)
    {
        Point3 pixelCenter = camera.ViewPort.CalculatePixelCenter(i, j);
        Ray ray = new(camera.Center, pixelCenter - camera.Center);
        Color color = camera.RayColor(ray);
        return color;
    }
}