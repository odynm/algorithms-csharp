namespace Algorithms
{
    // Take a list of ((edge),weight)
    // Make all starting parents of the nodes be the node itself
    // Sort the list of edges based by weight
    // Add them to MST as long as the root parent of the nodes are not equal, that is:
    //     if the root parent is not equal, means they are not in the same cycle yet and will not form a cycle
    // If not, add to MST and merge its root parents. So now the two items (or two trees) are have the root
    // This is a clever way to avoid cycles
    
    // O(E log E) 
    internal static class Kruskal
    {
        public static List<Tuple<Tuple<int, int>, int>> Run(List<Tuple<Tuple<int, int>, int>> list, int numVertices)
        {
            var mst = new List<Tuple<Tuple<int, int>, int>>();

            var parents = new int[numVertices];

            for (int i = 0; i < parents.Length; i++)
            {
                parents[i] = i;
            }

            list.Sort((a, b) => a.Item2.CompareTo(b.Item2));

            // IMPROVEMENT: we could do while(mst.Count < (numVertices-1)), because the MST is always Vertices-1
            foreach (var item in list)
            {
                var item1Root = FindRoot(parents, item.Item1.Item1);
                var item2Root = FindRoot(parents, item.Item1.Item2);
                if (item1Root != item2Root)
                {
                    mst.Add(item);
                    Union(parents, item1Root, item2Root);
                }
            }

            return mst;
        }

        static int FindRoot(int[] parents, int index)
        {
            if (parents[index] == index) return index;
            return FindRoot(parents, parents[index]);
        }

        static void Union(int[] parents, int aRoot, int bRoot)
        {
            parents[aRoot] = parents[bRoot];
        }
    }
}
