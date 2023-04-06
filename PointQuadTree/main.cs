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


            // Create 10 random points

            var delete = new Point(2);
            delete.Set(0, -20);
            delete.Set(1, 20);

            PQ.Insert(delete);

            for (int i = 0; i < 10; i++)   
            {
                p = new Point(2);           // Zero Point

                for (int j = 0; j < PQ.dimension; j++)  // Assign values to zero point
                    p.Set(j, rand.Next(-50,50));   // Lower bound 0, upper bound of 100
                
                // Insert into PointQuad
                PQ.Insert(p);   
            }

            p = new Point(2);
            p.Set(0, -50);
            p.Set(1, 0);
        
            var d = new Point(2);
            d.Set(0, 50);
            d.Set(1, 50);

           
            PQ.Insert(d); // farthest right
            PQ.Insert(p); // farthest left

            PQ.PrintQuadTree();
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine(PQ.Contains(p));
            Console.WriteLine(PQ.Contains(d));

            PQ.Delete(p);
            PQ.Delete(d);
            PQ.Delete(delete);

            PQ.PrintQuadTree();
            Console.WriteLine(PQ.Contains(p));
            Console.WriteLine(PQ.Contains(d));

            Console.ReadKey();
        }
    }
}
