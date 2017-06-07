using System;

static class Program {
    //Exits out of program early.
    public static void Exit() {
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey();
        try {
            Environment.Exit(0);
        } catch (Exception) {
            Console.WriteLine("Error: Unable to exit.");
            Console.WriteLine("Please exit console manually.");
            while (true) { }
        }
    }

    private static void Main(string[] args) {
        Console.WriteLine("Generating Rule Table...");
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
        Output.WriteToFile();
    }
}
