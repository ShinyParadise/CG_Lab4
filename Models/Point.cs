namespace CG_Lab4.Models
{
    public struct Point : IEquatable<Point>
    {
        public int X;
        public int Y;
        public Color Color;
        public Point(int X, int Y, Color? color = null)
        {
            color ??= Color.Black;
            this.Color = (Color)color;
            this.X = X;
            this.Y = Y;
        }

        public static Point operator / (Point a, int b)
        {
            return new Point(a.X / b, a.Y / b);
        }

        public static Point operator * (Point a, int b)
        {
            return new Point(a.X * b, a.Y * b);
        }

        public static Point operator - (Point a, int b)
        {
            return new Point(a.X - b, a.Y - b);
        }

        public static Point operator + (Point a, int b)
        {
            return new Point(a.X + b, a.Y + b);
        }

        public readonly bool Equals(Point other) => X == other.X && Y == other.Y;

        public override readonly bool Equals(object? obj)
        {
            return obj is Point point && Equals(point);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }
    }

    public class Edge
    {
        public Point Start { get; }
        public Point End { get; }

        public Edge(Point start, Point end)
        {
            Start = start;
            End = end;
        }
    }
}
