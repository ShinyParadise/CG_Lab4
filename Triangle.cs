namespace CG_Lab4
{
    public class Triangle
    {
        public PointF p1 { get; set; }
        public PointF p2 { get; set; }
        public PointF p3 { get; set; }

        public Triangle(PointF p1, PointF p2, PointF p3)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
        }
    }
}
