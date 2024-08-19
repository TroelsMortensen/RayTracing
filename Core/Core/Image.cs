namespace Core;

public record Color(double R, double G, double B);

public record Image
{
    public int Width { get; }
    public int Height { get; }
    public Color[] Colors { get; }

    public Image(int width, int height) =>
        (Width, Height, Colors) = (width, height, new Color[width * height]);

    public Image(int width, int height, Color[] pixels) =>
        (Width, Height, Colors) = (width, height, pixels);

    public Color this[int x, int y]
    {
        get => Colors[x + Width * y];
        set => Colors[x + Width * y] = value;
    }
}