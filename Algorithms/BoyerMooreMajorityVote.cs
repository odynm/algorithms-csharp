namespace Algorithms
{
    // In an array, find the element that appears more than n/2, where n is the array size
    // There's a couple of solutions:
    // Brute force: iterate through half of the array. For each of those elements, iterate through the whole array couting elements
    // Linear time: using hashing, iterate through the array adding the count to a hashtable (table[i]++). Return the element whos count
    //     is >n/2, if any
    // Boyer-Moore: linear time and space. Only works when the array has a majority! If not, it may return a incorrect output.
    //     It works by iterating through the sequence with index i, a counter c and a x candidate (i, c, x)
    //         * if c == 0, set x = arr[i] and c = 1
    //         * if c > 0, if(arr[i] == x) c++ else c--
    //     This is a clever solution because the item with the majority will always be able to maintain the position at x
    internal static class BoyerMooreMajorityVote
    {
        // The Boyer-Moore
        internal static int Majority(int[] nums)
        {
            int x = -1;
            int c = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                if (c == 0)
                {
                    x = nums[i];
                    c = 1;
                }
                else if (x == nums[i])
                {
                    c++;
                }
                else
                {
                    c--;
                }
            }

            return x;
        }
    }
}
