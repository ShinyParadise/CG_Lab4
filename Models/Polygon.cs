namespace CG_Lab4.Models
{
    public class Polygon : IFigure
    {
        public Polygon(params Point[] points)
        {
            Points = points.ToList();
        }

        public List<Point> Points { get; set; }
        public Color FillColor { get; set; }
    }
}
