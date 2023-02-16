namespace Algorithms
{
    internal static class MergeSort
    {
        // Partition the array and then merge it back together, sorting it on each merge
        // using the aux array. By starting the sort with small chunks, we ensure that
        // when we merge larger chunks the lower and higher parts are already sorted.
        // Exemplifying: if we know that lower[] and higher[] are sorted, a simple 
        // merge of them using the Merge() function bellow will sufice to have a full sorted array.

        // It is stable, meaning that items with the same value will not change positions

        // n log(n)
        public static void Sort(int[] arr)
        {
            var aux = new int[arr.Length];

            Sort(arr, aux, 0, arr.Length - 1);
        }

        static void Sort(int[] arr, int[] aux, int low, int high)
        {
            if (high <= low)
            {
                return;
            }

            var mid = low + ((high - low) / 2);

            Sort(arr, aux, low, mid);
            Sort(arr, aux, mid + 1, high);

            Merge(arr, aux, low, mid, high);
        }

        static void Merge(int[] arr, int[] aux, int low, int mid, int high)
        {
            int arrLowWalk = low, auxLowWalk = low, arrHighWalk = mid + 1;

            while (arrLowWalk <= mid && arrHighWalk <= high)
            {
                if (arr[arrLowWalk] <= arr[arrHighWalk])
                {
                    aux[auxLowWalk++] = arr[arrLowWalk++];
                }
                else
                {
                    aux[auxLowWalk++] = arr[arrHighWalk++];
                }
            }

            // The lower part might be smaller
            while (arrLowWalk <= mid)
            {
                aux[auxLowWalk++] = arr[arrLowWalk++];
            }
            // XOR
            // The higher part might be smaller
            while (arrHighWalk <= high)
            {
                aux[auxLowWalk++] = arr[arrHighWalk++];
            }

            for (int i = low; i <= high; i++)
            {
                arr[i] = aux[i];
            }
        }
    }
}
