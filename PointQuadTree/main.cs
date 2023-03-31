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
            PointQuadTree PQ = new PointQuadTree(3);    // Init with specified dimension (1D, 2D, 3D ... ND)
            Point p;
            Random rand = new Random();     // For generating random point values

           
            // Create 10 random points

            for (int i = 0; i < 10; i++)   
            {
                p = new Point(3);           // Zero Point

                for (int j = 0; j < PQ.dimension; j++)  // Assign values to zero point
                {
                    p.Set(j, rand.Next(-50,50));   // Lower bound 0, upper bound of 100
                }

                // Insert into PointQuad
                PQ.Insert(p);   
            }
          
            PQ.PrintQuadTree();
            Console.ReadKey();
        }
    }
}
