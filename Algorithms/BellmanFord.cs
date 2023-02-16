namespace Algorithms
{
    // Single source multiple paths, can be weighted and directed
    // Differs from Dijskstra in the fact that it can work with NEGATIVE weights. Dijkstra can't work with negative because
    // once a vertex is visited, it is market as closed and the shortest path to it is assumed to be discovered.
    // It also can detect negative cycles. Of course, with negative cycles, there's no shortest path.
    // But it comes at a cost, and BellmanFord is slower than Dijkstra
    // O(VxE)
    internal static class BellmanFord
    {
        // As always, the form of input doesn't matter, you will only need to adapt the implementation
        // This will use adjacency list just because
        public static Tuple<int[], int[]> ShortestPath(int[,] adjMatrix, int source)
        {
            var n = adjMatrix.GetLength(0);

            var distances = new int[n]; // Distances of all nodes from source
            var prevs = new int[n]; // Best previous path node from source to node

            distances[source] = 0;
            prevs[source] = -1;

            // Initialize distances to max and prevs to -1 for all items that are not source
            for (int i = 0; i < n; i++)
            {
                if (i != source)
                {
                    distances[i] = int.MaxValue;
                    prevs[i] = -1; // Could be null or any other value representing "not set yet"
                }
            }

            // As with Dijkstra, keep looking for improved paths
            // Here we don't take the item with minimum weight because we will check all of them in order and never close them
            for (int node = 0; node < n; node++)
            {
                for (int neighbor = 0; neighbor < n; neighbor++)
                {
                    var edgeToNeighborWeight = adjMatrix[node, neighbor];
                    if (edgeToNeighborWeight == int.MaxValue) continue; // MaxValue means no connection between those nodes
                    if (distances[node] + edgeToNeighborWeight < distances[neighbor])
                    {
                        distances[neighbor] = distances[node] + edgeToNeighborWeight;
                        prevs[neighbor] = node;
                    }
                }
            }

            // If we are concerned about negative cycles, we can use a second pass to check for them
            // If we can still improve the path after the first pass, means we are facing a negative cycle
            for (int node = 0; node < n; node++)
            {
                for (int neighbor = 0; neighbor < n; neighbor++)
                {
                    var edgeToNeighborWeight = adjMatrix[node, neighbor];
                    if (edgeToNeighborWeight == int.MaxValue) continue; // MaxValue means no connection between those nodes
                    if (distances[node] + edgeToNeighborWeight < distances[neighbor])
                    {
                        throw new Exception("Negative cycle found");
                    }
                }
            }

            return new Tuple<int[], int[]>(distances, prevs);
        }

        public static void PrintFullPath(int[] prevs, int destination)
        {
            Console.Write("Path to " + destination + " in reverse order:");
            PrintPath(prevs, destination);
            Console.WriteLine();
        }

        static void PrintPath(int[] prevs, int current)
        {
            Console.Write(current + ", ");

            if (prevs[current] != -1)
            {
                PrintPath(prevs, prevs[current]);
            }
        }
    }
}
