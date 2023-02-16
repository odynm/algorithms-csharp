namespace Algorithms
{
    // Knuth Morris and Pratt
    // This one is complex to understand and implement
    // Pattern matching in string, but instead of the naive approach that just keeps checking without remembering previous attempts
    // and having run time O(n*m), this clevearly remembers previous and has a runtime of O(n+m), n text length, m pattern length
    // KMP takes n time for patch matching and m time for construction of a partial match table
    // This works by creating a partial match table. This table stores at each index, the length of the
    // longest prefix that's equal to the longest suffix.
    // This allows us to discard some checking if we encounter a slice of text that doesn't match the table
    // It's no easy to explain, so there you go http://jakeboxer.com/blog/2009/12/13/the-knuth-morris-pratt-algorithm-in-my-own-words/
    // https://www.youtube.com/watch?v=4jY57Ehc14Y
    // LPS array: longes prefix suffix array
    internal static class KMP
    {
        // This will return true in the first match
        internal static bool FindMatch(string text, string pattern)
        {
            var textLength = text.Length;
            var patternLength = pattern.Length;

            // Pattern is empty
            if (patternLength == 0)
            {
                return false;
            }

            // Text length is less than pattern length
            if (textLength < patternLength)
            {
                return false;
            }

            // Build LPS array
            var lps = new int[patternLength];
            // Start at 1 because index 0 will always be 0
            for (int suffixWalk = 1, prefixWalk = 0; suffixWalk < patternLength;)
            {
                if (pattern[prefixWalk] == pattern[suffixWalk]) // Match
                {
                    lps[suffixWalk] = prefixWalk + 1; // We have +1 character that matches prefix and suffix
                    prefixWalk++; // Walk the prefix
                    suffixWalk++; // Walk the suffix
                }
                else // No match
                {
                    if (prefixWalk == 0) // If prefix has not walked yet (no match ever)
                    {
                        lps[suffixWalk] = 0; // The lps is zero
                        suffixWalk++; // Walk suffix
                    }
                    else
                    {
                        prefixWalk = lps[prefixWalk - 1]; // No match, but since prefix has walked, we can take the last total match
                                                          // and NOT increment the suffix to see if they will match in the next check
                    }
                }
            }

            // Match string
            for (int textWalk = 0, patternWalk = 0; textWalk < textLength;)
            {
                if (text[textWalk] == pattern[patternWalk])
                {
                    patternWalk++;
                    textWalk++;
                    // In a match, we increment everything
                }
                else
                {
                    if (patternWalk != 0) // No pattern walk...
                    {
                        patternWalk = lps[patternWalk - 1]; //...we will take the previous lps and not increment textWalk to check
                                                            // if this one is a match, so we can continue from there instead
                                                            // of starting from scrach
                    }
                    else
                    {
                        textWalk++; // No match and patternWalk is 0, just walk the text pointer
                    }
                }
                if (patternWalk == patternLength)
                {
                    // If we are able to get the patternWalk to the end of the pattern length, we found the string
                    return true;
                }
            }

            // No match in the whole text, return false
            return false;
        }
    }
}
