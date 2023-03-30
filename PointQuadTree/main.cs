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
            PointQuadTree PQ = new PointQuadTree(2);    // Init with specified dimension (1D, 2D, 3D ... ND)
            Point p;
            Random rand = new Random();     // For generating random point values

            // Create 15 random points

            for (int i = 0; i < 15; i++)   
            {
                p = new Point(2);           // Zero Point

                for (int j = 0; j < PQ.dimension; j++)  // Assign values to zero point
                {
                    p.Set(j, rand.Next(-50, 50));   // Lower bound of -50, upper bound of 50, for each dimension
                }

                // Insert into PointQuad
                PQ.Insert(p);   
            }

            PQ.PrintQuadTree();
            Console.ReadKey();
        }
    }
}
