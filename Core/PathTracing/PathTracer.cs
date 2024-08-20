using Core;
using Tooling;
using static Core.MathExtensions;

namespace PathTracing;

public static class PathTracer
{
    public static void Render(Image image, Camera camera) =>
        GenerateXYCoordinates(image.Width, image.Height)
            .Then(CalculatePixelCenterOnViewPortAtXY(camera.ViewPort))
            .Then(CreateRayFromCameraOriginToPixel(camera))
            .Then(CalculateColorOfRayHit(camera))
            .Then(CombineColorAndPixelIndex())
            .ForEach(InsertColorInImage(image));

    private static Action<(Color Color, int Index)> InsertColorInImage(Image image) =>
        colorAtIndex => image[colorAtIndex.Index] = colorAtIndex.Color;

    private static Func<Color, int, (Color Color, int Index)> CombineColorAndPixelIndex() =>
        (color, i) => (Color: color, Index: i);

    private static Func<Ray, Color> CalculateColorOfRayHit(Camera camera) =>
        camera.RayColor;

    private static Func<Point3, Ray> CreateRayFromCameraOriginToPixel(Camera camera) =>
        pixelCenter => new Ray(camera.Center, pixelCenter - camera.Center);

    private static Func<(int X, int Y), Point3> CalculatePixelCenterOnViewPortAtXY(ViewPort viewPort) =>
        xyCoordinates => viewPort.CalculatePixelCenter(xyCoordinates.X, xyCoordinates.Y);
}