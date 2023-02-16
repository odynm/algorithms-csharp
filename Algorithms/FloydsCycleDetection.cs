namespace Algorithms
{
    // Detect a cycle in a linked list
    // We could just create a hashset and add the linked list items to the hashset if they don't exist. If they exist, there's a cycle
    // OR, we can use the Floyds cycle detection algorithm presented here
    // The idea is to move through the list with two pointers at different speeds. The first moves 1 each increment, the second moves 2
    // Because of this is called the tortoise-hare algorithm
    // If the pointers meet, we have a cycle.
    // Reason: the slow pointer would never catch the fast, but if there's a cycle, the fast pointer will start cycling it undefinately,
    // and at some point the slow pointer will enter the loop and they will be equal somewhere.
    // O(n)
    internal static class FloydsCycleDetection
    {
        public class Node
        {
            public int Data { get; set; }
            public Node Next { get; set; }

            public Node(Node next, int data)
            {
                Data = data;
                Next = next;
            }
        }

        public static bool Detect(Node linkedListHead)
        {
            var slow = linkedListHead;
            var fast = linkedListHead;

            while (slow != null && fast != null)
            {
                slow = slow.Next;
                fast = fast.Next?.Next;

                if (slow == fast)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
