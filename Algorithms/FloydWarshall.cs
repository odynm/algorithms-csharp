namespace Algorithms
{
    // Shortest path between *all* pairs of vertices to *all* other vertices, negatives edges *allowed*
    // Negative cycles are not allowed, as in any shortest path algorithm, because cycling through them will always made the path shorter
    // Time complexity: O(V^3)
    // Negative cycle checking:
    // If distance of any vertex from itself becomes negative, it means we have a negative cycle
    // As of any graph algorithm, the input could be in any form, provided that we transform it to our needs
    // The return could be a matrix of paths, a matrix of distances, or a list of all vertices' path to all other nodes,
    // according to the needs
    // Works also for DIRECTED graphs
    internal static class FloydWarshall
    {
        /// <summary>
        /// This implementation receives an adjacency matrix of weights and returns a matrix for distances and one for the shortest path
        /// Unreachable nodes are market with weight = int.MaxValue
        /// To know the full path, check PrintPath function
        /// IMPORTANT: This implementation doesn't check for negative cycles, but it would be easy to add this if needed
        /// </summary>
        /// <param name="adjList">A graph represented in ajacency list form</param>
        /// <returns>Return a Tuple of <distances, paths></returns>
        public static Tuple<int[,], int[,]> Run(int[,] adjMatrix)
        {
            // Adj matrix is squared by nature
            if (adjMatrix.GetLength(0) != adjMatrix.GetLength(1)) return new Tuple<int[,], int[,]>(new int[0, 0], new int[0, 0]);

            var dimensionLength = adjMatrix.GetLength(0);

            var distances = new int[dimensionLength, dimensionLength];
            var paths = new int[dimensionLength, dimensionLength];

            for (int i = 0; i < dimensionLength; i++)
            {
                for (int j = 0; j < dimensionLength; j++)
                {
                    // The initial distance is the same as the weight of the edge for now
                    distances[i, j] = adjMatrix[i, j];

                    if (i == j)
                        paths[i, j] = 0; // To i == j, there's no origin
                    else if (distances[i, j] != int.MaxValue)
                        paths[i, j] = j; // Reachable places are marked with the index of the reachable node itself.
                    else
                        paths[i, j] = -1; // Unreachable places are -1
                }
            }

            // Update matrices whenever we discover that there's a path that has last cost than the previous one we knew
            // basically by bruteforcing all possible paths. It checks every possible path, starting from every possible vertex
            // We keep each path source->destination "distance memory" on the distances matrix, so we can compare with the new path
            // to know which option will be shorter and update both the distances[] and the paths[] (see PrintPath() to know how paths[] work
            for (int k = 0; k < dimensionLength; k++)
            {
                for (int i = 0; i < dimensionLength; i++)
                {
                    for (int j = 0; j < dimensionLength; j++)
                    {
                        // This if basically:
                        // Check if the distance from i->k->j is less than the distance from i->j, that is, we add a new route option
                        // If is less, we update the path. If not, we don't do anything. 
                        // If MaxValue, means there's no route through i->k or k->j, so we don't do anything as well
                        // On the other hand, if i->j is MaxValue, means we can't reach j directly from i and we can use k as a route to pass through
                        if (
                            distances[i, k] != int.MaxValue && distances[k, j] != int.MaxValue &&
                            distances[i, k] + distances[k, j] < distances[i, j])
                        {
                            distances[i, j] = distances[i, k] + distances[k, j]; // Update distance by using the i->k->j route
                            paths[i, j] = paths[i, k]; // Update paths by saying: using source i, to dest j, we can use node k as a route
                        }
                    }
                }
            }
            return new Tuple<int[,], int[,]>(distances, paths);
        }

        // FloydWarshall returns a path that is a adjacency matrix. This matrix represents source X destination
        // This means that for any paths[x,dest], you can get which is the next best node to take until you arrive at dest.
        // So if you want to know the path from S to D, you:
        // * take paths[S,D]: this will return the best next node N you should take
        // * if N == D, you arrived. If not, take paths[N,D] now: **the same destination, now a different source**
        // * Do this until you arrive.
        public static void PrintPath(int[,] paths)
        {
            for (int i = 0; i < paths.GetLength(0); i++)
            {
                for (int j = 0; j < paths.GetLength(1); j++)
                {
                    if (i != j)
                    {
                        Console.Write(string.Format("From {0} to {1} is: {2}", i, j, i));
                        PrintPath(paths, i, j);
                        Console.WriteLine(string.Format("-> {0}", j));
                    }
                }
            }

            Console.WriteLine();
        }

        static void PrintPath(int[,] paths, int source, int dest)
        {
            if (paths[source, dest] == dest)
            {
                return;
            }
            Console.Write(string.Format("-> {0}", paths[source, dest]));
            PrintPath(paths, paths[source, dest], dest);
        }
    }
}
