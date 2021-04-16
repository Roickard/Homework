using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModularApplication
{
    class Point
    {
        public double X { get; private set; }
        public double Y { get; private set; }
		public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
		static public double GetDistance(Point firstPoint, Point secondPoint)
		{
			return firstPoint.GetDistance(secondPoint);
		}
		public double GetDistance(Point finishPoint)
		{
			double xLength = Math.Abs(X - finishPoint.X);
			double yLength = Math.Abs(Y - finishPoint.Y);

			return Math.Sqrt(Math.Pow(xLength, 2.0) + Math.Pow(yLength, 2.0));
		}
		static public IEnumerable<Point> GetPoints(string filePath)
		{
			using (FileStream file = new FileStream(filePath, FileMode.OpenOrCreate))
			{
				using (StreamReader stream = new StreamReader(file))
				{
					while (!stream.EndOfStream)
					{
						string[] coordinates = stream.ReadLine().Split(';');
						yield return new Point(double.Parse(coordinates[0]), double.Parse(coordinates[1]));
					}
				}
			}
		}
		public override string ToString()
        {
            return $"x = {Math.Round(X, 4)}\ty = {Math.Round(Y, 4)}";
        }
    }
}
