using CG_Lab4.Models;
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

        public static void ClipToRectangle(Triangle triangle, Rectangle rect)
        {
            for (int i = 0; i < triangle.Points.Count; i++)
            {
                // TODO: проверить правильность передачи точек
                ClipPoint(triangle.Points, i, rect.Points[3].X, rect.Points[1].Y, rect.Points[1].X, rect.Points[3].Y);
            }
        }

        private static void ClipPoint(List<Point> vertices, int index, int xmin, int ymin, int xmax, int ymax)
        {
            int code = INSIDE;
            Point p = vertices[index];

            if (p.X < xmin)
                code |= LEFT;
            else if (p.X > xmax)
                code |= RIGHT;

            if (p.Y < ymin)
                code |= BOTTOM;
            else if (p.Y > ymax)
                code |= TOP;

            switch (code)
            {
                case INSIDE:
                    break;
                default:
                    vertices[index] = new Point
                    {
                        X = Math.Clamp(p.X, xmin, xmax),
                        Y = Math.Clamp(p.Y, ymin, ymax)
                    };
                    break;
            }
        }
    }
}
