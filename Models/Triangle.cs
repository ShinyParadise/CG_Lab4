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
    }
}
