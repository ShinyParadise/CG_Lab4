﻿namespace CG_Lab4.Models
{
    public class Triangle : IFigure
    {
        public Triangle(Point p1, Point p2, Point p3)
        {
            _points = new List<Point> { p1, p2, p3 };
        }

        public List<Point> Points => _points;
        public Color FillColor { get => _fillColor; set => _fillColor = value; }

        private List<Point> _points;
        private Color _fillColor;
    }
}
