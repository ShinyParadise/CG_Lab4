namespace CG_Lab4.Models
{
    public interface IFigure
    {
        public List<Point> Points { get; }
        public Color FillColor { get; set; }
    }
}
