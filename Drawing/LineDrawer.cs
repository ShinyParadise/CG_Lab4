namespace CG_Lab4.Drawing
{
    public class LineDrawer
    {
        private int _scaleFactor;

        public LineDrawer(int scaleFactor)
        {
            _scaleFactor = scaleFactor;
        }

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
                linePoints.Add(new Point(x1 * _scaleFactor, y1 * _scaleFactor));

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
