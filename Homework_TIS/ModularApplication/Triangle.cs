using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularApplication
{
    class Triangle
    {
        public List<Point> Vertices { get; private set; }
        public List<double> Lengths { get; private set; }
        public double Square { get; private set; }
        public Triangle(List<Point> vertices)
        {
            Vertices = new List<Point>(vertices);
            Lengths = new List<double>(3);

			CalculateLengths();
			CalculateSquare();
        }
		private void CalculateLengths()
		{
			for (int point = 0; point < 3; ++point)
			{
				Lengths.Add(Point.GetDistance(Vertices[point], Vertices[(point + 1) % 3]));
			}
		}
        private void CalculateSquare()
        {
			double semiPerimeter = CalculatePerimeter() / 2.0;

            Square = Math.Sqrt(semiPerimeter * (semiPerimeter - Lengths[0]) * (semiPerimeter - Lengths[1]) * (semiPerimeter - Lengths[2]));
        }
		private double CalculatePerimeter()
		{
			double perimeter = 0.0;
			foreach (double length in Lengths)
			{
				perimeter += length;
			}

			return perimeter;
		}
        static public Triangle GetMax(List<Triangle> triangles)
        {
            Triangle max = null;
            foreach(Triangle triangle in triangles)
            {
                if(max == null || max.Square < triangle.Square)
                {
                    max = triangle;
                }
            }

            return max;
        }
        static public IEnumerable<Triangle> SplitToTriangles(List<Point> points)
        {
            List<Point> tempPoints = new List<Point>();
            for(int pointIndex = 0; pointIndex < (points.Count / 3) * 3; ++pointIndex)
            {
                tempPoints.Add(points[pointIndex]);

                if (pointIndex % 3 == 2)
                {
					yield return new Triangle(tempPoints);
                    tempPoints.Clear();
                }
            }
        }
        public override string ToString()
        {
            return $"A : {Vertices[0]}\nB : {Vertices[1]}\nC : {Vertices[2]}\n" +
                   $"a = {Math.Round(Lengths[0], 4)}\tb = {Math.Round(Lengths[1], 4)}\tc = {Math.Round(Lengths[2], 4)}\n" +
                   $"S = {Math.Round(Square), 4}\n";
        }
    }
}
