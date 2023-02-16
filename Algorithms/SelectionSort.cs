namespace Algorithms
{
    // Is not that different from Insertion, but Insertion moves all items to the right while finding where to put 
    // the item.
    // Selection on the other hand finds where to put the item and simply swaps with the item that was there before
    // O(n^2)
    internal static class SelectionSort
    {
        internal static void Swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        internal static void Sort(int[] arr)
        {
            // For each element in array we are look for a number that's smaller
            for (int i = 0; i < arr.Length; i++)
            {
                // Starting minimum index is itself
                int minIndex = i;
                // Look for all items post i
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[j] < arr[minIndex])
                    {
                        // We find out a item that's less than our courrent minimum
                        minIndex = j;
                    }
                }

                // Swap the current i with the smallest item we found
                Swap(arr, i, minIndex);
            }
        }
    }
}
