using CG_Lab4.Drawing;
using CG_Lab4.Models;
using System.IO;

namespace CG_Lab4
{
    public partial class MainScreen : Form
    {
        private List<Triangle> triangles = new List<Triangle>();

        private Graphics graphics;
        private Bitmap bitmap;

        private const int scaleFactor = 5;
        private LineDrawer lineDrawer = new LineDrawer(scaleFactor);
        private Filler filler = new Filler(scaleFactor);

        string inputPath = "../../../triangles.txt";

        public MainScreen()
        {
            InitializeComponent();

            InitCanvas();

            InitTriangles();

            DrawAll();
        }

        private void DrawAll()
        {
            foreach (var tri in triangles) 
            {
                DrawTriangle(tri);
            }
        }

        private void DrawTriangle(Triangle t)
        {
            var points = lineDrawer.DrawLine(t.p1, t.p2);
            points.AddRange(lineDrawer.DrawLine(t.p2, t.p3));
            points.AddRange(lineDrawer.DrawLine(t.p3, t.p1));

            var brush = new SolidBrush(Color.Black);

            foreach (var point in points)
            {
                graphics.FillRectangle(brush, point.X, point.Y, scaleFactor, scaleFactor);
            }
        }

        private void InitCanvas()
        {
            Rectangle rectangle = pictureBox1.ClientRectangle;
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