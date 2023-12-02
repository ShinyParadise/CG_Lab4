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

        public void AddCutted(params IFigure[] figures)
        {
            _cutFigures.AddRange(figures);
        }

        public void RemoveCutted(IFigure figure)
        {
            _cutFigures.Remove(figure);
        }


        private List<IFigure> _figures = new();
        private List<IFigure> _cutFigures = new();
        public List<IFigure> Figures { get => _figures; set => _figures = value; }
        public List<IFigure> CutFigures { get => _cutFigures; set => _cutFigures = value; }

    }
}
