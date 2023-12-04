using Point = CG_Lab4.Models.Point;

namespace CG_Lab4.Drawing
{
    public class SutherlandHodgman
    {
        public static List<Point> Clip(List<Point> subjectPolygon, List<Point> clipPolygon)
        {
            List<Point> clippedPolygon = new List<Point>(subjectPolygon);

            for (int i = 0; i < clipPolygon.Count; i++)
            {
                int nextIndex = (i + 1) % clipPolygon.Count;
                Point clipEdgeStart = clipPolygon[i];
                Point clipEdgeEnd = clipPolygon[nextIndex];

                clippedPolygon = ClipEdge(clippedPolygon, clipEdgeStart.X, clipEdgeStart.Y, clipEdgeEnd.X, clipEdgeEnd.Y);
            }
            return clippedPolygon;
        }

        private static List<Point> ClipEdge(List<Point> polygon, int x1, int y1, int x2, int y2)
        {
            List<Point> clippedPolygon = new List<Point>();

            for (int i = 0; i < polygon.Count; i++)
            {
                Point current = polygon[i];
                Point next = polygon[(i + 1) % polygon.Count];

                if (IsInside(current.X, current.Y, x1, y1, x2, y2))
                {
                    if (!IsInside(next.X, next.Y, x1, y1, x2, y2))
                    {
                        Point intersection = GetIntersection(current, next, x1, y1, x2, y2);
                        clippedPolygon.Add(intersection);
                    }
                    else
                    {
                        clippedPolygon.Add(next);
                    }
                }
                else if (IsInside(next.X, next.Y, x1, y1, x2, y2))
                {
                    clippedPolygon.Add(next);
                    Point intersection = GetIntersection(current, next, x1, y1, x2, y2);
                    clippedPolygon.Add(intersection);
                }
            }

            return clippedPolygon;
        }

        private static bool IsInside(int x, int y, int x1, int y1, int x2, int y2)
        {
            return (x - x1) * (y2 - y1) - (y - y1) * (x2 - x1) >= 0;
        }

        private static Point GetIntersection(Point p1, Point p2, int x1, int y1, int x2, int y2)
        {
            int den = (p1.X - p2.X) * (y1 - y2) - (p1.Y - p2.Y) * (x1 - x2);
            if (den == 0) { return new Point { X = p1.X, Y = p1.Y }; }
            int num_x = (p1.X * p2.Y - p1.Y * p2.X) * (x1 - x2) -
                (p1.X - p2.X) * (x1 * y2 - y1 * x2);
            int num_y = (p1.X * p2.Y - p1.Y * p2.X) * (y1 - y2) -
                (p1.Y - p2.Y) * (x1 * y2 - y1 * x2);
            return new Point { X = num_x / den, Y = num_y / den };
        }
    }
}
