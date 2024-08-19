// See https://aka.ms/new-console-template for more information

using Core;
using Tooling;
using static PngImage.PngImageExport;
using Image = Core.Image;

PrintTestPng(256, 256);

Console.WriteLine("Printed");

return;

void PrintTestPng(int width, int height) =>
    GenerateArrayOfDummyPixels(width, height)
        .Then(CreateImageFromPixels(width, height))
        .Finally(SaveToPng());

Pixel[] GenerateArrayOfDummyPixels(int width, int height) =>
    Enumerable.Range(0, width).SelectMany(x => Enumerable.Range(0, height).Select(y => (X: x, Y: y)))
        .Select(XYTupleToPixel(width, height))
        .ToArray();

Func<Pixel[], Image> CreateImageFromPixels(int width, int height) =>
    pxls => new Image(width, height, pxls);

Action<Image> SaveToPng() =>
    image => ExportImageToPngFile(
        image,
        @"C:\TRMO\RiderProjects\RayTracing\Shell\CLI\dummy.png"
    );

// ReSharper disable once InconsistentNaming
Func<(int X, int Y), Pixel> XYTupleToPixel(int width, int height) =>
    tuple => new Pixel(
        tuple.X / (float)(height - 1),
        tuple.Y / (float)(width - 1),
        0.0f);