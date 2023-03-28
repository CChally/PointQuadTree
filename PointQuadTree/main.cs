using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driver
{
    using PointQuadTree;
    class Driver
    {
        static void Main()
        {
            PointQuadTree PQ = new PointQuadTree(2); // Init with 2D
            var PointSet = new Point[10]; // Set of Points

            Point p = new Point(3);

            PQ.Insert(p);


        }
    }
}
