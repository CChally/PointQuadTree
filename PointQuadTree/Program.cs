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
        private Point p; // Point 
        private Node[] quadrants; // Node References
        public Node(int dim)
        {
            p = new Point(dim); // Create zero node
            quadrants = new Node[dim*dim]; // For the n-th dimension, n^2 node child references are needed

        }
    }

    // QuadTree Class
    // Represents a Point Quadtree in n-th dimensional space
    public class PointQuadTree
    {
        private Node root { get; set; } // Root Reference
        private int dimension { get; set; } // Number of Dimensions

        // Constructor
        // Creates an initially empty quadtree of the specified dimension
        public PointQuadTree(int dim)
        {
            root = null; // Initially empty quadtree
            this.dimension = dim; // Init with number of dimensions
        }
        public void Insert()
        {

        }
    }
}
