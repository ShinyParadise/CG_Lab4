using CG_Lab4.Drawing;
using CG_Lab4.Models;
using Point = CG_Lab4.Models.Point;
using Rectangle = CG_Lab4.Models.Rectangle;
using SystemRectangle = System.Drawing.Rectangle;

namespace CG_Lab4
{
    public partial class MainScreen : Form
    {
        private LayeredImage layeredImage = new();

        private Graphics graphics;
        private Bitmap bitmap;
        private Color bgColor = Color.White;
        private Color borderColor = Color.Black;

        private const int scaleFactor = 3;
        private LineDrawer lineDrawer = new();
        private Filler filler = new(scaleFactor);

        double angle = 0;
        int selectedLayer = 0;

        private string inputPath = "../../../triangles_color.txt";

        public MainScreen()
        {
            InitializeComponent();

            InitTriangles();

            InitCanvas();

            DrawFrame();

            DrawAll();
        }

        private void DrawAll()
        {
            layeredImage.ClipAllLayersToFrame();
            //layeredImage.ClipAllLayers();

            foreach (var layer in layeredImage.Layers)
            {
                foreach (var fig in layer.Figures)
                {
                    if (fig.Points.Count != 0)
                    {
                        fig.Borders = DrawPoints(fig.Points);
                        fig.Insides = FillFigure(fig);
                        layeredImage.AddDrawable(fig);
                    }
                }
            }
            foreach (var p in layeredImage.Drawable)
            {
                Brush brush = new SolidBrush(p.Color);
                graphics.FillRectangle(brush, p.X * scaleFactor, p.Y * scaleFactor, scaleFactor, scaleFactor);
            }
        }

        private List<Point> FillFigure(IFigure figure)
        {
            var insidePoint = figure.GeneratePointInside();
            if (insidePoint != null)
            {
                return filler.FloodFill(figure.Borders, (Point)insidePoint, figure.FillColor);
            }
            else
            {
                return new();
            }
        }

        private void DrawFrame()
        {
            /*var windowRect = pictureBox1.ClientRectangle;*/
            var a = new Point(0, 0);          // left top
            var b = new Point(150, 0);         // right top
            var c = new Point(150, 150);        // right bot
            var d = new Point(0, 150);         // left bot

            var frame = new Rectangle(a, b, c, d);
            layeredImage.ChangeFrame(frame);

            var points = layeredImage.Frame.Points;
            layeredImage.Drawable = DrawPoints(points, Color.Tan);
        }

        private List<Point> DrawPoints(List<Point> points, Color? color = null)
        {
            color ??= borderColor;
            var linesPoints = new List<Point>();

            for (int i = 0; i < points.Count - 1; i++)
            {
                var newPoints = lineDrawer.DrawLine(points[i], points[i + 1]);
                linesPoints.AddRange(newPoints);
            }
            var lastLine = lineDrawer.DrawLine(points.Last(), points.First());
            linesPoints.AddRange(lastLine);

            return linesPoints;
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
            layeredImage.LoadImage(inputPath);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            _ = int.TryParse(textBox2.Text, out selectedLayer);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _ = double.TryParse(textBox1.Text, out angle);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitCanvas();

            layeredImage.Rotate(selectedLayer, angle);

            DrawAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using SaveFileDialog saveFileDialog = new() { Filter = @"text|*.txt" };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                layeredImage.SaveImage(saveFileDialog.FileName);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using OpenFileDialog openFileDialog = new() { Filter = @"text|*.txt" };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                InitCanvas();
                layeredImage.LoadImage(openFileDialog.FileName);
                DrawAll();
            }
        }
    }
}