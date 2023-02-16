namespace Algorithms
{
    // The basic premisse is to take each element and find where to put it in the array.
    // So we will take each element and look down in the array where we need to put it
    // O(n^2)
    internal static class InsertionSort
    {
        internal static void Sort(int[] arr)
        {
            // Start from the second element on
            for (int i = 1; i < arr.Length; i++)
            {
                int current = arr[i];
                int j = i;

                // Iterate DOWN, while we have not reached 0 and the *left value (j-1)* is biggest than the current one
                while (j > 0 && arr[j - 1] > current)
                {
                    arr[j] = arr[j - 1]; // Move the *left value (j-1)* to the right, to make space for the insertion
                    j--; // Go to the next value
                }

                arr[j] = current; // We found the place to insert it
            }
        }
    }
}
