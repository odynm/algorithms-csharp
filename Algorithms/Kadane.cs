namespace Algorithms
{
    // Kadane's algorithm is used to find the contiguos subarray with the largest sum
    // Input:  {-2, 1, -3, 4, -1, 2, 1, -5, 4}
    // Output: {4, -1, 2, 1} with sum 6
    // Solved by taking the max sum ending at each index and comparing with the maxSoFar
    // If the maxSumEnding here is less than the current element, we use the current element to start a new subarray
    // O(n)
    internal static class Kadane
    {
        public static int Run(int[] arr)
        {
            int maxSoFar = int.MinValue;

            int maxEndingHere = 0;

            for (int i = 0; i < arr.Length; i++)
            {
                var current = arr[i];

                maxEndingHere += current;

                // Use the full sum OR start a new sum using the current value if the sum is less
                maxEndingHere = Math.Max(maxEndingHere, current);

                maxSoFar = Math.Max(maxSoFar, maxEndingHere);
            }

            return maxSoFar;
        }
    }
}
