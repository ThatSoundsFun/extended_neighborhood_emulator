using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

static class Input {
    private const string fileName = "Input.json";
    private const string @default =
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
    //Exits out the application early if the input-file does not exist or is unreadable.
    private static object readInputFile() {
        try {
            return JsonConvert.DeserializeObject(File.ReadAllText(fileName));
        } catch (FileNotFoundException) {
            Console.WriteLine("Error: Input-file not found.");
            Console.WriteLine("Creating new input-file...");
            File.WriteAllText(fileName, @default);
            Program.Exit();
        } catch (JsonException) {
            Console.WriteLine("Error: Input-file is unreadable.");
            Program.Exit();
        } catch (Exception e) {
            Console.WriteLine("Error: {0}", e);
            Program.Exit();
        }

        throw new ArgumentException("This should never happen.");
    }
}