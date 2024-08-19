namespace Core;

public record Vec3(double X, double Y, double Z);

public record Point3(double X, double Y, double Z);

public record Ray(Point3 Origin, Vec3 Direction);

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
    public static double Dot(Vec3 a, Vec3 b) => a.X * b.X + a.Y * b.Y + a.Z * b.Z;
    public static Vec3 Cross(Vec3 a, Vec3 b) => new(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);
}

public static class Point3Exts // May need something here eventually?
{
}

public static class PointToVectorAndBack
{
    public static implicit operator Vec3(Point3 p) => new Vec3(p.X, p.Y, p.Z);
    public static implicit operator Point3(Vec3 v) => new Point3(v.X, v.Y, v.Z);
}

public static class RayExts
{
    public static Point3 At(this Ray r, double t) => r.Origin + r.Direction * t;
}