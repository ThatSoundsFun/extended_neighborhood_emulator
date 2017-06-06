using Newtonsoft.Json;
using System.IO;
using System;
using System.Collections.Generic;

static class Input {
    const string InputFileName = "Input.json";
    const string Default =
@"{
  ""Born"": [],
  ""Survive"": []
}";

    public static readonly HashSet<int> Born;
    public static readonly HashSet<int> Survive;

    static Input() {
        dynamic input = readInputFile();

        Born = input.Born.ToObject<HashSet<int>>();
        Survive = input.Survive.ToObject<HashSet<int>>();
    }

    //Returns the data input.
    //Exits out application if the input-file does not exist or is unreadable.
    private static object readInputFile() {
        try {
            return JsonConvert.DeserializeObject(File.ReadAllText(InputFileName));
        } catch (FileNotFoundException) {
            Console.WriteLine("Error: Input-file not found.");
            Console.WriteLine("Creating new input-file...");

            File.WriteAllText(InputFileName, Default);

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            Environment.Exit(0);
        } catch (JsonException) {
            Console.WriteLine("Error: Input-file is unreadable.");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            Environment.Exit(0);
        } catch (Exception e) {
            Console.WriteLine("Error: {0}", e);
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            Environment.Exit(0);
        }

        throw new ArgumentException("Unable to exit automatically.");
    }
}