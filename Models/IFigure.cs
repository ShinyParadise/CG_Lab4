using CG_Lab4.Drawing;

namespace CG_Lab4.Models
{
    public interface IFigure : IEquatable<IFigure>
    {
        public List<Point> Points { get; set; }
        public Color FillColor { get; set; }
        public List<Point> Borders { get; set; }
        public List<Point> Insides { get; set; }
        public List<Edge> Edges { get => CreateEdges(Points); }

        public void Rotate(double angle)
        {
            var center = FindCenter();
            float angleRadians = (float)(angle * Math.PI / 180.0);

            for (int i = 0; i < Points.Count; i++)
            {
                int x = Points[i].X - center.X;
                int y = Points[i].Y - center.Y;

                // Формула поворота для точки (x, y) относительно центра (0, 0)
                Points[i] = new Point(
                    (int)(x * Math.Cos(angleRadians) - y * Math.Sin(angleRadians)) + center.X,
                    (int)(x * Math.Sin(angleRadians) + y * Math.Cos(angleRadians)) + center.Y
                );
            }
        }

        public void ClipToFrame(Rectangle clipRectangle)
        {
            List<Point> clippedPoints = new();

            for (int i = 0; i < Points.Count; i++)
            {
                Point p1 = Points[i];
                Point p2 = Points[(i + 1) % Points.Count];

                var (startP, endP) = CohenSutherland.ClipLine(clipRectangle, p1, p2);
                //var (startP, endP) = LSSB.Clip(p1, p2, clipRectangle);

                if (startP !=  null) { p1 = (Point)startP; }
                if (endP !=  null) { p2 = (Point)endP; }

                if (!clippedPoints.Contains(p1) && startP != null)
                {
                    clippedPoints.Add(p1);
                }
                if (!clippedPoints.Contains(p2) && endP != null)
                {
                    clippedPoints.Add(p2);
                }
            }

            Points = clippedPoints;
        }

        public Point? GeneratePointInside()
        {
            if (Points.Count < 3) { return null; }

            double lenA = Math.Sqrt((Points[0].X - Points[1].X) * (Points[0].X - Points[1].X) + (Points[0].Y - Points[1].Y) * (Points[0].Y - Points[1].Y));
            double lenB = Math.Sqrt((Points[0].X - Points[2].X) * (Points[0].X - Points[2].X) + (Points[0].Y - Points[2].Y) * (Points[0].Y - Points[2].Y));
            double lenC = Math.Sqrt((Points[1].X - Points[2].X) * (Points[1].X - Points[2].X) + (Points[1].Y - Points[2].Y) * (Points[1].Y - Points[2].Y));
            double per = lenA + lenB + lenC;
            int x = (int)((lenA * Points[0].X + lenB * Points[1].X + lenC * Points[2].X) / per);
            int y = (int)((lenA * Points[0].Y + lenB * Points[1].Y + lenC * Points[2].Y) / per);
            return new Point(x, y);
        }

        public static bool IsPointInPolygon(Point point, List<Point> polygon)
        {
            int crossings = 0;

            for (int i = 0; i < polygon.Count; i++)
            {
                int j = (i + 1) % polygon.Count;
                if (((polygon[i].Y <= point.Y && point.Y < polygon[j].Y) || (polygon[j].Y <= point.Y && point.Y < polygon[i].Y)) &&
                    (point.X < (polygon[j].X - polygon[i].X) * (point.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X))
                {
                    crossings++;
                }
            }
            return crossings % 2 != 0;
        }

        protected static List<Edge> CreateEdges(List<Point> vertices)
        {
            List<Edge> edges = new();

            for (int i = 0; i < vertices.Count - 1; i++)
            {
                edges.Add(new Edge(vertices[i], vertices[i + 1]));
            }

            edges.Add(new Edge(vertices.Last(), vertices.First()));

            return edges;
        }

        protected Point FindCenter()
        {
            int totalX = 0, totalY = 0;

            foreach (Point point in Points)
            {
                totalX += point.X;
                totalY += point.Y;
            }

            int centerX = totalX / Points.Count;
            int centerY = totalY / Points.Count;

            return new Point(centerX, centerY);
        }
    }
}
