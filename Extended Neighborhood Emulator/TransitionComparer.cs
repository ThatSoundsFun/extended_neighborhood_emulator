using System;
using System.Collections.Generic;
using System.Linq;

//Compares two transitions to see if they're rotational or reflectional duplicates of eachother.
//Currently unused.
sealed class TransitionComparer : IEqualityComparer<string> {
    //Returns true if both lines are reflectional or rotational duplicates.
    //Returns false if either lines are comments.
    bool IEqualityComparer<string>.Equals(string left, string right) {
        if (left[0] == '#' || right[0] == '#') {
            return false;
        }
        left = removeVariants(left);
        right = removeVariants(right);
        return (
            left == right ||
            left == getRotation(right, 2) ||
            left == getRotation(right, 4) ||
            left == getRotation(right, 6) ||
            left == getReflection(right) ||
            left == getReflection(getRotation(right, 2)) ||
            left == getReflection(getRotation(right, 4)) ||
            left == getReflection(getRotation(right, 6))
        );
    }

    //Gets the hashcodes of all rotations and reflection of the line and returns the lowest one.
    //This is so that lines that are duplicates of each other would always have the same hashcode.
    int IEqualityComparer<string>.GetHashCode(string line) {
        if (line[0] == '#') {
            return line.GetHashCode();
        }
        line = removeVariants(line);
        return new int[]{
            line.GetHashCode(),
            getRotation(line, 2).GetHashCode(),
            getRotation(line, 4).GetHashCode(),
            getRotation(line, 6).GetHashCode(),
            getReflection(line).GetHashCode(),
            getReflection(getRotation(line, 2)).GetHashCode(),
            getReflection(getRotation(line, 4)).GetHashCode(),
            getReflection(getRotation(line, 6)).GetHashCode(),
        }.Min();
    }

    //Removes the variations from the group count-states so that they can be compared.
    private static string removeVariants(string line) {
        var cells = Split(line);

        for (var i = 0; i < cells.Length; i++) {
            if (cells[i].Contains("0c")) {
                cells[i] = "0c";
            } else
            if (cells[i].Contains("1c")) {
                cells[i] = "1c";
            }
        }

        return string.Join(",", cells);
    }

    //Returns the rotation of the line by a value.
    //Each value rotates the surrounding cells by 45 degrees.
    private static string getRotation(string line, int rotation) {
        var cells = Split(line);

        var surrounding = cells.Take(9).Skip(1);

        var newSurrounding = surrounding.Skip(rotation).Concat(surrounding.Take(rotation));

        return string.Join(",", cells[0], string.Join(",", newSurrounding), cells[9]);
    }
    
    //Returns the vertical reflection of the transition.
    private static string getReflection(string line) {
        var cells = Split(line);

        return string.Join(
            ",",
            cells[0],
            cells[1],
            cells[8],
            cells[7],
            cells[6],
            cells[5],
            cells[4],
            cells[3],
            cells[2],
            cells[9]
        );
    }

    //returns a list of each cell of the transition.
    private static string[] Split(string line) {
        var cells = line.Split(',');
        if (cells.Length != 10) {
            throw new ArgumentException("Error: line must contain exactly 10 cell entries.");
        }
        return cells;
    }
}
