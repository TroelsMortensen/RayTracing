namespace Core;

public record Pixel(float R, float G, float B);

public record Image
{
    public Image(int width, int height) =>
        (Width, Height, Pixels) = (width, height, new Pixel[width * height]);
    //     Width = width;
    //     Height = height;
    //     Pixels = new Pixel[width * height];
    // }

    public Image(int width, int height, Pixel[] pixels) =>
        (Width, Height, Pixels) = (width, height, pixels);

    public int Width { get; }
    public int Height { get; }
    public Pixel[] Pixels { get; }

    public void SetPixel(Pixel p, int x, int y) =>
        Pixels[x + Width * y] = p;

    public Pixel GetPixel(int x, int y) =>
        Pixels[x + Width * y];
}