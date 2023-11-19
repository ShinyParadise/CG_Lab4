namespace CG_Lab4
{
    public class Layer
    {
        private List<Triangle> figures = new List<Triangle>();

        public Layer() {}

        public void Add(Triangle triangle)
        {
            figures.Add(triangle);
        }

        public void Remove(Triangle triangle) 
        {
            figures.Remove(triangle);
        }

        public Triangle this[int index]
        {
            get => figures[index];
            set => figures[index] = value;
        }
    }
}
