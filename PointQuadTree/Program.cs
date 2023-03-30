
namespace PointQuadTree
{
    // Point Class
    // Represents a point in the n-th dimension
    public class Point
    {
        private float[] coord { get; set; } // Coordinate storage 

        // Constructor
        // Creates a zero point in the specified dimension
        public Point(int dim)
        {
            coord = new float[dim]; // Create array to store 
        }

        // getDim
        // Returns the dimension the point is in
        public int getDim()
        {
            return coord.Length;
        }

        // Get
        // Returns the i-th dimension value in a coordinate
        public float Get(int i)
        {
            return coord[i];
        }

        // Set
        // Sets the i-th dimension value in a cordinate
        public void Set(int i, float value)
        {
            coord[i] = value;
        }

        // distanceTo
        // Returns the Euclidean distance between two points in the nth-dimensions
        public float distanceTo(Point other)
        {
            double distance = 0;

            for (int i = 0; i < getDim(); i++) // For each dimension
            {
                distance += Math.Pow(other.coord[i] - this.coord[i], 2);
            }
            return (float)Math.Sqrt(distance);
        }

        // Equals Override
        // Returns if a point in k-th dimensional space 
        public override bool Equals(Object? obj)
        {
            if (obj != null) // Null check
            {
                Point p = (Point)obj; // Downcast 
                for (int i = 0; i < getDim(); i++) // For each dimension
                {
                    if (this.coord[i] != p.coord[i]) return false; // Compare values
                }
                return true;
            }
            return false; // Null
        }

        // ToString Override
        // Converts the point to a string
        public override string ToString()
        {
            string pointString = "( "; // Empty string

            for (int i = 0; i < getDim(); i++) // For each dimension, concatenate the dimension value to the return string
            {
                pointString += coord[i]+ " ";
            }
            pointString += ")";
            return pointString;
        }

    }

    // Node Class
    // Represents a node in a QuadTree
    public class Node
    {
        public Point p { get; set; } // Point 
        public Node[] quadrants; // Node References

        // Constructor
        // Creates a Node given a dimension and point
        public Node(int dim, Point p)
        {
            this.p = p; // Assign Point to Node
            quadrants = new Node[(int)Math.Pow(2,dim)]; // For the n-th dimension, the number children pointers = 2^dim
        }
    }

    // QuadTree Class
    // Represents a Point Quadtree in n-th dimensional space
    public class PointQuadTree
    {
        private Node root; // Root Reference
        public int dimension; // Number of Dimensions, public so Main can have access when constructing points.

        // Constructor
        // Creates an initially empty quadtree of the specified dimension
        public PointQuadTree(int dim)
        {
            root = null; // Initially empty quadtree
            this.dimension = dim; // Init with number of dimensions
        }

        // Insert
        // 
        public bool Insert(Point p)
        {
            if (p.getDim() != dimension) { throw new ArgumentException($"Invalid dimensions! Point {p.ToString()} is not {dimension}-D"); }  // Invalid Point Dimensions
            return Insert(ref root, p);
        }
        private bool Insert(ref Node node, Point point)
        {
            if(node == null) // hits leaf node
            {
                node = new Node(dimension, point); // create node with point
                return true;
            }
            else // traverse
            {
                if (!node.p.Equals(point)) // Not a duplicate
                {
                    int quadrantIndex = 0;

                    for (int i = 0; i < dimension; i++) // Compare each dimension
                    {
                        if (point.Get(i) > node.p.Get(i)) // Point coord is greater
                        {
                            quadrantIndex += (int)Math.Pow(2, i); // Get quadrant index 
                        }
                        // Continue with quadrantIndex, no modification
                    }
                    return Insert(ref node.quadrants[quadrantIndex], point); // Select quadrant
                }
                else { return false; } // duplicate point
            }
        }
        
        // Public Print
        public void PrintQuadTree()
        {
            PrintQuadTree(root, 0);
        }

        // Private Recursive Print
        private void PrintQuadTree(Node root, int n)
        {
            if (root != null)
            {
               
            }
        }
    }
}
