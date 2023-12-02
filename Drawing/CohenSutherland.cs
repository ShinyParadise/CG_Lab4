using CG_Lab4.Models;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography.Pkcs;
using Point = CG_Lab4.Models.Point;
using Rectangle = CG_Lab4.Models.Rectangle;

namespace CG_Lab4.Drawing
{
    public class CohenSutherland
    {
        // Коды для каждого положения точки относительно окна
        private const int INSIDE = 0; // 0000
        private const int LEFT = 1;   // 0001
        private const int RIGHT = 2;  // 0010
        private const int BOTTOM = 4; // 0100
        private const int TOP = 8;    // 1000

        public static IFigure ClipToRectangle(IFigure triangle, IFigure rect)
        {
            Polygon polygon = new Polygon();
            for (int i = 0; i < triangle.Points.Count - 1; i++)
            {
                polygon.Points.AddRange(ClipPoint(triangle.Points, i, rect.Points[3].X, rect.Points[1].Y, rect.Points[1].X, rect.Points[3].Y));
            }
            polygon.Points.AddRange(ClipPoint(triangle.Points, triangle.Points.Count, rect.Points[3].X, rect.Points[1].Y, rect.Points[1].X, rect.Points[3].Y));
            return polygon;
        }

        private static List<Point> ClipPoint(List<Point> vertices, int index, int xmin, int ymin, int xmax, int ymax)
        {
            List<Point> points = new List<Point>();
            int code = INSIDE;
            Point point = new Point();
            Point pBegin;
            Point pEnd;
            if (index != vertices.Count)
            {
                pBegin = vertices[index];
                pEnd = vertices[index + 1];
            }
            else
            {
                pBegin = vertices[0];
                pEnd = vertices[index - 1];
            }

            int codeBegin = FindCode(pBegin, xmin, ymin, xmax, ymax);
            int codeEnd = FindCode(pEnd, xmin, ymin, xmax, ymax);

            while ((codeBegin | codeEnd) != 0)
            {
                /* если обе точки с одной стороны прямоугольника, то отрезок не пересекает прямоугольник */
                if ((codeBegin & codeEnd) != 0)
                {
                    return points;
                }
                /* выбираем точку c с ненулевым кодом */
                if ((codeBegin) != 0)
                {
                    code = codeBegin;
                    point = pBegin;
                }
                else
                {
                    code = codeEnd;
                    point = pEnd;
                }

                if ((code & LEFT) != 0)
                {
                    point.Y += (pBegin.Y - pEnd.Y) * (xmin - point.X) / (pBegin.X - pEnd.X);
                    point.X = xmin;
                }
                else if ((code & RIGHT) != 0)
                {
                    point.Y += (pBegin.Y - pEnd.Y) * (xmax - point.X) / (pBegin.X - pEnd.X);
                    point.X = xmax;
                }

                /* если c ниже r, то передвигаем c на прямую y = r->y_min
                если c выше r, то передвигаем c на прямую y = r->y_max */
                else if ((code & BOTTOM) != 0)
                {
                    point.X += (pBegin.X - pEnd.X) * (ymin - point.Y) / (pBegin.Y - pEnd.Y);
                    point.Y = ymin;
                }
                else if ((code & TOP) != 0)
                {
                    point.X += (pBegin.X - pEnd.X) * (ymax - point.Y) / (pBegin.Y - pEnd.Y);
                    point.Y = ymax;
                }

                if (code == codeBegin)
                {
                    pBegin = point;
                    codeBegin = FindCode(pBegin, xmin, ymin, xmax, ymax);
                }
                else
                {
                    pEnd = point;
                    codeEnd = FindCode(pEnd, xmin, ymin, xmax, ymax);
                }
            }
            
            points.Add(pBegin);
            points.Add(pEnd);
            return points;
        }


        private static int FindCode(Point point, int xmin, int ymin, int xmax, int ymax)
        {
            int code = INSIDE;

            if (point.X < xmin)
                code |= LEFT;
            else if (point.X > xmax)
                code |= RIGHT;

            if (point.Y < ymin)
                code |= BOTTOM;
            else if (point.Y > ymax)
                code |= TOP;

            return code;
        }
    }
}
