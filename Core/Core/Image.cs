namespace Core;

public record Color(double R, double G, double B);

public record Image
{
    public int Width { get; }
    public int Height { get; }
    public Color[] Pixels { get; }

    public Image(int width, int height) =>
        (Width, Height, Pixels) = (width, height, new Color[width * height]);

    public Image(int width, int height, Color[] pixels) =>
        (Width, Height, Pixels) = (width, height, pixels);

    public Color this[int x, int y]
    {
        get => Pixels[x + Width * y];
        set => Pixels[x + Width * y] = value;
    }
}