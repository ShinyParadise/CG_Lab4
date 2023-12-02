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

        public IFigure this[int index]
        {
            get => _figures[index];
            set => _figures[index] = value;
        }


        private List<IFigure> _figures = new(1);

        public List<IFigure> Figures { get => _figures; set => _figures = value; }
    }
}
