namespace Epsi.MazeCs;

public readonly record struct Vec2d(int X, int Y)
{
    public static Vec2d Zero => new(0, 0);

    public bool IsXInBound(int max) => X >= 0 && X < max;

    public bool IsYInBound(int max) => Y >= 0 && Y < max;

    public bool IsInBound(int maxX, int maxY) => IsXInBound(maxX) && IsYInBound(maxY);

    public Vec2d Add(int dx, int dy) => new(X + dx, Y + dy);

    public Vec2d Add(Vec2d other) => new(X + other.X, Y + other.Y);

    public Vec2d Midpoint(Vec2d other) => new((X + other.X) / 2, (Y + other.Y) / 2);

    public override string ToString() => $"({X}, {Y})";
}
