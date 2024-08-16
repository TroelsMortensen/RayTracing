// See https://aka.ms/new-console-template for more information

using Core;
using static PpmImage.PpmImageExport;

PrintHelloWorldImage();

void PrintHelloWorldImage()
{
    const int width = 256;
    const int height = 256;
    Image image = new(width, height);

    for (int x = 0; x < image.Width; x++)
    {
        for (int y = 0; y < image.Height; y++)
        {
            float r = x / (float)(image.Height - 1);
            float g = y / (float)(image.Width - 1);
            float b = 0.0f;

            image.SetPixel(new Pixel(r, g, b), x, y);
        }
    }

    ExportPpm(image, WriteToFile);
}

void WriteToFile(string s)
{
    File.WriteAllText("C:\\TRMO\\RiderProjects\\RayTracing\\Shell\\CLI\\hello_world.ppm", s);
    Console.WriteLine("Printed");
}