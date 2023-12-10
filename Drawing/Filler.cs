using Point = CG_Lab4.Models.Point;

namespace CG_Lab4.Drawing
{
    public class Filler
    {
        private int _scaleFactor;

        public Filler(int scaleFactor) 
        {
            _scaleFactor = scaleFactor;
        }

        public List<Point> FloodFill(List<Point> borders, Point startPoint, Color fillColor)
        {
            List<Point> points = new List<Point>();
            Stack<Point> stack = new();
            startPoint.Color = fillColor;
            stack.Push(startPoint);

            int BeginX = startPoint.X;
            int BeginY = startPoint.Y;
            int R, B, G;
            int deltaR, deltaB, deltaG;
            //int step = 1;
            deltaR = 255 - fillColor.R;
            deltaB = 255 - fillColor.B;
            deltaG = 255 - fillColor.G;
            var brush = new SolidBrush(fillColor);

            while (stack.Count > 0)
            {
                Point currentPoint = stack.Pop();
                int x = currentPoint.X;
                int y = currentPoint.Y;

                if (x < 0 || y < 0 )
                {
                    continue; // Пропускаем, если координаты за границами изображения
                }

                if (!borders.Contains(currentPoint) && (!points.Contains(currentPoint)))
                {
                    points.Add(currentPoint);

                    double deltaX = (x - BeginX) * (x - BeginX); double deltaY = (y - BeginY) * (y - BeginY);
                    double distance = Math.Sqrt(deltaX + deltaY);
                    R = (int)(fillColor.R + deltaR * distance / 5000) % 256;
                    G = (int)(fillColor.G + deltaG * distance / 5000) % 256;
                    B = (int)(fillColor.B + deltaB * distance / 5000) % 256;
                    fillColor = Color.FromArgb(R, G, B);

                    var rightNeighbor = new Point(currentPoint.X + 1, currentPoint.Y, fillColor);
                    var leftNeighbor = new Point(currentPoint.X - 1, currentPoint.Y, fillColor);
                    var bottomNeighbor = new Point(currentPoint.X, currentPoint.Y + 1, fillColor);
                    var upperNeighbor = new Point(currentPoint.X, currentPoint.Y - 1, fillColor);

                    if (!stack.Contains(rightNeighbor))
                        stack.Push(rightNeighbor);

                    if (!stack.Contains(leftNeighbor))
                        stack.Push(leftNeighbor);

                    if (!stack.Contains(bottomNeighbor))
                        stack.Push(bottomNeighbor);

                    if (!stack.Contains(upperNeighbor))
                        stack.Push(upperNeighbor);
                }
            }
            return points;
        }
    }
}
