using Rectangle = CG_Lab4.Models.Rectangle;
using Point = CG_Lab4.Models.Point;

namespace CG_Lab4.Drawing
{
    public class LSSB
    {
        public static (Point?, Point?) Clip(Point p1, Point p2, Rectangle clipRect)
        {
            int c1, c2;
            int dx, dy, k, m, z;

            int Code(Point p)
            {
                int code = 0;
                if (p.X < clipRect.Left) code = 1;
                else if (p.X > clipRect.Right) code = 2;
                if (p.Y < clipRect.Top) code |= 4;
                else if (p.Y > clipRect.Bottom) code |= 8;
                return code;
            }

            c1 = Code(p1);
            c2 = Code(p2);

            if ((c1 & c2) != 0)
            {
                // The line segment is outside
                return (null, null);
            }

            if ((c1 | c2) == 0)
            {
                // The line segment is inside of the clipping rectangle
                return (p1, p2);
            }

            dx = p2.X - p1.X;
            dy = p2.Y - p1.Y;

            switch (c1 + c2)
            {
                case 1: case 2: case 4: case 8:
                    if (c1 == 1)
                    {
                        p1.X = clipRect.Left;
                        p1.Y = (clipRect.Left - p2.X) * dy / dx + p2.Y;
                    }
                    else
                    {
                        p2.X = clipRect.Left;
                        p2.Y = (clipRect.Left - p1.X) * dy / dx + p1.Y;
                    }
                    break;

                case 3: case 12:
                    k = dy / dx;
                    p1.Y = (clipRect.Left - p1.X) * k + p1.Y;
                    p1.X = clipRect.Left;
                    p2.Y = (clipRect.Right - clipRect.Left) * k + p1.Y;
                    p2.X = clipRect.Right;
                    break;

                case 5: case 6: case 9: case 10:
                    k = dy / dx;
                    z = (clipRect.Left - p1.X) * k + p1.Y;
                    if (z < clipRect.Top)
                    {
                        switch (c1)
                        {
                            case 0:
                                p2.X += (clipRect.Top - p2.Y) / k;
                                p2.Y = clipRect.Top;
                                break;
                            case 5:
                                p1.X += (clipRect.Top - p1.Y) / k;
                                p1.Y = clipRect.Top;
                                break;
                            default:
                                return (null, null); // the line segment is outside
                        }
                    }
                    else
                    {
                        switch (c1)
                        {
                            case 0:
                                p2.X = clipRect.Left;
                                p2.Y = z;
                                break;
                            case 1:
                                p2.X += (clipRect.Top - p2.Y) / k;
                                p2.Y = clipRect.Top;
                                p1.X = clipRect.Left;
                                p1.Y = z;
                                break;
                            case 4:
                                p1.X += (clipRect.Top - p1.Y) / k;
                                p1.Y = clipRect.Top;
                                p2.X = clipRect.Left;
                                p2.Y = z;
                                break;
                            case 5:
                                p1.X = clipRect.Left;
                                p1.Y = z;
                                break;
                        }
                    }
                    break;

                case 7: case 11: case 13: case 14:
                    switch (c1)
                    {
                        case 1:
                            k = dy / dx;
                            p1.Y = (clipRect.Left - p2.X) * k + p2.Y;
                            if (p1.Y < clipRect.Top)
                            {
                                return (null, null); // the line segment is outside
                            }
                            p2.Y = (clipRect.Right - clipRect.Left) * k + p1.Y;
                            if (p2.Y < clipRect.Top)
                            {
                                p2.X = (clipRect.Top - p2.Y) / k + clipRect.Right;
                                p2.Y = clipRect.Top;
                            }
                            else
                            {
                                p2.X = clipRect.Right;
                            }
                            break;

                        // Аналогично для случаев c1 = 2, 5, 6

                        default:
                            break;
                    }
                    break;
                case 15:
                    if (dy * (clipRect.Right - clipRect.Left) < dx * (clipRect.Bottom - clipRect.Top))
                    {
                        k = dy / dx;
                        p1.Y = (clipRect.Left - p2.X) * k + p2.Y;
                        if (p1.Y > clipRect.Bottom)
                        {
                            return (p1, p2);
                        }
                        p2.Y = (clipRect.Right - clipRect.Left) * k + p1.Y;
                        if (p2.Y < clipRect.Top)
                        {
                            return (p1, p2);
                        }
                        if (p1.Y < clipRect.Top)
                        {
                            p1.X = (clipRect.Top - p1.Y) / k + clipRect.Left;
                            p1.Y = clipRect.Top;
                            p2.X = clipRect.Right;
                            p2.Y = clipRect.Bottom;
                        }
                        else
                        {
                            p1.X = clipRect.Left;
                            if (p2.Y > clipRect.Bottom)
                            {
                                p2.X = (clipRect.Bottom - p2.Y) / k + clipRect.Right;
                                p2.Y = clipRect.Bottom;
                            }
                            else
                            {
                                p2.X = clipRect.Right;
                            }
                        }
                    }
                    else
                    {
                        m = dx / dy;
                        p1.X = (clipRect.Top - p2.Y) * m + p2.X;
                        if (p1.X > clipRect.Right)
                        {
                            return (p1, p2);
                        }
                        p2.X = (clipRect.Bottom - clipRect.Top) * m + p1.X;
                        if (p2.X < clipRect.Left)
                        {
                            return (p1, p2);
                        }
                        if (p1.X < clipRect.Left)
                        {
                            p1.Y = (clipRect.Left - p1.X) / m + clipRect.Top;
                            p1.X = clipRect.Left;
                            p2.Y = clipRect.Bottom;
                            p2.X = clipRect.Right;
                        }
                        else
                        {
                            p1.Y = clipRect.Top;
                            if (p2.X > clipRect.Right)
                            {
                                p2.Y = (clipRect.Right - p2.X) / m + clipRect.Bottom;
                                p2.X = clipRect.Right;
                            }
                            else
                            {
                                p2.Y = clipRect.Bottom;
                            }
                        }
                    }
                    break;

                // Add other cases as in the pseudocode

                default:
                    break;
            }

            return (p1, p2);
        }
    }
}
