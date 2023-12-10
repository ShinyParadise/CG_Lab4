using CG_Lab4.Drawing;

namespace CG_Lab4.Models
{
    public class Layer
    {
        public Layer() { }

        public Layer(params IFigure[] figures) 
        {
            _figures.AddRange(figures);
        }

        public void Add(params IFigure[] figures)
        {
            _figures.AddRange(figures);
        }

        public void Remove(IFigure figure)
        {
            _figures.Remove(figure);
        }

        public void ClipToFrame(Rectangle frame)
        {
            for (int i = 0; i < Figures.Count; i++)
            {
                IFigure fig = Figures[i];
                fig.ClipToFrame(frame);
            }
        }

        public void ClipToLayer(Layer layer)
        {
            var toRemove = new List<IFigure>();
            var toAdd = new List<IFigure>();
            for (int i = 0; i < _figures.Count; i++) 
            {
                IFigure fig = _figures[i];
                foreach (var otherLayerFig in layer.Figures) 
                {
                    var polygons = WeilerAtherton.Clip(fig, otherLayerFig);
                    if (polygons.Count > 0)
                    {
                        toRemove.Add(fig);
                        toAdd.AddRange(polygons);
                    }
                }
            }

            toRemove.ForEach(fig => Remove(fig));
            toAdd.ForEach(fig => Add(fig));
        }

        public IFigure this[int index]
        {
            get => _figures[index];
            set => _figures[index] = value;
        }


        private List<IFigure> _figures = new(1);

        public List<IFigure> Figures { get => _figures; set => _figures = value; }
    }
}
