namespace Core;

public record Pixel(float R, float G, float B);

public record Image
{
    public Image(int width, int height)
    {
        Width = width;
        Height = height;
        Pixels = new Pixel[width * height];
    }

    public int Width { get; }
    public int Height { get; }
    public Pixel[] Pixels { get; }

    public void SetPixel(Pixel p, int x, int y) =>
        Pixels[x + Width * y] = p;
}