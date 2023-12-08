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

        public void AddInsides(params IFigure[] figures)
        {
            _insides.AddRange(figures);
        }


        public void AddNotFilled(params Point[] points)
        {
            _notFilled.AddRange(points);
        }

        public IFigure this[int index]
        {
            get => _figures[index];
            set => _figures[index] = value;
        }


        private List<IFigure> _figures = new(1);
        private List<IFigure> _insides = new(1);
        private List<Point> _notFilled = new(1);
        public List<IFigure> Figures { get => _figures; set => _figures = value; }
        public List<IFigure> Insides { get => _insides; set => _insides = value; }
        public List<Point> NotFilled { get => _notFilled; set => _notFilled = value; }
    }
}
