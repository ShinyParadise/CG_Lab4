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
    }
}
