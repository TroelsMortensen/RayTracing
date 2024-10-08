﻿namespace Core;

public record Color
{
    public Color(double R, double G, double B)
    {
        this.R = R;
        this.G = G;
        this.B = B;
    }

    public static Color operator *(double a, Color b) => new(a * b.R, a * b.G, a * b.B);
    public static Color operator +(Color a, Color b) => new(a.R + b.R, a.G + b.G, a.B + b.B);
    public double R { get; }
    public double G { get; }
    public double B { get; }

    public override string ToString() =>
        $"Color: ({R}, {G}, {B})";
}

public static class ColorExts
{
    public static Color Color(double r, double g, double b) => new Color(r, g, b);
}

public record Image
{
    public int Width { get; }
    public int Height { get; }
    public Color[] Colors { get; }

    public Image(int width, int height) =>
        (Width, Height, Colors) = (width, height, new Color[width * height]);

    public Image(int width, int height, Color[] colors) =>
        (Width, Height, Colors) = (width, height, colors);

    public Image(int width, double aspectRatio) =>
        (Width, Height, Colors) = (width, (int)(width / aspectRatio), new Color[width * (int)(width / aspectRatio)]);

    public Color this[int index]
    {
        get => Colors[index];
        set => Colors[index] = value;
    }

    public Color this[int x, int y]
    {
        get => Colors[x + Width * y];
        set => Colors[x + Width * y] = value;
    }
}