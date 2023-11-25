namespace CG_Lab4.Models
{
    public struct Point
    {
        public int X;
        public int Y;
        public bool isVisible = true;

        public Point(int X, int Y)
        {
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
    }
}
