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
            int dimension = 2; // Set dimension

            PointQuadTree PQ = new PointQuadTree(dimension);    // Init with specified dimension (1D, 2D, 3D ... ND)
            Point p;
            List<Point> pointSet = new List<Point>(); // Point storage
            Random rand = new Random();     // For generating random point values

            Point root = new Point(dimension); // Random root so I can test deleting the root later in the code
            for (int i = 0; i < dimension; i++)
            {
                root.Set(i, rand.Next(-50, 50)); // Just so I can reference it later
            }
            PQ.Insert(root);

            // Create 10 random points
            for (int i = 0; i < 10; i++)   
            {
                p = new Point(dimension);           // Zero Point

                for (int j = 0; j < PQ.dimension; j++)  // Assign values to zero point
                    p.Set(j, rand.Next(-50,50));   // Lower bound 0, upper bound of 100

                // Insert into PointQuad
                pointSet.Add(p);
                PQ.Insert(p);   
            }

            // Test leaf node deletions
            // Give min and max points to appear on the far right and far left in console

            Point min = new Point(dimension);
            Point max = new Point(dimension);

            for(int i = 0; i<dimension; i++)
            {
                min.Set(i, -50); // Set mins
                max.Set(i, 50); // Set maxs
            }
            Console.WriteLine($"Max : {max.ToString()}");
            Console.WriteLine($"Min : {min.ToString()}");

            PQ.Insert(min);
            PQ.Insert(max);

            Console.WriteLine($"Contains min: {PQ.Contains(min)}");
            Console.WriteLine($"Contains max: {PQ.Contains(max)}");

            PQ.PrintQuadTree();
            Console.ResetColor();
            Console.WriteLine("----------------------------------------------------------------------");

            Console.WriteLine();
            Console.WriteLine("Deleting min and max.... (Leaf Nodes)");
            // Delete min and max
            PQ.Delete(min);
            PQ.Delete(max);
            Console.WriteLine($"Contains min: {PQ.Contains(min)}");
            Console.WriteLine($"Contains max: {PQ.Contains(max)} \n");
            PQ.PrintQuadTree(); // Print after delete
            Console.ResetColor();
            Console.WriteLine("---------------------------------------------------------------------- \n");

            // Deleting 2 random nodes
            Console.WriteLine("Deleting 2 random nodes from the Point Set");
            for (int i = 0; i < 2; i++)
            {
                Point randomDelete = pointSet[rand.Next(0, pointSet.Count - 1)];
                Console.WriteLine($"Deleting : {randomDelete.ToString()}");
                PQ.Delete(randomDelete);
                pointSet.Remove(randomDelete); // Remove from pointset
            }
            Console.WriteLine();
            PQ.PrintQuadTree(); // Print after delete
            Console.ResetColor();
            Console.WriteLine("---------------------------------------------------------------------- \n");

            // Delete root
            Console.WriteLine("Deleting the root...");
            PQ.Delete(root); // Delete root
            Console.WriteLine();
            PQ.PrintQuadTree();
            Console.ReadKey();
        }
    }
}
