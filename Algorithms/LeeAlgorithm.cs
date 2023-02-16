namespace Algorithms
{
    // Finds the shortest path in a binary maze
    // 1 1 1 0 1 0
    // 0 0 1 1 0 0
    // 0 0 0 1 1 1
    // Can be used for example to find paths in wiring or routing
    // We compute it as a form of Breadth First Search
    // Take the source and add to queue. Keep adding adjacent nodes as long as they are not visited
    // Keep iterating and adding the next node to the queue, to the visited array, and keeping track of its parent for future backtracking
    // To get the path, we use the same strategy as other pathfindings: recursively get the parent until you hit the source node, then reverse
    // O(MxN) (dimensions of the matrix)
    internal static class LeeAlgorithm
    {
        static int[] rowMovements = new int[] { 0, -1, 1, 0 };
        static int[] colMovements = new int[] { -1, 0, 0, 1 };

        internal static Tuple<List<Tuple<int, int>>, int> ShortestPath(int[,] maze, int sx, int sy, int dx, int dy)
        {
            var visited = new HashSet<Tuple<int, int>>(); // (x, y) // Visited nodes 
            var queue = new Queue<Tuple<Tuple<int, int>, int>>(); // ((x, y), distance) // Queued nodes + distance
            var parents = new Dictionary<Tuple<int, int>, Tuple<int, int>>(); // ((x, y), (parentx, parenty))) // Nodes + its parents
             
            queue.Enqueue(new Tuple<Tuple<int, int>, int>(new Tuple<int, int>(sx, sy), 0));

            while (queue.Any())
            {
                var node = queue.Dequeue();

                if (node.Item1.Item1 == dx && node.Item1.Item2 == dy)
                {
                    return new Tuple<List<Tuple<int, int>>, int>(
                        Backtrack(parents, new Tuple<int, int>(sx, sy), new Tuple<int, int>(dx, dy)),
                        node.Item2);
                }

                // For each adjacent cells that are valid and not viisted, mark as visited and add to queue and parents
                for (int i = 0; i < 4; i++)
                {
                    var row = rowMovements[i];
                    var col = colMovements[i];
                    if (IsValid(maze, visited, node.Item1.Item1 + row, node.Item1.Item2 + col))
                    {
                        var neighbor = new Tuple<int, int>(node.Item1.Item1 + row, node.Item1.Item2 + col);
                        var neighborQueue = new Tuple<Tuple<int, int>, int>(neighbor, node.Item2 + 1);
                        visited.Add(neighbor);
                        queue.Enqueue(neighborQueue);
                        parents.Add(neighbor, node.Item1);
                    }
                }
            }

            return new Tuple<List<Tuple<int, int>>, int>(new List<Tuple<int, int>>(), 0);
        }

        static bool IsValid(int[,] maze, HashSet<Tuple<int, int>> visited, int x, int y)
        {
            if (visited.Contains(new Tuple<int, int>(x, y))) return false;
            if (x < 0 || y < 0 || x >= maze.GetLength(0) || y >= maze.GetLength(1)) return false;
            if (maze[x, y] == 0) return false;

            return true;
        }

        static List<Tuple<int, int>> Backtrack(
            Dictionary<Tuple<int, int>, Tuple<int, int>> parents,
            Tuple<int, int> source,
            Tuple<int, int> dest
        )
        {
            var path = new List<Tuple<int, int>>();
            Tuple<int, int> current = dest;

            path.Add(dest);

            while (parents.Any())
            {
                var prev = current;
                current = parents[current];
                parents.Remove(prev);
                path.Add(current);

                if (current.Item1 == source.Item1 && current.Item1 == current.Item2)
                {
                    path.Reverse();
                    return path;
                }
            }

            throw new Exception("Error generating path");
        }
    }
}
