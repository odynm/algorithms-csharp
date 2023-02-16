namespace Algorithms
{
    internal static class BinarySearch
    {
        public static Item? Search(Item[] list, int findId)
        {
            return SearchSpan(list, findId, 0, list.Length);
        }
        static Item? SearchSpan(Item[] list, int findId, int low, int high)
        {
            var half = low + ((high - low) / 2);
            var value = list[half];
            if (value.Id == findId)
                return value;

            if (findId < value.Id)
            {
                return SearchSpan(list, findId, 0, half);
            }
            else
            {
                return SearchSpan(list, findId, half, list.Length);
            }
        }
    }
}
