namespace CG_Lab4.Drawing
{
    public class Filler
    {
        private int _scaleFactor;

        public Filler(int scaleFactor) 
        {
            _scaleFactor = scaleFactor;
        }

        public List<Point> FloodFill(Bitmap image, Point startPoint, Color fillColor)
        {
            var fillPoints = new List<Point>();
            Color oldColor = image.GetPixel(startPoint.X, startPoint.Y);
            if (oldColor == fillColor) return fillPoints;

            List<Point> filledPoints = new List<Point>();
            Stack<Point> stack = new Stack<Point>();
            stack.Push(startPoint);

            while (stack.Count > 0)
            {
                Point point = stack.Pop();
                int x = point.X;
                int y = point.Y;

                if (x < 0 || x >= image.Width || y < 0 || y >= image.Height)
                    continue;

                if (image.GetPixel(x * _scaleFactor, y * _scaleFactor) == oldColor)
                {
                    filledPoints.Add(new Point(x, y));

                    stack.Push(new Point(x + 1, y));
                    stack.Push(new Point(x - 1, y));
                    stack.Push(new Point(x, y + 1));
                    stack.Push(new Point(x, y - 1));
                }
            }

            return fillPoints;
        }
    }
}
