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

        public bool Equals(IFigure? other)
        {
            if (other == null) return false;

            if (other is not Polygon) return false;

            return Points.SequenceEqual(Points);
        }
    }
}
