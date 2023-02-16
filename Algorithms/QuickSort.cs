namespace Algorithms
{
    internal static class QuickSort
    {
        // We use a pivot as a base for a inplace sort of chunks
        // As opposite to mergesort, we start sorting bigger chunks,
        // and then increasing smaller ones, but because we are sorting around a given item (pivot),
        // it is guaranteed that: **every item of the start chunk is smaller than the items of the end chunk**
        // Or: any item lowerChunk[] < any item higherChunk[]
        // This ensures that we are progressively sorting the array

        // The pivot is defined as the last element of the chunk.
        // Items smaller than the pivot are placed before, then the pivot is placed in the middle,
        // and items bigger than the pivot are placed on the back.
        // IMPORTANT: the pivot is excluded from further partitioning on sorting!

        // It is not stable, so items will the same value can change positions

        // Best case n log(n), worst case n^2
        public static void Sort(int[] arr)
        {
            var aux = new int[arr.Length];

            Sort(arr, 0, arr.Length - 1);
        }

        static void Sort(int[] arr, int start, int end)
        {
            // Base case to stop recursion
            if (start >= end)
            {
                return;
            }

            int pivotIndex = SortChunk(arr, start, end);


            // Do that to smaller chunks - but the pivot is excluded (notice the pivotIndex - 1 and pivotIndex + 1)
            Sort(arr, start, pivotIndex - 1);
            Sort(arr, pivotIndex + 1, end);
        }

        static int SortChunk(int[] arr, int start, int end)
        {
            int pivotIndex = start; // Will get updated and returned
            int pivotValue = arr[end];

            // Take all items smaller than pivot and keep puting it where the pointer is
            for (int i = start; i < end; i++)
            {
                if (arr[i] < pivotValue)
                {
                    Swap(arr, i, pivotIndex++);
                }
            }

            // Place the pivot at the end of the items smaller than it
            Swap(arr, pivotIndex, end);

            // Now all remaining items are bigger than the pivot

            return pivotIndex;
        }

        static void Swap(int[] arr, int a, int b)
        {
            int temp = arr[a];
            arr[a] = arr[b];
            arr[b] = temp;
        }
    }
}
