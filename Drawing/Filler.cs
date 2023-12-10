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
            stack.Push(startPoint);
            
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
                    points.Add(new Point(currentPoint.X, currentPoint.Y, fillColor));

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
            return points;
        }
    }
}
