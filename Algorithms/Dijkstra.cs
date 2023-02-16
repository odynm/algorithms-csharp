namespace Algorithms
{
    // Ditance from source to all other nodes
    // Can be weightned or not, can be directed or not, and we can use adjacency list or adjacency matrix to compute it
    // This is a "simpler" version of FloydWarshal, but since I did that one first, go check that out
    // It basicaly return an array with distance from source to a given node and also the previous node path to that node
    // So to generate path we recursevely get the prev path node until we are at source (-1). 
    // O(E + V log V) (depends on the heap/priority queue used
    internal static class Dijkstra
    {
        // On this implementation we use adjacency matrix
        public static Tuple<int[], int[]> ShortestPath(int[,] adjMatrix, int source)
        {
            var n = adjMatrix.GetLength(0);

            var distances = new int[n]; // Distances of all nodes from source
            var prevs = new int[n]; // Best previous path node from source to node

            var minQueue = new SortedSet<Tuple<int, int>>(Comparer<Tuple<int, int>>.Create((a, b) =>
            {
                var result = a.Item2.CompareTo(b.Item2);
                if (result == 0) return a.Item1.CompareTo(b.Item1);
                else return result;
            }));

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
                minQueue.Add(new Tuple<int, int>(i, distances[i]));
            }

            // The queue holds items by priority. The starting node is the source (priority 0), and all others are priority int.MaxValue
            // Starting from source, we will updated the priority of each node as we go along the neighbors of each node
            while (minQueue.Any())
            {
                var node = minQueue.Min;
                minQueue.Remove(node); // Pop the item out, so we will consider this node "closed" after we finish computing its neighbors

                // For all items that have edges with the dequeued node
                for (int neighbor = 0; neighbor < n; neighbor++)
                {
                    var edgeToNeighborWeight = adjMatrix[node.Item1, neighbor];
                    if (edgeToNeighborWeight == int.MaxValue) continue; // MaxValue distance on adjacency matrix means no connection]
                    if (!minQueue.Any(x => x.Item1 == neighbor)) continue; // If neighbor is out of the queue already, means that
                                                  // it is closed, and so all neighbors and the shortest path are computed already

                    // Now we check if source->node->neighbor < source->neighbor
                    // If it is, we update de shortest path
                    var alternativePathDist = distances[node.Item1] + edgeToNeighborWeight;
                    if (alternativePathDist < distances[neighbor])
                    {
                        distances[neighbor] = alternativePathDist;
                        prevs[neighbor] = node.Item1; // To go to neighbor, now we pass through node

                        // Update priority of neighbor, now that we know a shortest path
                        minQueue.RemoveWhere(x => x.Item1 == neighbor);
                        minQueue.Add(new Tuple<int, int>(neighbor, alternativePathDist));
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