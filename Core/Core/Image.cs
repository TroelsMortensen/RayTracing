namespace Core;

public record Pixel(double R, double G, double B);

public record Image
{
    public int Width { get; }
    public int Height { get; }
    public Pixel[] Pixels { get; }

    public Image(int width, int height) =>
        (Width, Height, Pixels) = (width, height, new Pixel[width * height]);

    public Image(int width, int height, Pixel[] pixels) =>
        (Width, Height, Pixels) = (width, height, pixels);

    public Pixel this[int x, int y]
    {
        get => Pixels[x + Width * y];
        set => Pixels[x + Width * y] = value;
    }
}