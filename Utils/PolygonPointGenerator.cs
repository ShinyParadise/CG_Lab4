using CG_Lab4.Models;
using Point = CG_Lab4.Models.Point;

namespace CG_Lab4.Utils
{
    class PolygonPointGenerator
    {
        private PolygonPointGenerator() { }

        public static Point GenerateRandomPointInsidePolygon(List<Point> polygon)
        {
            // вернет центр вписанной окружности
            double lenA = Math.Sqrt((polygon[0].X - polygon[1].X) * (polygon[0].X - polygon[1].X) + (polygon[0].Y - polygon[1].Y) * (polygon[0].Y - polygon[1].Y));
            double lenB = Math.Sqrt((polygon[0].X - polygon[2].X) * (polygon[0].X - polygon[2].X) + (polygon[0].Y - polygon[2].Y) * (polygon[0].Y - polygon[2].Y));
            double lenC = Math.Sqrt((polygon[1].X - polygon[2].X) * (polygon[1].X - polygon[2].X) + (polygon[1].Y - polygon[2].Y) * (polygon[1].Y - polygon[2].Y));
            double per = lenA + lenB + lenC;
            int x = (int)((lenA * polygon[0].X + lenB * polygon[1].X + lenC * polygon[2].X) / per);
            int y = (int)((lenA * polygon[0].Y + lenB * polygon[1].Y + lenC * polygon[2].Y) / per);
            return new Point(x, y);
        }
    }
    /*var triangles = TriangulatePolygon(polygon);
            double totalArea = triangles.Sum(t => t.Area);
            double randomArea = RandomDouble() * totalArea;

            Triangle? selectedTriangle = null;
            double accumulatedArea = 0;

            foreach (var triangle in triangles)
            {
                accumulatedArea += triangle.Area;
                if (accumulatedArea >= randomArea)
                {
                    selectedTriangle = triangle;
                    break;
                }
            }

            double alpha = RandomDouble();
            double beta = RandomDouble();

            if (alpha + beta > 1)
            {
                alpha = 1 - alpha;
                beta = 1 - beta;
            }

            double gamma = 1 - alpha - beta;
            double x = alpha * selectedTriangle.A.X + beta * selectedTriangle.B.X + gamma * selectedTriangle.C.X;
            double y = alpha * selectedTriangle.A.Y + beta * selectedTriangle.B.Y + gamma * selectedTriangle.C.Y;

            return new Point((int)Math.Floor(x), (int)Math.Floor(y));
        }

        private static List<Triangle> TriangulatePolygon(List<Point> polygon)
        {
            List<Triangle> triangles = new List<Triangle>();

            for (int i = 2; i < polygon.Count; i++)
            {
                triangles.Add(new Triangle(polygon[0], polygon[i - 1], polygon[i]));
            }

            return triangles;
        }

        private static double RandomDouble()
        {
            Random random = new();
            return random.NextDouble();
        }
    }*/

        }
