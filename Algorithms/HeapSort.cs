namespace Algorithms
{
    // Is similar to a selection sort in that it divides the array in a sorted and in a unsorted region, but is uses a heap to 
    //    select the next item instead of a linear search
    // Not stable
    // It can be in place or out of place
    // Of course its much faster if we already have a heap
    // If we have a heap, is basically just pop until we are done
    // It becomes complex because the heapify is an algorithm itself
    // https://www.youtube.com/watch?v=2DmK_H7IdTo

    // O(n log(n))
    internal class HeapSort
    {
        // Get left child of arr[i]
        private static int GetLeft(int i)
        {
            return (2 * i + 1);
        }

        // Get right child of arr[i]
        private static int GetRight(int i)
        {
            return (2 * i + 2);
        }

        // Utility function to swap two indices in the array
        private static void Swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        // Recursive heapify-down algorithm, swapping nodes whenever we found one bigger than the current root node
        private static void Heapify(int[] arr, int i, int size)
        {
            // Get left and right child
            int left = GetLeft(i);
            int right = GetRight(i);

            int largest = i;

            // compare arr[i] with its left and right child and find the largest value
            if (left < size && arr[left] > arr[i])
            {
                largest = left;
            }

            if (right < size && arr[right] > arr[largest])
            {
                largest = right;
            }

            // Swap with a child having greater value and keep the process on the child that is now the root
            if (largest != i)
            {
                Swap(arr, i, largest);
                Heapify(arr, largest, size);
            }
        }

        // Pop an element with the highest priority, and the heapify the array again (subsequents heapifies are always faster)
        internal static int Pop(int[] arr, int size)
        {
            // if the heap has no elements
            if (size <= 0)
            {
                return -1;
            }

            int top = arr[0];

            // replace the root of the heap with the last element
            // of the array
            arr[0] = arr[size - 1];

            // call heapify-down on the root node
            Heapify(arr, 0, size - 1);

            return top;
        }

        // Function to perform heapsort on array `A` of size `n`
        internal static void Sort(int[] arr)
        {
            int length = arr.Length;

            // Build the heap, starting at the first internal nodes (n-2) / 2 to the root node
            int i = (length - 2) / 2;
            while (i >= 0)
            {
                Heapify(arr, i--, length);
            }

            // Pop from the heap till it becomes "empty"
            // What we are doing is pop the largest element, put it on the back of the array,
            // and then saying that the array has the length = length - 1, which means we disconsider the poped numbers
            // as being already sorted
            while (length > 0)
            {
                arr[length - 1] = Pop(arr, length);
                length--;
            }
        }
    }
}
