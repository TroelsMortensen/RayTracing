namespace Core;

public record Vec3(double X, double Y, double Z);

public record Point(double X, double Y, double Z);

public static class Vector3Exts
{
    public static double Length(this Vec3 v) => Math.Sqrt(v.X.Square() + v.Y.Square() + v.Z.Square());
    public static double LengthSquared(this Vec3 v) => v.X.Square() + v.Y.Square() + v.Z.Square();
    public static Vec3 Normalize(this Vec3 v) => v / v.Length();
    public static void Print(this Vec3 v) => Console.WriteLine($"({v.X}, {v.Y}, {v.Z})");

    public static Vec3 operator +(Vec3 a, Vec3 b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
    public static Vec3 operator -(Vec3 a, Vec3 b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
    public static Vec3 operator *(Vec3 a, double b) => new(a.X * b, a.Y * b, a.Z * b);
    public static Vec3 operator *(double a, Vec3 b) => new(a * b.X, a * b.Y, a * b.Z);
    public static Vec3 operator /(Vec3 a, double b) => new(a.X / b, a.Y / b, a.Z / b);
}

public static class PointExts
{
}

public static class SharedExts
{
    public static implicit operator Vec3(Point p) => new Vec3(p.X, p.Y, p.Z);
    public static implicit operator Point(Vec3 v) => new Point(v.X, v.Y, v.Z);
}