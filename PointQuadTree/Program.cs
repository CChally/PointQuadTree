
namespace PointQuadTree
{
    // Point Class
    // Represents a point in the n-th dimension
    public class Point
    {
        public float[] coord { get; set; } // Coordinate storage 

        // Constructor
        // Creates a zero point in the specified dimension
        public Point(int dim)
        {
            coord = new float[dim]; // Create array to store 
            /* For 2D: (Can be generalized for any dimension)

              coord[0] -> SW
              coord[1] -> SE
              coord[2] -> NW
              coord[3] -> NE

             */
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
        // Absolute value
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
            string pointString = "( ";

            for (int i = 0; i < getDim(); i++) // For each dimension, concatenate the dimension value to the return string
            {
                pointString += coord[i] + " ";
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
            quadrants = new Node[(int)Math.Pow(2, dim)]; // For the n-th dimension, the number children pointers = 2^dim
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
            // Invalid Point Dimensions Check
            if (p.getDim() != dimension)
                throw new ArgumentException($"Invalid dimensions! Point {p.ToString()} is not {dimension}-D");

            // Valid dimension
            return Insert(ref root, p);
        }
        // Insert Private Recursive
        private bool Insert(ref Node node, Point point)
        {
            if (node == null) // Hits leaf node
            {
                node = new Node(dimension, point); // Create node with point
                return true;
            }
            else // Traverse
            {
                if (!node.p.Equals(point)) // Not a duplicate
                {
                    int quadrantIndex = 0;
                    for (int i = 0; i < dimension; i++) // Compare each dimension
                    {
                        if (point.Get(i) > node.p.Get(i)) // Point coord is greater
                            quadrantIndex += (int)Math.Pow(2, i); // Get quadrant index 

                        // Continue with quadrantIndex, no modification
                    }
                    return Insert(ref node.quadrants[quadrantIndex], point); // Select quadrant
                }
                else { return false; } // Duplicate point
            }
        }

        // Public Delete
        public bool Delete(Point p)
        {
            if (p.getDim() != dimension)
                throw new ArgumentException($"Invalid dimensions! Point {p.ToString()} is not {dimension}-D");

            return Delete(ref root, p);
        }

        // Private Recursive Delete
        private bool Delete(ref Node node, Point point)
        {
            if (node == null) // Check if node exists
                return false;

            else if (node.p.Equals(point)) // Node found
            {
                bool hasChildren = false;

                // Determine if the found node is an internal node, or a leaf node
                for (int i = 0; i < Math.Pow(2, dimension); i++)
                {
                    // Determine if leaf node
                    if (node.quadrants[i] != null)
                    {
                        hasChildren = true; // Child exists
                        break;             // Stop here
                    }
                }

                if (hasChildren) // Internal node
                {
                    node.p = FindLeaf(ref node); // Replace point value with leaf node, and snip off the replacing node
                    
                    // Check if any of the children of the replacement are in the incorrect quadrants, if they are, the entire subtree
                    // needs to be reinserted

                    List<Point> points = new List<Point>(); // List of points to be added in the case of violating subtrees

                    for (int i = 0; i < Math.Pow(2, dimension); i++) // For each child index of deleting node,
                    {
                        // Compare root and child to generate a quadrantIndex, and compare to i
                        int quadrantIndex = 0;
                        for (int j = 0; j < dimension; j++) // Compare each dimension
                        {
                            if (point.Get(j) > node.p.Get(j)) // Point coord is greater
                                quadrantIndex += (int)Math.Pow(2, i); // Get quadrant index 

                            // Continue with quadrantIndex, no modification
                        }

                        if (i != quadrantIndex) // Point is in wrong quadrant
                        {
                            InsertSubtree(ref node.quadrants[i], points); // Recurse down subtree with pointList
                        }

                    }
                    foreach (Point p in points) // Insert violating points
                    {
                        Insert(p); // Re-insert
                    }
                    return true; // Successful deletion
                }
                else // Leaf Node (No Children)
                {
                    node = null; // Snip Node
                    return true;
                }
            }
            else // Traverse
            {
                int quadrantIndex = 0;
                for (int i = 0; i < dimension; i++) // Compare Each Dimension
                {
                    if (point.Get(i) > node.p.Get(i)) // Point Coord is Greater
                        quadrantIndex += (int)Math.Pow(2, i); // Get Quadrant Index 

                    // Continue with quadrantIndex, no modification
                }
                return Delete(ref node.quadrants[quadrantIndex], point); // Select Quadrant
            }
        }

        // Recursively inserts a subtree back into the QuadTree, starting from a leaf and working back up to the root. The node point
        // is first added to a list of running re-inserts to be used after return. The point is added, and then snipped off.
        private void InsertSubtree(ref Node node, List<Point> pointList)
        {
            if (node == null)
            {
                return;
            }
            else
            {
                // Recursively insert all children first
                for (int i = 0; i < Math.Pow(2, dimension); i++)
                {
                    InsertSubtree(ref node.quadrants[i], pointList); // Recurse
                }

                // Insert current node and set to null
                Point p = node.p;
                pointList.Add(p); // Add to pointList
                node = null; // Snip off node
            }
        }

        // Finds leaf node to replace deleting node with
        private Point FindLeaf(ref Node node)
        {
            if (node == null) throw new ArgumentException("Deleting node is already null!");
            else // Recurse until leaf node is found
            {
                bool hasChildren = false;
                for (int i = 0; i < Math.Pow(2, dimension); i++) // Look at each child
                {
                    if (node.quadrants[i] != null) // If a child exists,
                    {
                        Point point = FindLeaf(ref node.quadrants[i]); // recurse to existing child
                        hasChildren = true;
                        return point;
                    }
                }
                if (!hasChildren) // leaf node
                {
                    Point replacement = node.p;
                    node = null; // Set leaf to null
                    return replacement; // return leaf node value
                }
                else
                    return null; // Something went terribly wrong?
            }
        }

        // Public Contains
        public bool Contains(Point p)
        {
            // Invalid Point Dimensions Check
            if (p.getDim() != dimension)
                throw new ArgumentException($"Invalid dimensions! Point {p.ToString()} is not {dimension}-D");

            // Valid Dimension
            return Contains(ref root, p);
        }

        // Private Recursive Contains
        private bool Contains(ref Node node, Point point)
        {
            if (node == null) return false; // Ran off tree
            else
            {
                if (node.p.Equals(point)) return true; // Point found in current node
                else // Traverse
                {
                    int quadrantIndex = 0;
                    for (int i = 0; i < dimension; i++) // Compare each dimension
                    {
                        if (point.Get(i) > node.p.Get(i)) // Point coord is greater
                            quadrantIndex += (int)Math.Pow(2, i); // Get quadrant index 
                    }
                    return Contains(ref node.quadrants[quadrantIndex], point);
                }
            }
        }

        // Public Print
        public void PrintQuadTree()
        {
            if (root == null) // Empty tree
                Console.WriteLine("Empty tree.");
            else
                PrintQuadTree(root, 0, 0); // Print tree recursively from root
        }

        // Private Recursive Print
        // Prints the Quadtree in Reverse Inorder with indenting for spaces. The output is a tree tilted to the left 90 degrees.
        private void PrintQuadTree(Node node, int indent, int wheredid)
        {
            if (node != null) // Base
            {
                // Traverse Right (Half) Subtree
                for (int i = node.quadrants.Length - 1; i >= (node.quadrants.Length / 2); i--)
                {
                    PrintQuadTree(node.quadrants[i], indent + 15, i);
                }

                if (wheredid >= (Math.Pow(2, dimension) / 2)) // Right subtree color
                    Console.ForegroundColor = ConsoleColor.Green;

                else // Left subtree color
                    Console.ForegroundColor = ConsoleColor.Red;

                // Print Inorder
                Console.WriteLine("".PadLeft(indent) + node.p.ToString());

                // Traverse Left (Half) Subtree
                for (int j = (node.quadrants.Length / 2) - 1; j >= 0; j--)
                {
                    PrintQuadTree(node.quadrants[j], indent + 15, j);
                }
            }
        }
    }
}
