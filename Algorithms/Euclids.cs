namespace Algorithms
{
    // Finding the GCD (greatest common divisor)
    // Based on the principle that the GCD doesn't change if the larger number is replaced by its difference with the smaller number
    // Repeating this gives you sucessively smaller pairs until two numbers are equal. When that occurs, they are the GCD
    internal static class Euclids
    {
        internal static int GCD(int a, int b)
        {
            while (a != b)
            {
                if (a > b)
                {
                    a -= b;
                }
                else
                {
                    b -= a;
                }
            }

            return a;
        }
    }
}
