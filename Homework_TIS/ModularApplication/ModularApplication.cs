using System;
using System.Collections.Generic;

namespace ModularApplication
{
    class ModularApplication
    {
        static void Main(string[] args)
        {
			List<Point> points = new List<Point>(Point.GetPoints("Test.txt"));
            List<Triangle> triangles = new List<Triangle>(Triangle.SplitToTriangles(points));

            foreach(Triangle triangle in triangles)
            {
                Console.WriteLine(triangle);
            }

            Console.WriteLine("Max:\n" + Triangle.GetMax(triangles));
        }
    }
}
