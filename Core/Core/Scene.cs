﻿using Tooling;
using static Core.ColorExts;

namespace Core;

public record Scene();

public record ViewPort
{
    public ViewPort(double height, Image image, Point3 cameraCenter, double cameraFocalLength)
    {
        Height = height;
        Width = height * ((double)image.Height) / image.Width;
        Right = new Vec3(Width, 0, 0);
        Down = new Vec3(0, -Height, 0);

        PixelDeltaRight = Right / image.Width;
        PixelDeltaDown = Down / image.Height;
        UpperLeft = cameraCenter - new Vec3(0, 0, cameraFocalLength) - Right / 2 - Down / 2;

        Pixel00Location = UpperLeft + 0.5 * (PixelDeltaRight + PixelDeltaDown);
    }

    public double Height { get; }
    public double Width { get; }

    public Vec3 Right { get; }
    public Vec3 Down { get; }

    public Vec3 PixelDeltaRight { get; }
    public Vec3 PixelDeltaDown { get; }

    public Point3 UpperLeft { get; }

    public Point3 Pixel00Location { get; }
}

public static class ViewPortExts
{
    public static Point3 CalculatePixelCenter(this ViewPort viewPort, int x, int y)
        => viewPort.Pixel00Location + x * viewPort.PixelDeltaRight + y * viewPort.PixelDeltaDown;
}

public record Camera
{
    public Point3 Center { get; }
    public double FocalLength { get; } = 1.0; // distance from cam center to view port center
    public ViewPort ViewPort { get; }

    public Camera(Point3 center, double viewportHeight, Image image) =>
        (Center, ViewPort) = (center, new ViewPort(viewportHeight, image, center, FocalLength));
}

public static class CameraExts
{
    public static Color RayHitColor(this Camera camera, Ray ray) =>
        PlaceHolderDummyFunctionWhichComputesBlueColorGradient(ray);

    private static Color PlaceHolderDummyFunctionWhichComputesBlueColorGradient(Ray ray) =>
        ray
            .Then(GetNormalizedRayDirection())
            .Then(CalculateGradientBasedOnRayDirectionYValue())
            .Then(CalculateBlueGradientBasedOnGradient());

    public static Func<double, Color> CalculateBlueGradientBasedOnGradient() =>
        d => (1.0 - d) * Color(1.0, 1.0, 1.0) + d * Color(0.5, 0.7, 1.0);

    private static Func<Vec3, double> CalculateGradientBasedOnRayDirectionYValue() =>
        direction => 0.5 * (direction.Y + 1);

    private static Func<Ray, Vec3> GetNormalizedRayDirection() =>
        r => r.Direction.Normalize();
}