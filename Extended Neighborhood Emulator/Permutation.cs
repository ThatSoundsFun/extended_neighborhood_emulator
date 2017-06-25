using System;
using System.Collections.Generic;

//Currently unused.
static class Permutation {
    //Returns a list of all permutations with the same amount of on-cells.
    public static IEnumerable<IEnumerable<Cell>> PermutateCells(int onCells, int total) {
        int min = Int32.MaxValue >> (31 - onCells);
        int max = min << (total - onCells);

        for (int i = min; i <= max; i++) {
            if (countBits(i) == onCells) {
                yield return ToCellList(i, total);
            }
        }
    }

    //Counts the number of bits on in the value.
    //It does this by continuously checking if the right-most bit is on, then shifts it to the right.
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

    //Returns the value as a list of cells.
    //Each on-bit and off-bit in the value will be an on-cell or an off-cell, respectivley.
    private static IEnumerable<Cell> ToCellList(int value, int max) {
        for (int x = 0; x <= max; x++) {
            if (value % 2 == 0) {
                yield return Cell.OFF;
            } else {
                yield return Cell.ON;
            }
            value >>= 1;
        }
    }
}
