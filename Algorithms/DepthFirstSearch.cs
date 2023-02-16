namespace Algorithms
{
    internal static class DepthFirstSearch
    {
        // There's a lot of ways of doing this according to needs
        // If the path is not needed for example, we can just return the node if we find it
        public static List<int>? FindPath(List<int>[] adjList, int start, int end)
        {
            // Method 1
            return Search_Iterative(adjList, start, end);

            // Method 2
            var visited = new List<int>();
            //return Search_Recursive(adjList, visited, start, end);
        }

        public static List<int>? Search_Iterative(List<int>[] adjList, int start, int end)
        {
            var visited = new List<int>();
            var parents = new int[adjList.Length];
            var stack = new Stack<int>();
            stack.Push(start);

            while (stack.Count > 0)
            {
                var node = stack.Pop();
                if (!visited.Contains(node))
                {
                    visited.Add(node);

                    for (var i = 0; i < adjList[node].Count; i++)
                    {
                        var neighbor = adjList[node][i];

                        if (node == end)
                        {
                            return BackTrace(parents, start, end);
                        }

                        if (!visited.Contains(neighbor))
                        {
                            parents[neighbor] = node;

                            stack.Push(neighbor);
                        }
                    }
                }
            }

            return null;
        }

        public static List<int>? Search_Recursive(List<int>[] adjList, List<int> visited, int cur, int end)
        {
            visited.Add(cur);

            if (cur == end)
                return visited;

            foreach (var neighbor in adjList[cur])
            {
                if (!visited.Contains(neighbor))
                {
                    var newVisited = new List<int>();
                    newVisited.AddRange(visited);
                    var path = Search_Recursive(adjList, newVisited, neighbor, end);

                    if (path != null) return path;
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
