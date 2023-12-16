namespace CG_Lab4.Models
{
    public class LayeredImage
    {
        public void SaveImage(string filename)
        {
            FileStream fs = new(filename, FileMode.Create);
            var sw = new StreamWriter(fs);

            foreach (var layer in Layers)
            {
                layer.WriteToFile(sw);
            }

            sw.Close();
        }

        public void LoadImage(string filename)
        {
            Layers.Clear();
            Drawable.Clear();

            try
            {
                using StreamReader sr = new(filename);
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
                        Polygon polygon = new(new Point(x1, y1), new Point(x2, y2), new Point(x3, y3))
                        {
                            FillColor = color
                        };
                        Layer layer = new(polygon);
                        Add(layer);
                    }
                    else
                    {
                        Console.WriteLine("Неверный формат данных в файле");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
            }
        }

        public void ChangeFrame(Rectangle frame)
        {
            _layers.Insert(0, new(frame));
        }

        public void Add(params Layer[] layers) 
        {
            _layers.AddRange(layers);
        }

        public void Remove(Layer layer)
        {
            _layers.Remove(layer);
        }

        public void ClipAllLayers()
        {
            // сверху вниз по слоям
            for (int i = 0; i < Layers.Count - 1; i++)
            {
                Layer layer = Layers[i];
                layer.ClipToLayer(Layers[i + 1]);
            }
        }

        public void ClipAllLayersToFrame()
        {
            for (int i = 0; i < Layers.Count; i++)
            {
                var layer = Layers[i];
                layer.ClipToFrame(Frame);
            }
        }


        public void AddDrawable(IFigure figure)
        {
            if (figure.Borders != null)
            {
                foreach (var point in figure.Borders)
                {
                    if (!Drawable.Contains(point))
                    {
                        Drawable.Add(point);
                    }
                }
            }
            if (figure.Insides != null)
            {
                foreach (var point in figure.Insides)
                {
                    if (!Drawable.Contains(point))
                    {
                        Drawable.Add(point);
                    }
                }
            }
        }

        internal void Rotate(int selectedLayer, double angle)
        {
            var layer = Layers[selectedLayer];

            foreach (var figure in layer.Figures)
            {
                foreach (var point in figure.Insides)
                {
                    Drawable.Remove(point);
                }
                foreach (var point in figure.Borders)
                {
                    Drawable.Remove(point);
                }
            }

            Layers[selectedLayer].Rotate(angle);
        }

        public List<Layer> Layers { get => _layers.Skip(1).ToList(); }
        public Rectangle Frame { get => (Rectangle)_layers[0][0]; set => _layers[0][0] = value; }
        public List<Point> Drawable { get; set; } = new();
        private List<Layer> _layers = new(1);
    }
}
