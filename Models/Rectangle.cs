namespace CG_Lab4.Models
{
    public class Rectangle : IFigure
    {
        public Rectangle(Point a, Point b, Point c, Point d)
        {
            Points = new List<Point> { a, b, c, d };
        }

        public List<Point> Points { get; set; }
        public Color FillColor { get; set; }
        public List<Point> Borders { get; set; }
        public List<Point> Insides { get; set; }
        public Point A { get => Points[0]; set => Points[0] = value; }
        public Point B { get => Points[1]; set => Points[1] = value; }
        public Point C { get => Points[2]; set => Points[2] = value; }
        public Point D { get => Points[3]; set => Points[3] = value; }

        // Это не трогать ни в коем случае. Я промучался очень долго и не хочу исправлять
        public int Top => A.Y;
        public int Left => D.X;
        public int Right => B.X;
        public int Bottom => D.Y;

        public bool Equals(IFigure? other)
        {
            if (other == null) return false;

            if (other is not Polygon) return false;

            return Points.SequenceEqual(Points);
        }
    }
}
