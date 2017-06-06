using System;
using System.Collections.Generic;
using System.IO;

static class Program {
    public static readonly string OutputFileName;
    public static readonly StreamWriter OutputFile;

    private static readonly HashSet<int> born;
    private static readonly HashSet<int> survive;

    static Program() {
        dynamic input = Input.getInput();

        OutputFileName = input.OutputFileName;
        if (File.Exists(OutputFileName)) {
            Console.WriteLine("Error: Output-file already exist.");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            Environment.Exit(0);
        }

        born = input.Born.ToObject<HashSet<int>>();
        survive = input.Survive.ToObject<HashSet<int>>();

        OutputFile = new StreamWriter(OutputFileName, append: true);
        OutputFile.AutoFlush = true;
    }

    static void Main(string[] args) {
        Console.WriteLine("Generating Rule Table...");
        OutputFile.WriteLine(Template.TopPart);
        foreach (var i in born) {
            Grid.NeighborCount = i;
            Grid.Center = Cell.OFF;
            Grid.MainLoop();
        }
        foreach (var i in survive) {
            Grid.NeighborCount = i;
            Grid.Center = Cell.ON;
            Grid.MainLoop();
        }
        OutputFile.WriteLine(Template.BottomPart);
    }
}
