namespace Algorithms
{
    // In most cases, using an adjacent list is optimal as opposed from a edge list, for example
    // The edge list might be most efficient space-wise sometimes
    // Runtime, given E(edges) and V(vertex):
    // Edge list: O(E^2) - for each vertex v, traverse all edge list to find all cases where v is the source
    // Adjacency list: O(V+E) - for each vertex, there's a list of adjacent ready
    // Matrix O(V^2) - for each vertex, traverse a list of vertices to check if they are adjacent
    internal static class BreadthFirstSearch
    {
        // There's a lot of ways of doing this according to needs
        // If the path is not needed for example, we can just return the node if we find it
        public static List<int>? FindPath(List<int>[] adjList, int start, int end)
        {
            // Method 1
            return Search_EnqueuePath(adjList, start, end);

            // Method 2
            //return Search_EnqueueNode(adjList, start, end);
        }

        // This implementation enqueues the next full path instead of just the next node
        public static List<int>? Search_EnqueuePath(List<int>[] list, int start, int end)
        {
            var queue = new Queue<List<int>>();
            var visited = new List<int>();

            queue.Enqueue(new List<int> { start });

            while (queue.Count > 0)
            {
                var path = queue.Dequeue();
                var node = path.Last();
                visited.Add(node);

                if (node == end)
                    return path;

                foreach (var neighbor in list[node])
                {
                    var newPath = new List<int>(path);
                    if (!visited.Contains(neighbor))
                    {
                        newPath.Add(neighbor);
                        queue.Enqueue(newPath);
                    }
                }
            }

            return null;
        }

        public static List<int>? Search_EnqueueNode(List<int>[] list, int start, int end)
        {
            var parent = new int[list.Length];
            var queue = new Queue<int>();
            var visited = new List<int>();

            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                visited.Add(node);

                if (node == end)
                    return BackTrace(parent, start, end);

                for (var i = 0; i < list[node].Count; i++)
                {
                    if (!visited.Contains(list[node][i]))
                    {
                        queue.Enqueue(list[node][i]);
                        parent[list[node][i]] = node;
                    }
                }
            }

            return null;
        }

        static List<int> BackTrace(int[] parents, int start, int end)
        {
            var path = new List<int>();
            int cur = end;

            while (path.LastOrDefault(-1) != start)
            {
                path.Add(cur);
                cur = parents[cur];
            }

            path.Reverse();

            return path;
        }
    }
}
