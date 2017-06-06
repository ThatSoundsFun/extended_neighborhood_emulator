using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

static class Program {
    public static readonly string OutputFileName;
    public static readonly StreamWriter OutputFile;

    static Program() {
        OutputFileName = string.Format("B{0}_S{1}_D2.RULE", String.Join(",", Input.Born.Select(i => i.ToString())), 
                                                            String.Join(",", Input.Survive.Select(i => i.ToString()))
        );

        if (File.Exists(OutputFileName)) {
            Console.WriteLine("Error: Output-file already exist.");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            Environment.Exit(0);
            throw new ArgumentException("Unable to exit automatically.");
        }

        OutputFile = new StreamWriter(OutputFileName, append: true);
        OutputFile.AutoFlush = true;
    }

    static void Main(string[] args) {
        Console.WriteLine("Generating Rule Table...");
        OutputFile.WriteLine(Template.TopPart);
        foreach (var i in Input.Born) {
            Grid.NeighborCount = i;
            Grid.Center = Cell.OFF;
            Grid.ProcessInput();
        }
        foreach (var i in Input.Survive) {
            Grid.NeighborCount = i;
            Grid.Center = Cell.ON;
            Grid.ProcessInput();
        }
        OutputFile.WriteLine(Template.BottomPart);
    }
}
