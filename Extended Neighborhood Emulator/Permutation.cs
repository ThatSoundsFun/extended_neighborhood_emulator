using System;
using System.Collections.Generic;
using System.Linq;

static class Permutation {
    //Returns a list of permutations of a bit.
    public static IEnumerable<IEnumerable<bool>> PermutateBits(int onBits, int total) {
        int min = Int32.MaxValue >> (31 - onBits);
        int max = min << (total - onBits);

        for (int i = min; i <= max; i++) {
            if (countBits(i) == onBits) {
                yield return Convert.ToString(i, 2).PadLeft(total, '0').Select(c => c == '1').ToArray();
            }
        }
    }

    //Counts the number of bits on in the value.
    private static int countBits(int value) {
        var count = 0;

        while (value != 0) {
            if (value % 2 == 1) {
                count++;
            }
            value >>= 1;
        }
        return count;
    }
}
