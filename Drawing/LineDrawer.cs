﻿using Point = CG_Lab4.Models.Point;

namespace CG_Lab4.Drawing
{
    public class LineDrawer
    {
        public List<Point> DrawLine(Point point1, Point point2)
        {
            List<Point> linePoints = new List<Point>();

            int x1 = point1.X;
            int y1 = point1.Y;
            int x2 = point2.X;
            int y2 = point2.Y;

            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);
            int sx = (x1 < x2) ? 1 : -1;
            int sy = (y1 < y2) ? 1 : -1;

            int err = dx - dy;

            while (true)
            {
                linePoints.Add(new Point(x1, y1));

                if (x1 == x2 && y1 == y2)
                    break;

                int err2 = 2 * err;

                if (err2 > -dy)
                {
                    err -= dy;
                    x1 += sx;
                }

                if (err2 < dx)
                {
                    err += dx;
                    y1 += sy;
                }
            }

            return linePoints;
        }
    }
}
