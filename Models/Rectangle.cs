namespace CG_Lab4.Models
{
    public class Rectangle : IFigure
    {
        public Rectangle(Point p1, Point p2, Point p3, Point p4)
        {
            Points = new List<Point> { p1, p2, p3, p4 };
        }

        public List<Point> Points { get; set; }
        public Color FillColor { get; set; }

        public Point A { get => Points[0]; set => Points[0] = value; }
        public Point B { get => Points[1]; set => Points[1] = value; }
        public Point C { get => Points[2]; set => Points[2] = value; }
        public Point D { get => Points[3]; set => Points[3] = value; }
    }
}
