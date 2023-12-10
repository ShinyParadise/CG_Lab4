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

        public void FloodFill(ref Bitmap image, ref Graphics graphics, Point startPoint, Color fillColor, Color oldColor)
        {
            if (fillColor == oldColor) return;
            Stack<Point> stack = new();
            stack.Push(startPoint);
            
            var brush = new SolidBrush(fillColor);

            while (stack.Count > 0)
            {
                Point currentPoint = stack.Pop();

                int x = (currentPoint.X * _scaleFactor);
                int y = (currentPoint.Y * _scaleFactor);

                if (x < 0 || x >= image.Width || y < 0 || y >= image.Height)
                {
                    continue; // Пропускаем, если координаты за границами изображения
                }

                var currentColor = image.GetPixel(x, y);

                if (currentColor.ToArgb() == oldColor.ToArgb())
                {
                    graphics.FillRectangle(brush, x, y, _scaleFactor, _scaleFactor);

                    var rightNeighbor = new Point(currentPoint.X + 1, currentPoint.Y);
                    var leftNeighbor = new Point(currentPoint.X - 1, currentPoint.Y);
                    var bottomNeighbor = new Point(currentPoint.X, currentPoint.Y + 1);
                    var upperNeighbor = new Point(currentPoint.X, currentPoint.Y - 1);

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
        }
    }
}
