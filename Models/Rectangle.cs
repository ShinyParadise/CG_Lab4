namespace CG_Lab4.Models
{
    public class Rectangle : IFigure
    {
        public Rectangle(Point p1, Point p2, Point p3, Point p4)
        {
            _points = new List<Point> { p1, p2, p3, p4 };
        }

        public List<Point> Points => _points;
        public Color FillColor { get => _fillColor; set => _fillColor = value; }

        private List<Point> _points;
        private Color _fillColor;
    }
}
