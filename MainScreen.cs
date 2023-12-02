using CG_Lab4.Drawing;
using CG_Lab4.Models;
using CG_Lab4.Utils;
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
        private Color bgColor = Color.LightGray;

        private const int scaleFactor = 4;
        private LineDrawer lineDrawer = new();
        private Filler filler = new(scaleFactor);

        private string inputPath = "../../../triangles_color.txt";

        public MainScreen()
        {
            InitializeComponent();

            InitCanvas();

            DrawFrame();

            InitTriangles();

            DrawAll();
        }

        private void DrawAll()
        {
            ClipAllLayersToFrame();

            foreach (var layer in layeredImage.Layers)
            {
                foreach (var fig in layer.Figures)
                {
                    if (fig.Points.Count != 0)
                    {
                        DrawFigure(fig);
                        FillFigure(fig);
                    }
                }
            }
        }

        private void ClipAllLayersToFrame()
        {
            for (int i = 1; i < layeredImage.Layers.Count; i++)
            {
                var layer = layeredImage.Layers[i];
                ClipToFrame(layer);
            }
        }

        private void ClipToFrame(Layer layer)
        {
            for (int i = 0; i < layer.Figures.Count; i++)
            {
                IFigure fig = layer.Figures[i];
                fig.ClipToFrame(layeredImage.Frame);
            }
        }

        private void FillFigure(IFigure figure)
        {
            var insidePoint = PolygonPointGenerator.GenerateRandomPointInsidePolygon(figure.Points);
            filler.FloodFill(ref bitmap, ref graphics, insidePoint, figure.FillColor, bgColor);
        }

        private void DrawFrame()
        {
            /*var windowRect = pictureBox1.ClientRectangle;*/
            var a = new Point(40, 40);        // left top
            var b = new Point(180, 40);      // right top
            var c = new Point(180, 100);    // right bot
            var d = new Point(40, 100);      // left bot

            var frame = new Rectangle(a, b, c, d);
            layeredImage.ChangeFrame(frame);

            var points = layeredImage.Frame.Points;
            DrawPoints(points, new SolidBrush(Color.Tan));
        }
        
        private void DrawFigure(IFigure f)
        {
            var brush = new SolidBrush(Color.Black);
            DrawPoints(f.Points, brush);
        }

        private void DrawPoints(List<Point> points, SolidBrush? brush = null)
        {
            brush ??= new SolidBrush(Color.Black);

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
                graphics.FillRectangle(brush, point.X * scaleFactor, point.Y * scaleFactor, scaleFactor, scaleFactor);
            }
        }

        private void InitCanvas()
        {
            SystemRectangle rectangle = pictureBox1.ClientRectangle;
            bitmap = new Bitmap(rectangle.Width, rectangle.Height);
            graphics = Graphics.FromImage(bitmap);
            pictureBox1.Image = bitmap;
            graphics.Clear(bgColor);
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
                        Color color = Color.FromName(numbersAsString[6]);

                        if (int.TryParse(numbersAsString[0], out int x1) &&
                            int.TryParse(numbersAsString[1], out int y1) &&
                            int.TryParse(numbersAsString[2], out int x2) &&
                            int.TryParse(numbersAsString[3], out int y2) &&
                            int.TryParse(numbersAsString[4], out int x3) &&
                            int.TryParse(numbersAsString[5], out int y3))
                        {
                            Triangle triangle = new(new Point(x1, y1), new Point(x2, y2), new Point(x3, y3));
                            triangle.FillColor = color;
                            triangles.Add(triangle);
                            Layer layer = new(triangle);
                            layeredImage.Add(layer);
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