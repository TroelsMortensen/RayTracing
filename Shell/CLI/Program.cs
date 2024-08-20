// See https://aka.ms/new-console-template for more information

using System.Text.Json;
using Core;
using PathTracing;
using Tooling;
using static Core.Point3Exts;
using static PngImage.PngImageExport;

// PrintTestPng(256, 256);

Image image = SetupImage();
Camera camera = SetupCamera(image);
PathTracer.Render(image, camera);

ExportImageToPngFile(image, @"C:\TRMO\RiderProjects\RayTracing\Shell\CLI\dummy.png");

Console.WriteLine("Printed");

return;

void PrintTestPng(int width, int height) =>
    GenerateArrayOfDummyColors(width, height)
        .Then(CreateImageFromColors(width, height))
        .Finally(SaveToPng());

Color[] GenerateArrayOfDummyColors(int width, int height) =>
    MathExtensions.GenerateXYCoordinates(width, height)
        .Select(AddColorToXYTuple(width, height))
        .ToArray();

Func<Color[], Image> CreateImageFromColors(int width, int height) =>
    clrs => new Image(width, height, clrs);

Action<Image> SaveToPng() =>
    img => ExportImageToPngFile(
        img,
        @"C:\TRMO\RiderProjects\RayTracing\Shell\CLI\dummy.png"
    );

// ReSharper disable once InconsistentNaming
Func<(int X, int Y), Color> AddColorToXYTuple(int width, int height) =>
    tuple => new Color(
        tuple.X / (float)(height - 1),
        tuple.Y / (float)(width - 1),
        0.0f);

Image SetupImage()
{
    const double aspectRatio = 16.0 / 9.0;
    const int imageWidth = 50;
    Image image1 = new(imageWidth, aspectRatio);
    return image1;
}

Camera SetupCamera(Image image2)
{
    const double viewportHeight = 2.0;
    Camera camera1 = new(PointZero, viewportHeight, image2);
    return camera1;
}

