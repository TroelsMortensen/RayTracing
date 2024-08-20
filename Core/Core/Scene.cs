namespace Core;

public record Scene();

public record ViewPort(int Width, int Height);

public record Camera
{
    public Point3 Center { get; }
    public double FocalLength { get; } = 1.0; // distance from cam center to view port center

    public Camera(Point3 center)
    {
        Center = center;
    }
}

public static class CameraExts
{

    public static Color RayColor(this Camera camera, Ray ray)
        => Color.BLACK;
}
