namespace CG_Lab4.Models
{
    public class LayeredImage
    {
        public LayeredImage() { }

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
            for (int i = Layers.Count - 1; i >= 1; i--)
            {
                Layer layer = Layers[i];
                layer.ClipToLayer(Layers[i - 1]);
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

        public List<Layer> Layers { get => _layers.Skip(1).ToList(); }
        public Rectangle Frame { get => (Rectangle)_layers[0][0]; set => _layers[0][0] = value; }

        private List<Layer> _layers = new(1);
    }
}
