namespace CG_Lab4.Models
{
    class Polygon : IFigure
    {
        public Polygon(params Point[] points)
        {
            Points = points.ToList();
        }

        public Polygon(List<Point> points)
        {
            Points = points;
        }

        public List<Point> Points { get; set; }
        public Color FillColor { get; set; }
    }
}
