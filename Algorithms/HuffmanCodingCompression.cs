using System.Text;

namespace Algorithms
{
    // Huffman Encoding
    // A lot of times, a sequence of chars has a patter that occurs more than once
    // In a string, maybe the letter A appears more than the letter Z
    // We can use that to assign the binary "0" to A and "10" to Z, so that we make the binary much shorter
    // Instead of using the ASCII code, we will use this shorter binary variation of them, diminishing the binary by a lot
    // To do that, no code shoud be the prefix of the other, So we can't have '0' and '01' and '010' because those have the same prefix
    // But we can have '0', '10', '110', and '111', as none of those codes is the prefix of the other
    // We will then build a binary tree with that information and use the binary tree for enconding and deconding
    // When traversing the tree, going left adds a 0, and going right adds a 1 to the code
    //
    // O(n log(n))
    //

    // **IMPORTANT**The following is a not optimal (and possibly buggy) implementation, but it gets the job done

    // Huffman needs a tree that it used to encode and decode the strings
    // This tree has LEAF nodes that are used to store the chars, and branch nodes that are there just to add '0' (left) or '1' (right)
    //    to the encoding
    // DECODING then is traversing the tree:
    //   * found a 0, go left
    //   * found a 1, go right
    //   * is LEAF? print the value
    //   * start at the root again and keep going
    //   * BECAUSE no coding is a prefix of other code, this will always work, and there's always just ONE WAY to end in a given leaf.
    // ENCODING on other hand is:
    //   * create a tree so that no code is the prefix of other codes
    //   * crete a tree where every element is a leaf
    //   * starting at elements with lower priority (that is, the ones that appear LESS TIMES in the input string), start building the tree
    //   * take the two lower priority nodes (say A and B), CREATE a new branch node, put one A on the left, B on the right
    //        and insert the branch node on the tree with the priority being the SUM of A and B
    //   * keep doing that, and the tree is done. Now use this tree to create a encoded string
    //       * just take the tree and find the node of the given char, and then add the binary string to the encoded string
    //       * when is done, this binary string is the encoded string, so we don't need to use the ASCII code of them, for example
    internal static class HuffmanCodingCompression
    {
        class Node
        {
            public char? Char;
            public Node Left;
            public Node Right;
            public int Priority;
        }

        internal static string Encode(string text, Dictionary<char, string> codes)
        {
            // Find each letter in text on the codes table and add to the encoded string

            var builder = new StringBuilder();

            foreach (var c in text)
            {
                builder.Append(codes[c]);
            }

            return builder.ToString();
        }

        internal static string Decode(string encoded, Dictionary<char, string> codes)
        {
            // Get a partial string of binary codes and check if there's a match on the codes dictionary
            // If there's a match, reset the partial string and add the matched char to the decoded string
            var builder = new StringBuilder();

            var partial = "";

            foreach (var c in encoded)
            {
                partial += c;
                if (codes.ContainsValue(partial))
                {
                    builder.Append(codes.Where(x => x.Value == partial).Select(x => x.Key).First());
                    partial = "";
                }
            }

            return builder.ToString();
        }

        internal static Dictionary<char, string> BuildHuffmanTree(string text)
        {
            if (string.IsNullOrEmpty(text)) { return null; }

            // Count the frequency of each char
            var frequency = new Dictionary<char, int>();
            foreach (var c in text)
            {
                if (frequency.ContainsKey(c))
                {
                    frequency[c]++;
                }
                else
                {
                    frequency.Add(c, 1);
                }
            }

            // Make a queue so that the char that appears less has GREATER priority 
            var queue = new PriorityQueue<Node, int>(Comparer<int>.Create((a, b) => a.CompareTo(b)));
            foreach (var c in frequency)
            {
                queue.Enqueue(new Node { Char = c.Key, Priority = c.Value }, c.Value);
            }

            // While you don't have one item (one item means having just a root on a tree with right and left nodes filled)
            while (queue.Count != 1)
            {
                var left = queue.Dequeue();
                var right = queue.Dequeue();

                // Take left and right and build a branch with it, summing theirs priorities
                var sum = right.Priority + left.Priority;
                queue.Enqueue(new Node { Char = null, Left = left, Right = right, Priority = sum }, sum);
            }

            // Take the root
            var root = queue.Peek();
            var codes = new Dictionary<char, string>();

            // Build the codes Dictionary
            Build(root, "", codes);

            return codes;
        }

        private static void Build(Node node, string currentString, Dictionary<char, string> codes)
        {
            if (node == null) { return; }

            if (IsLeaf(node))
            {
                // Is a leaf, add to the codes string
                codes.Add(node.Char.Value, currentString.Length > 0 ? currentString : "1");
            }

            // Is not leaf, go left and right and add 0 and 1 to the binary code
            Build(node.Left, currentString + '0', codes);
            Build(node.Right, currentString + '1', codes);
        }

        private static bool IsLeaf(Node node)
        {
            return node.Left == null && node.Right == null;
        }
    }
}
