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

        public List<Layer> Layers { get => _layers; }
        public Rectangle Frame { get => (Rectangle)_layers[0][0]; set => _layers[0][0] = value; }

        private List<Layer> _layers = new(1);
    }
}
