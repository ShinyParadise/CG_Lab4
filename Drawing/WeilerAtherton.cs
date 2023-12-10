using CG_Lab4.Models;
using Point = CG_Lab4.Models.Point;

namespace CG_Lab4.Drawing
{
    public class WeilerAtherton
    {
        public static List<Polygon> Clip(IFigure polygonA, IFigure polygonB)
        {
            List<Point> resultVertices = new();

            foreach (var edgeA in polygonA.Edges)
            {
                foreach (var edgeB in polygonB.Edges)
                {
                    List<Point> intersectionPoints = FindIntersection(edgeA, edgeB);
                    resultVertices.AddRange(intersectionPoints);
                }
            }

            List<Polygon> resultPolygons = AssemblePolygons(resultVertices);

            return resultPolygons;
        }

        static List<Point> FindIntersection(Edge edgeA, Edge edgeB)
        {
            List<Point> intersectionPoints = new List<Point>();

            (int Ax, int Ay, int C1) = Coefficients(edgeA);
            (int Bx, int By, int C2) = Coefficients(edgeB);

            int determinant = Ax * By - Ay * Bx;

            if (Math.Abs(determinant) < 1)
            {
                return intersectionPoints;
            }

            int x = (By * C1 - Ay * C2) / determinant;
            int y = (Ax * C2 - Bx * C1) / determinant;

            if (IsPointOnEdge(x, y, edgeA) && IsPointOnEdge(x, y, edgeB))
            {
                intersectionPoints.Add(new Point(x, y));
            }

            return intersectionPoints;
        }

        static (int, int, int) Coefficients(Edge edge)
        {
            int Ax = edge.End.Y - edge.Start.Y;
            int Ay = edge.Start.X - edge.End.X;
            int C = edge.End.X * edge.Start.Y - edge.Start.X * edge.End.Y;
            return (Ax, Ay, C);
        }

        static bool IsPointOnEdge(int x, int y, Edge edge)
        {
            int x1 = edge.Start.X, y1 = edge.Start.Y, x2 = edge.End.X, y2 = edge.End.Y;

            return Math.Abs((x - x1) * (y2 - y1) - (y - y1) * (x2 - x1)) < 1 &&
                   x >= Math.Min(x1, x2) && x <= Math.Max(x1, x2) &&
                   y >= Math.Min(y1, y2) && y <= Math.Max(y1, y2);
        }

        static List<Polygon> AssemblePolygons(List<Point> vertices)
        {
            List<Polygon> resultPolygons = new();
            // Для простоты примера, создается только один контур, представляющий весь список вершин
            List<Point> outerContour = vertices;
            List<Polygon> innerContours = new();

            resultPolygons.Add(new(outerContour.ToArray()));

            foreach (var contour in innerContours)
            {
                if (IsInside(contour.Points, outerContour))
                {
                    resultPolygons.Add(contour);
                }
            }

            return resultPolygons;
        }

        static bool IsInside(List<Point> contour, List<Point> outerContour)
        {
            int count = 0;
            int n = outerContour.Count;

            for (int i = 0; i < n; i++)
            {
                Point vertex1 = outerContour[i];
                Point vertex2 = outerContour[(i + 1) % n];

                if (IsPointOnSegment(contour[0], vertex1, vertex2))
                {
                    count++;
                }

                if (((vertex1.Y <= contour[0].Y && vertex2.Y > contour[0].Y) || (vertex2.Y <= contour[0].Y && vertex1.Y > contour[0].Y))
                    && (contour[0].X < (vertex1.X - vertex2.X) * (contour[0].Y - vertex2.Y) / (vertex1.Y - vertex2.Y) + vertex2.X))
                {
                    count++;
                }
            }

            return count % 2 == 1;
        }

        static bool IsPointOnSegment(Point p, Point q1, Point q2)
        {
            return (p.Y <= Math.Max(q1.Y, q2.Y) && p.Y >= Math.Min(q1.Y, q2.Y) &&
                    p.X <= Math.Max(q1.X, q2.X) && p.X >= Math.Min(q1.X, q2.X));
        }
    }
}
