using Point = CG_Lab4.Models.Point;

namespace CG_Lab4.Drawing
{
    public class SutherlandHodgman
    {
        public static List<Point> ClipPolygon(List<Point> subjectPolygon, List<Point> clipPolygon)
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

                    clippedPolygon.Add(next);
                }
                else if (IsInside(next.X, next.Y, x1, y1, x2, y2))
                {
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
            int dx1 = p2.X - p1.X;
            int dy1 = p2.Y - p1.Y;

            int dx2 = x2 - x1;
            int dy2 = y2 - y1;

            int denom = dx1 * dy2 - dy1 * dx2;

            if (denom == 0)
            {
                return p1;  // Линии параллельны или совпадают
            }

            int t = ((x1 - p1.X) * dy2 - (y1 - p1.Y) * dx2) / denom;

            int ix = p1.X + t * dx1;
            int iy = p1.Y + t * dy1;

            return new Point { X = ix, Y = iy };
        }
    }
}
