using Core;
using Tooling;
using static Core.MathExtensions;

namespace PathTracing;

public static class PathTracer
{
    public static void Render(Image image, Camera camera) =>
        GenerateXYCoordinates(image.Width, image.Height)
            .Then(CalculatePixelCenter(camera))
            .Then(CreateRayFromCamCenterToPixelCenter(camera))
            .Then(CalculateRayHitColor(camera))
            .Then(CombineColorAndIndex())
            .ForEach(SetColorOnImage(image));

    private static Action<(Color Color, int Index)> SetColorOnImage(Image image) =>
        colorAndPixelIndex => image[colorAndPixelIndex.Index] = colorAndPixelIndex.Color;

    private static Func<Color, int, (Color Color, int Index)> CombineColorAndIndex() =>
        (color, i) => (Color: color, Index: i);

    private static Func<Ray, Color> CalculateRayHitColor(Camera camera) =>
        camera.RayHitColor;

    private static Func<Point3, Ray> CreateRayFromCamCenterToPixelCenter(Camera camera) =>
        pixelCenter => new Ray(camera.Center, pixelCenter - camera.Center);

    private static Func<(int X, int Y), Point3> CalculatePixelCenter(Camera camera) =>
        xyCoords => camera.ViewPort.CalculatePixelCenter(xyCoords.X, xyCoords.Y);
}