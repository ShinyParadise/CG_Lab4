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
                        if (!clippedPolygon.Contains(intersection))
                        {
                            clippedPolygon.Add(intersection);
                        }
                    }
                    else
                    {
                        if (!clippedPolygon.Contains(next))
                        {
                            clippedPolygon.Add(next);
                        }
                    }
                }
                else if (IsInside(next.X, next.Y, x1, y1, x2, y2))
                {
                    Point intersection = GetIntersection(current, next, x1, y1, x2, y2);
                    if (!clippedPolygon.Contains(intersection))
                    {
                        clippedPolygon.Add(intersection);
                    }
                    if (!clippedPolygon.Contains(next))
                    {
                        clippedPolygon.Add(next);
                    }
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
            /*if ((Math.Max(p1.X, p2.X) < Math.Min(x1, x2)) || (Math.Min(p1.X, p2.X) > Math.Max(x1, x2)))
            { return new Point { X = p1.X, Y = p1.Y }; }
            if ((Math.Max(p1.Y, p2.Y) < Math.Min(y1, y2)) || (Math.Min(p1.Y, p2.Y) > Math.Max(y1, y2)))
            { return new Point { X = p1.X, Y = p1.Y }; }*/

            int den = (p1.X - p2.X) * (y1 - y2) - (p1.Y - p2.Y) * (x1 - x2);
            if (den == 0) { return new Point { X = p1.X, Y = p1.Y }; }
            int num_x = (p1.X * p2.Y - p1.Y * p2.X) * (x1 - x2) -
                (p1.X - p2.X) * (x1 * y2 - y1 * x2);
            int num_y = (p1.X * p2.Y - p1.Y * p2.X) * (y1 - y2) -
                (p1.Y - p2.Y) * (x1 * y2 - y1 * x2);
            double x = num_x / den;
            double y = num_y / den;
            return new Point { X = (int)Math.Round(x), Y = (int)Math.Round(y) };
        }
    }
}
