using System.Globalization;

namespace Algorithms
{
    // Selection algorithm to find the k'th smallest element in an unordered list.
    // For example, if the input is k=2, it will get the 3rd smallest element on the set (0 indexed)
    // It uses an approach similar to quicksort, paritioning the data in two based on the pivot, but instead of recursing
    //   in both sides, quick select only recurs to one side with its searching element.
    // The std C++ lib has a implementation of this agorithm called Introselect, which is more efficient

    // O(n), with worst case O(n^2)
    internal static class Quickselect
    {
        internal static int Select(int[] nums, int left, int right, int k)
        {
            // If has one element, return it
            if (left == right)
            {
                return nums[left];
            }

            int pivotIndex = ((right + left) / 2); // This could be a random number

            pivotIndex = Partition(nums, left, right, pivotIndex); // Sort the given range of "nums", using pIndex as the pivot

            // The pivot is in its final sorted position
            if (k == pivotIndex)
            {
                return nums[k];
            }
            else if (k < pivotIndex)
            {
                // Search only the lower part
                return Select(nums, left, pivotIndex - 1, k);
            }
            else // Else k > pIndex
            {
                // Search only the upper part
                return Select(nums, pivotIndex + 1, right, k);
            }
        }

        private static int Partition(int[] nums, int left, int right, int pivotIndex)
        {
            // Use pivotIndex as the pivot of the partition
            int pivot = nums[pivotIndex];

            // Move pivot to the end of the slice (right)
            Swap(nums, pivotIndex, right);

            // Elements less than the pivot will be pushed to the left of "pivotIndex"
            // Elements more than the pivot will be pushed to the right of "pivotIndex"
            // Equal elements don't matter

            // pivot is now the smaller (left) value
            var newPivotIndex = left;

            // From left to right
            for (int i = left; i < right; i++)
            {
                // Found a number less or equal to pivot
                if (nums[i] <= pivot)
                {
                    // Swap it with the current pivot and add the pivot index
                    Swap(nums, i, newPivotIndex);
                    newPivotIndex++;
                }
            }

            // Take the pivot value and place it back on the new found pivot index,
            // because all values after newPivotIndex are greater than the pivot value
            Swap(nums, newPivotIndex, right);

            return newPivotIndex;
        }

        private static void Swap(int[] nums, int i, int j)
        {
            int temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }
    }
}
