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
            Point d, p;

            d = new Point(2);
            d.Set(0, 12);
            d.Set(1, 5);      // 0,5

            p = new Point(2);
            p.Set(0, 0);
            p.Set(1, 20);

            // Test prints
            Console.WriteLine(d.ToString()); // Print d
            Console.WriteLine(p.ToString()); // Print p

            Point e = new Point(2);
            e.Set(0, 0);
            e.Set(1, 5);

            // Test Equals
            Console.WriteLine(e.Equals(d));

            // Test Euclid Distance
            Console.WriteLine(p.distanceTo(d));
        }
    }
}
