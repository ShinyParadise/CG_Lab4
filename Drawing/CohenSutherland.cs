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

        private static int ComputeOutCode(Rectangle clipRectangle, Point p)
        {
            int code = INSIDE;

            if (p.X < clipRectangle.Left)
                code |= LEFT;
            else if (p.X > clipRectangle.Right)
                code |= RIGHT;

            if (p.Y < clipRectangle.Top)
                code |= TOP;
            else if (p.Y > clipRectangle.Bottom)
                code |= BOTTOM;

            return code;
        }

        // Отсечение отрезка прямоугольным окном
        public static bool ClipLine(Rectangle clipRectangle, ref Point p1, ref Point p2)
        {
            int outcode1 = ComputeOutCode(clipRectangle, p1);
            int outcode2 = ComputeOutCode(clipRectangle, p2);
            bool accept = false;

            while (true)
            {
                if ((outcode1 | outcode2) == 0)
                {
                    accept = true;
                    break;
                }
                else if ((outcode1 & outcode2) != 0)
                {
                    break;
                }
                else
                {
                    int x, y;
                    int outcodeOut = (outcode1 != 0) ? outcode1 : outcode2;

                    if ((outcodeOut & TOP) != 0)
                    {
                        x = p1.X + (p2.X - p1.X) * (clipRectangle.Top - p1.Y) / (p2.Y - p1.Y);
                        y = clipRectangle.Top;
                    }
                    else if ((outcodeOut & BOTTOM) != 0)
                    {
                        x = p1.X + (p2.X - p1.X) * (clipRectangle.Bottom - p1.Y) / (p2.Y - p1.Y);
                        y = clipRectangle.Bottom;
                    }
                    else if ((outcodeOut & RIGHT) != 0)
                    {
                        y = p1.Y + (p2.Y - p1.Y) * (clipRectangle.Right - p1.X) / (p2.X - p1.X);
                        x = clipRectangle.Right;
                    }
                    else
                    {
                        y = p1.Y + (p2.Y - p1.Y) * (clipRectangle.Left - p1.X) / (p2.X - p1.X);
                        x = clipRectangle.Left;
                    }

                    if (outcodeOut == outcode1)
                    {
                        p1 = new Point(x, y);
                        outcode1 = ComputeOutCode(clipRectangle, p1);
                    }
                    else
                    {
                        p2 = new Point(x, y);
                        outcode2 = ComputeOutCode(clipRectangle, p2);
                    }
                }
            }

            return accept;
        }
    }
}
