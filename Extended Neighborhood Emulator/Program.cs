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
        Console.WriteLine("Building Rule Table...");
        foreach (var birthCount in Input.Born) {
            Output.AddLine(Pregenerated.Born[birthCount]);
        }
        foreach (var survivalCount in Input.Survive) {
            Output.AddLine(Pregenerated.Survive[survivalCount]);
        }
        Output.WriteRuleTable();
    }
}
