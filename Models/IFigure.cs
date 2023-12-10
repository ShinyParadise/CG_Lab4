using CG_Lab4.Drawing;

namespace CG_Lab4.Models
{
    public interface IFigure : IEquatable<IFigure>
    {
        public List<Point> Points { get; set; }
        public Color FillColor { get; set; }

        public List<Edge> Edges { get => CreateEdges(Points); }

        public void ClipToFrame(Rectangle clipRectangle)
        {
            List<Point> clippedPoints = new();

            for (int i = 0; i < Points.Count; i++)
            {
                Point p1 = Points[i];
                Point p2 = Points[(i + 1) % Points.Count];

                if (CohenSutherland.ClipLine(clipRectangle, ref p1, ref p2))
                {
                    if (!clippedPoints.Contains(p1))
                    {
                        clippedPoints.Add(p1);
                    }
                    if (!clippedPoints.Contains(p2))
                    {
                        clippedPoints.Add(p2);
                    }
                }
            }

            Points = clippedPoints;
        }

        public Point GeneratePointInside()
        {
            var random = new Random();

            // Найти ограничивающий прямоугольник
            int minX = int.MaxValue, minY = int.MaxValue, maxX = int.MinValue, maxY = int.MinValue;
            foreach (Point point in Points)
            {
                minX = Math.Min(minX, point.X);
                minY = Math.Min(minY, point.Y);
                maxX = Math.Max(maxX, point.X);
                maxY = Math.Max(maxY, point.Y);
            }

            // Генерировать случайные точки внутри ограничивающего прямоугольника
            int x, y;
            do
            {
                x = random.Next(minX, maxX + 1);
                y = random.Next(minY, maxY + 1);
            } while (!IsPointInPolygon(new Point(x, y), Points));

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
    }
}
