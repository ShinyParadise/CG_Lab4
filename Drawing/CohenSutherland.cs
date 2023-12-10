using CG_Lab4.Models;
using Point = CG_Lab4.Models.Point;
using Rectangle = CG_Lab4.Models.Rectangle;

namespace CG_Lab4.Drawing
{
    public class CohenSutherland
    {
        // Коды для каждого положения точки относительно окна
        private const int LEFT = 1;   // 0001
        private const int RIGHT = 2;  // 0010
        private const int BOTTOM = 4; // 0100
        private const int TOP = 8;    // 1000

        private static int ClipCode(Point point, Rectangle clipRectangle)
        {
            int code = 0;

            if (point.X < clipRectangle.Left)
                code |= LEFT;
            else if (point.X > clipRectangle.Right)
                code |= RIGHT;

            if (point.Y < clipRectangle.Top)
                code |= BOTTOM;
            else if (point.Y > clipRectangle.Bottom)
                code |= TOP;

            return code;
        }

        public static (Point?, Point?) ClipLine(Rectangle clipRectangle, Point p1, Point p2)
        {
            Point? startP = null;
            Point? endP = null;

            int code0 = ClipCode(p1, clipRectangle);
            int code1 = ClipCode(p2, clipRectangle);

            while ((code0 | code1) != 0)
            {
                if ((code0 & code1) != 0)
                {
                    return (startP, endP);
                }

                int x = 0, y = 0;

                int codeOut = (code0 != 0) ? code0 : code1;

                if ((codeOut & TOP) != 0)
                {
                    x = p1.X + (p2.X - p1.X) * (clipRectangle.Bottom - p1.Y) / (p2.Y - p1.Y);
                    y = clipRectangle.Bottom;
                }
                else if ((codeOut & BOTTOM) != 0)
                {
                    x = p1.X + (p2.X - p1.X) * (clipRectangle.Top - p1.Y) / (p2.Y - p1.Y);
                    y = clipRectangle.Top;
                }
                else if ((codeOut & RIGHT) != 0)
                {
                    y = p1.Y + (p2.Y - p1.Y) * (clipRectangle.Right - p1.X) / (p2.X - p1.X);
                    x = clipRectangle.Right;
                }
                else if ((codeOut & LEFT) != 0)
                {
                    y = p1.Y + (p2.Y - p1.Y) * (clipRectangle.Left - p1.X) / (p2.X - p1.X);
                    x = clipRectangle.Left;
                }

                if (codeOut == code0)
                {
                    p1.X = x;
                    p1.Y = y;
                    code0 = ClipCode(p1, clipRectangle);
                }
                else
                {
                    p2.X = x;
                    p2.Y = y;
                    code1 = ClipCode(p2, clipRectangle);
                }
            }

            startP = p1;
            endP = p2;

            return (startP, endP);
        }
    }
}
