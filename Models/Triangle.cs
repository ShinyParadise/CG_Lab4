namespace CG_Lab4.Models
{
    public class Triangle : IFigure
    {
        public Triangle(Point p1, Point p2, Point p3)
        {
            Points = new List<Point> { p1, p2, p3 };
        }

        public List<Point> Points { get; set; }
        public Color FillColor { get; set; }
        public double Area => CalculateArea();

        public Point A { get => Points[0]; set => Points[0] = value; }
        public Point B { get => Points[1]; set => Points[1] = value; }
        public Point C { get => Points[2]; set => Points[2] = value; }

        private double CalculateArea()
        {
            return 0.5 * Math.Abs((A.X - C.X) * (B.Y - A.Y) - (A.X - B.X) * (C.Y - A.Y));
        }
    }
}
