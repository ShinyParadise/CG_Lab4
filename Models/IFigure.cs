using CG_Lab4.Drawing;

namespace CG_Lab4.Models
{
    public interface IFigure
    {
        public List<Point> Points { get; set; }
        public Color FillColor { get; set; }

        public void ClipToFrame(Rectangle clipRectangle)
        {
            List<Point> clippedPoints = new();

            for (int i = 0; i < Points.Count; i++)
            {
                Point p1 = Points[i];
                Point p2 = Points[(i + 1) % Points.Count];

                if (CohenSutherland.ClipLine(clipRectangle, ref p1, ref p2))
                {
                    clippedPoints.Add(p1);
                    clippedPoints.Add(p2);
                }
            }

            Points = clippedPoints;
        }
    }
}
