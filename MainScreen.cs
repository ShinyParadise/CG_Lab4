using CG_Lab4.Drawing;
using CG_Lab4.Models;
using Point = CG_Lab4.Models.Point;
using Rectangle = CG_Lab4.Models.Rectangle;
using SystemRectangle = System.Drawing.Rectangle;

namespace CG_Lab4
{
    public partial class MainScreen : Form
    {
        private List<Triangle> triangles = new();
        private LayeredImage layeredImage = new();

        private Graphics graphics;
        private Bitmap bitmap;

        private const int scaleFactor = 5;
        private LineDrawer lineDrawer = new(scaleFactor);
        private Filler filler = new(scaleFactor);

        private string inputPath = "../../../triangles.txt";

        public MainScreen()
        {
            InitializeComponent();

            InitCanvas();

            InitTriangles();

            DrawAll();
        }

        private void DrawAll()
        {
            DrawFrame();

            foreach (var tri in triangles)
            {
                DrawTriangle(tri);
            }
        }

        private void DrawFrame()
        {
            /*var windowRect = pictureBox1.ClientRectangle;*/
            var p1 = new Point(0, 0);
            var p2 = new Point(200, 0);
            var p3 = new Point(200, 170);
            var p4 = new Point(0, 170);

            var frame = new Rectangle(p1, p2, p3, p4);
            layeredImage.ChangeFrame(frame);

            var points = layeredImage.Frame[0].Points;
            DrawPoints(points, new SolidBrush(Color.Tan));
        }
        
        private void DrawTriangle(Triangle t)
        {
            var brush = new SolidBrush(Color.Black);
            DrawPoints(t.Points, brush);
        }

        private void DrawPoints(List<Point> points, SolidBrush? brush = null)
        {
            if (brush == null) brush = new SolidBrush(Color.Black);

            var linesPoints = new List<Point>();
            
            for(int i = 0; i < points.Count - 1; i++)
            {
                var newPoints = lineDrawer.DrawLine(points[i], points[i + 1]);
                linesPoints.AddRange(newPoints);
            }
            var lastLine = lineDrawer.DrawLine(points.Last(), points.First());
            linesPoints.AddRange(lastLine);

            foreach (var point in linesPoints)
            {
                graphics.FillRectangle(brush, point.X, point.Y, scaleFactor, scaleFactor);
            }
        }

        private void InitCanvas()
        {
            SystemRectangle rectangle = pictureBox1.ClientRectangle;
            bitmap = new Bitmap(rectangle.Width, rectangle.Height);
            graphics = Graphics.FromImage(bitmap);
            pictureBox1.Image = bitmap;
        }

        private void InitTriangles()
        {
            try
            {
                using (StreamReader sr = new StreamReader(inputPath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        if (line == null) return;

                        string[] numbersAsString = line.Split(' ', '\t', '\n', '\r');

                        if (int.TryParse(numbersAsString[0], out int x1) &&
                            int.TryParse(numbersAsString[1], out int y1) &&
                            int.TryParse(numbersAsString[2], out int x2) &&
                            int.TryParse(numbersAsString[3], out int y2) &&
                            int.TryParse(numbersAsString[4], out int x3) &&
                            int.TryParse(numbersAsString[5], out int y3))
                        {
                            triangles.Add(new(new Point(x1, y1), new Point(x2, y2), new Point(x3, y3)));
                        }
                        else
                        {
                            Console.WriteLine("Неверный формат данных в файле");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
            }
        }
    }
}