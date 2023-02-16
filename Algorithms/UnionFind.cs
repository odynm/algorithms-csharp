using System.Reflection.Metadata.Ecma335;

namespace Algorithms
{
    // Union-find data strucure, or disjoint-set, is a group of sets with elements that can't be on more than one set at the same time
    // Find: determines in which set a particular element is and return a "representative" of the set
    // Union: merges two different subsets in a single subset, and the "representative" of the sets is now the same
    // Make set: creates a new set with only a given element in it
    // We can determine if two items are in the same set by checking if they have the same "representative"
    // Simply: find follows parents until the root node ("representative") and unions "merges" the root nodes
    // We can simpy use a group of linked lists (sets) to implement the algorithm
    // Optimizations:
    // * using a rank, we can always attach the smaller tree to the bigger one. This will decrease the depth of the tree
    // a tree with one element has rank 0. So the running time improves to O(log(n))
    // * using path compression, is a way to flatten the tree structure whenever a "find" is called. This means attaching every
    // visited node while doing the "find" operation and attaching it directly to the root node. This really speeds up future finds
    // It can be used for example to find the MST of a graph in Kruskal or detect a cycle in a undirected graph

    // To find cycles: makeSet with all vertices, then keep calling Union on edges. If you call Union in two elements that are
    // already in the same set, a cycle is found
    // MST Kruskal: sort all edges by weight, and add the edges as long as they don't have same root (that will mean a cycle). Do this
    // until you have processes all edges or unified all vertices
    internal static class UnionFind
    {
        static Dictionary<int, int> parents = new Dictionary<int, int>();
        static Dictionary<int, int> ranks = new Dictionary<int, int>();

        // Each element will be it's own set
        internal static void MakeSet(int[] universe)
        {
            foreach (var set in universe)
            {
                parents.Add(set, set); // Each set has only itself as element
                ranks.Add(set, 0); // all with rank 0
            }
        }

        internal static int Find(int element)
        {
            // If element is not the root
            if (parents[element] != element)
            {
                // Compress path, making the parent of the element be the next parent
                parents[element] = Find(parents[element]);
            }

            return parents[element]; // Return the parent
            // Since this is recursive, it will only stop when parents[element] == root, and then it will return the root
        }

        internal static void Union(int a, int b)
        {
            var aRoot = Find(a);
            var bRoot = Find(b);

            // Already same set
            if (aRoot == bRoot) return;

            // Compare rank and add the smallest to the biggest
            if (ranks[aRoot] < ranks[bRoot])
            {
                parents[aRoot] = bRoot;
            }
            else if (ranks[bRoot] < ranks[aRoot])
            {
                parents[bRoot] = aRoot;
            }
            else
            {
                parents[aRoot] = bRoot;
                ranks[bRoot] = bRoot + 1;
            }
        }

        internal static void Print(int[] universe)
        {
            foreach (var set in universe)
            {
                Console.Write(Find(set) + " ");
            }

            Console.WriteLine();
        }
    }
}