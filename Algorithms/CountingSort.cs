namespace Algorithms
{
    // Sorts an array where the items lie in a range. Counts the total number of elements of each key
    // It can be used to find the most frequent character and is also used as a subroutine in the Radix sort algorithm, and because
    //   of this, should be stable
    // A "variation" of this, mainly the Step 2 of indexing items that share the same key, is used in compression,
    //   since freq and keyRange are the only needed items to decompress the content. The problem is that not many things
    //   can be meaningfully compressed in this way (requires a lot of items to repeat)
    //
    // O(n+k), where n size input and k the range of values
    internal static class CountingSort
    {
        // This assumes that all key integers are in the form 0...k-1
        // This means that all items inside arr will have a value between 0 and k-1
        internal static int[] Sort(int[] arr, int keyRange)
        {
            // Step 1: Count =====================
            // Sorted
            int[] output = new int[arr.Length];
            // Store each key frequency
            int[] freq = new int[keyRange + 1];

            // since all values are between 0 and k-1, we can use freq to count the frequency that they appear
            for (int i = 0; i < arr.Length; i++)
            {
                freq[arr[i]]++;
            }

            // Step 2: Index =====================
            // Now we are going to loop again and save the index that the next number starts on the sequence
            // Example: for [0]2 [1]5 [2]3 we will have [0]0 [1]2 [2]7
            int accumulator = 0;
            for (int i = 0; i < keyRange + 1; i++)
            {
                int count = freq[i];
                freq[i] = accumulator;
                accumulator += count;
            }

            // Step 3: Fill the output array =====================
            for (int i = 0; i < arr.Length; i++)
            {
                // Fill the output position using the frequency array's value at the position of the key arr[i] with the key value
                int key = arr[i];
                output[freq[key]] = key;
                // Since frequency has the starting place of the key (remember [0]0 [1]2 [2]7), we need to jump one place
                //    so that the next value of this key gets placed on then next position
                // NODE: This preserves the order of items with the same key
                // NODE: We could simplify this by NOT making it stable, 
                freq[key]++;
            }

            return output;
        }

        // Simpler way in which the order of the items with the same key is not preserved
        internal static int[] UnstabledSort(int[] arr, int keyRange)
        {
            // Store each key frequency
            int[] freq = new int[keyRange + 1];

            // since all values are between 0 and k-1, we can use freq to count the frequency that they appear
            for (int i = 0; i < arr.Length; i++)
            {
                freq[arr[i]]++;
            }

            // Take all keys, iterate through them on the freq array, and print the keyValue, while increasing the arr index in the
            //    process
            int index = 0;
            for (int i = 0; i < keyRange + 1; i++)
            {
                while (freq[i]-- > 0)
                {
                    arr[index++] = i;
                }
            }

            return arr;
        }
    }
}
