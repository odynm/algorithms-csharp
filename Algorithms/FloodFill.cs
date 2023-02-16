namespace Algorithms
{
    // Given a matrix MxN, this algorithm change de color to replacement color
    // Is used for example by paint bucket tool
    // The solution generaly involves a queue-based implementation of Bredth First Search (BFS)
    // We could do with Deep First (DFS) as well, and we don't need a visited array as we gonna change de item color and it will
    // not be valid as a neighbor after that.
    // O (MxN) as time an space complexity for both approaches.
    internal static class FloodFill
    {
        private static int[] rowMovements = new int[] { -1, 0, 1, -1, 1, -1, 0, 1 };
        private static int[] colMovements = new int[] { -1, -1, -1, 0, 0, 1, 1, 1 };

        // The BFS approach using an adjMatrix
        // Easy: start at the given node, take its color, and then using a simple BFS algorithm, as long as it is a valid node
        // (inside bounding box and of the same color as the origin one), add to the queue and color it the new color
        public static void Fill(int[,] adjMatrix, Tuple<int, int> startNode, int replacementColor)
        {
            var startingColor = adjMatrix[startNode.Item1, startNode.Item2];

            var queue = new Queue<Tuple<int, int>>();

            queue.Enqueue(new Tuple<int, int>(startNode.Item1, startNode.Item2));

            while (queue.Any())
            {
                var node = queue.Dequeue();

                adjMatrix[node.Item1, node.Item2] = replacementColor;

                for (int i = 0; i < 8; i++)
                {
                    var x = node.Item1 + rowMovements[i];
                    var y = node.Item2 + colMovements[i];
                    if (IsValid(adjMatrix, x, y, startingColor))
                    {
                        queue.Enqueue(new Tuple<int, int>(x, y));
                    }
                }
            }
        }

        private static bool IsValid(int[,] adjMatrix, int x, int y, int targetColor)
        {
            return x >= 0 && y >= 0 && x < adjMatrix.GetLength(0) && y < adjMatrix.GetLength(1) && adjMatrix[x, y] == targetColor;
        }
    }
}
