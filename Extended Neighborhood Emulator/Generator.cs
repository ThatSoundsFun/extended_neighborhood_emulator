﻿using System;
using System.Collections.Generic;

//Contains a 5x5 grid used for generating all transitions.
//currently unused.
static class Generator {
    private static Cell[,] grid = new Cell[5, 5];

    //Generates all the cell birth transition
    public static void GenerateBirthTransitions() {
        foreach (var neighborCount in Input.Born) {
            //Output.AddLine("#@\"");
            grid[2, 2] = Cell.OFF;
            writeComment(neighborCount);
            foreach (var permutation in Permutation.PermutateCells(neighborCount, 20)) {
                fillgrid(permutation);
                outputGrid();
            }
            //Output.AddLine("#\",");
        }
    }

    //Generates all the cell survival transition.
    public static void GenerateSurvivalTransition() {
        foreach (var neighborCount in Input.Survive) {
            //Output.AddLine("#@\"");
            grid[2, 2] = Cell.ON;
            writeComment(neighborCount);
            foreach (var perms in Permutation.PermutateCells(neighborCount, 20)) {
                fillgrid(perms);
                outputGrid();
            }
            //Output.AddLine("#\",");
        }
    }

    //Writes comment about the group of transitions.
    private static void writeComment(int neighbors) {
        string center;

        if (grid[2, 2] == Cell.OFF) {
            center = "OFF_COUNT";
        } else {
            center = "ON_COUNT";
        }
        Output.AddLine("#overrides COUNT_STATES -> OFF");
        Output.AddLine(string.Format("#{0} -> ON when there's {1} neighbor(s)", center, neighbors));
    }

    //Transfers the cell permutation to the grid.
    //The corners are excluded as they are not part of the extended neighborhood.
    //The center is also excluded as it is already set.
    private static void fillgrid(IEnumerable<Cell> bitEnumerable) {
        var bitEnumerator = bitEnumerable.GetEnumerator();

        bitEnumerator.MoveNext();

        for (int x = 0; x <= 4; x++) {
            for (int y = 0; y <= 4; y++) {
                //excluded points
                if (!(x == 0 && y == 0) && //northeast corner
                    !(x == 0 && y == 4) && //northwest corner
                    !(x == 4 && y == 0) && //southeast corner
                    !(x == 4 && y == 4) && //southwest corner
                    !(x == 2 && y == 2)) { //center

                    grid[x, y] = bitEnumerator.Current;
                    bitEnumerator.MoveNext();
                }
            }
        }
    }

    //Gets cell transition from grid, then adds it to output
    private static void outputGrid() {
        Output.AddLine(
            String.Format(
                "{0},{1},{2},{3},{4},{5},{6},{7},{8},1", 
                grid[2, 2].ToCountGroup(0),                    //center              
                grid[2, 1].ToCountState(countNeighbors(2, 1)), //north
                grid[3, 1].ToCountGroup(1),                    //northeast
                grid[3, 2].ToCountState(countNeighbors(3, 2)), //east
                grid[3, 3].ToCountGroup(2),                    //southeast
                grid[2, 3].ToCountState(countNeighbors(2, 3)), //south
                grid[1, 3].ToCountGroup(3),                    //southwest
                grid[1, 2].ToCountState(countNeighbors(1, 2)), //west
                grid[1, 1].ToCountGroup(4)                     //northwest
           )
       );
    }

    //Counts the number of on-cells around a center.
    private static int countNeighbors(int xCenter, int yCenter) {
        var count = 0;

        for (var x = xCenter - 1; x <= xCenter + 1; x++) {
            for (var y = yCenter - 1; y <= yCenter + 1; y++) {
                if (grid[x, y] == Cell.ON && !(x == xCenter && y == yCenter)) {
                    count++;
                }
            }
        }
        return count;
    }

    //Used for debugging the grid.
    private static void printGrid() {
        for (var y = 0; y <= 4; y++) {
            for (var x = 0; x <= 4; x++) {
                if (grid[x, y] == Cell.OFF) {
                    Console.Write("0");
                } else {
                    Console.Write("1");
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine("north:{0}", countNeighbors(2, 1));
        Console.WriteLine("south:{0}", countNeighbors(2, 3));
        Console.WriteLine("west:{0}", countNeighbors(1, 2));
        Console.WriteLine("east:{0}", countNeighbors(3, 2));
        Console.ReadLine();
    }
}