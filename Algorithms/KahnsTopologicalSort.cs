using System.Runtime.CompilerServices;

namespace Algorithms
{
    // In a DAG (directed acyclic graph), make so that every directed edge uv from u to v, u comes before v in the ordering
    // Using depth first search, this would be calculated using a sort of the departure time. By searching for depth first,
    // this ensures a topological sort. Arrival time: arrived at node. Departure time: when explored all neighbors.
    // So every new arrival we add the "time" variable, and in departure, we set it to the node. Now we will go back to the
    // previous node, until we are back a the start node of the DFS, which will have time 0. https://www.youtube.com/watch?v=n_yl2a6n7nM

    // On the other hand:
    // Kahns is more efficient.
    // * First we calculate the inDegree of those nodes. The degree is a count of how many nodes point to that vertice
    // * Then we add all inDegrees zero to the queue
    // * Pop the queue, add to the topological sort, and update inDegrees of adjacent vertices. If they become zero, add to the queue
    // * Remove the computed vertice from the graph
    // * Do this until all vertices are computed
    // O(V+E)

    // THIS IS A CLUNKY IMPLEMENTATION, I tried some different ways first. Could be better.
    internal static class KahnsTopologicalSort
    {
        // As always, can receive any type of structure, we will use a edge array
        public static List<int> Sort(Tuple<int, int>[] edges, int n)
        {
            var sorted = new List<int>(); // Sorted list
            var queue = new Queue<int>(); // Next nodes to be computed
            var inDegree = new int[n]; // Stores how many edges are pointing to a given node
                                       // Each time one node is computed, it is removed from the graph,
                                       // and the inDegree of its neighbors decreased
            var adjacencies = new List<int>[n]; // Adjacency list to be used instead of edge list

            // Create an adjacency list and populate the initial degrees
            foreach (var edge in edges)
            {
                if (adjacencies[edge.Item1] == null)
                {
                    adjacencies[edge.Item1] = new List<int>();
                }
                adjacencies[edge.Item1].Add(edge.Item2);
                inDegree[edge.Item2]++;
            }

            // Enqueue inDegree 0
            for (var i = 0; i < n; i++)
            {
                if (inDegree[i] == 0)
                {
                    queue.Enqueue(i);
                }
            }

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                sorted.Add(node);

                // Iterate on neighbors, updating the inDegree and enqueuing 
                if (adjacencies[node] != null)
                {
                    foreach (var neighbor in adjacencies[node])
                    {
                        inDegree[neighbor]--;
                        if (inDegree[neighbor] == 0)
                        {
                            queue.Enqueue(neighbor);
                        }
                    }
                }

                // When done with neighbors, remove from adjacency list
                adjacencies[node] = null;
            }

            // If not all items are removed, we encountered a cycle
            if (adjacencies.Any(x => x != null))
            {
                throw new Exception("The graph has at least on cycle");
            }
            else
            {
                return sorted;
            }
        }
    }
}
