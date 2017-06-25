using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

//Handles the input file and all of its data.
static class Input {
    private const string fileName = "Input.json";
    private const string @default =
@"{
  ""Directory"": "",
  ""Born"": [],
  ""Survive"": []
}";

    public static readonly string Directory;
    public static readonly IEnumerable<int> Born;
    public static readonly IEnumerable<int> Survive;

    static Input() {
        dynamic input = readInput();

        Directory = input.Directory.ToObject<string>();
        if (Input.Directory != string.Empty) {
            Directory = Input.Directory.TrimEnd('/') + "/";
        }
        Born = input.Born.ToObject<HashSet<int>>();
        Survive = input.Survive.ToObject<HashSet<int>>();

        checkInput();
    }

    //Returns the data input.
    //Exits out the application early if the input-file does not exist or is unreadable.
    private static object readInput() {
        try {
            //forward slashes get replaced by backslashes as json.net can't deserialize with them.
            return JsonConvert.DeserializeObject(File.ReadAllText(fileName).Replace('\\','/'));
        } catch (FileNotFoundException) {
            Console.WriteLine("Error: File not found.");
            Console.WriteLine("Creating new input-file...");
            File.WriteAllText(fileName, @default);
        } catch (PathTooLongException) {
            Console.WriteLine("Error: File path is too long.");
        } catch (UnauthorizedAccessException) {
            Console.WriteLine("Error: File cannot be accessed.");
        } catch (DirectoryNotFoundException) {
            Console.WriteLine("Error: Directory not found");
        } catch (JsonReaderException) {
            Console.WriteLine("Error: Input-file is unreadable.");
        } catch (Exception e) {
            Console.WriteLine("Error: {0}", e);
        }

        Program.Exit();
        throw new ArgumentException("This should never happen.");
    }

    private static void checkInput() {
        if (Born.Count() != 0) {
            if (Born.Min() < 1) {
                Console.WriteLine("Error: Born List must not contain numbers less than 1");
                Program.Exit();
            }
            if (Born.Max() > 20) {
                Console.WriteLine("Error: Survive List must not contain numbers greater than 20");
                Program.Exit();
            }
        }

        if (Survive.Count() != 0) {
            if (Survive.Min() < 0) {
                Console.WriteLine("Error: Survive List must not contain numbers less than 0");
                Program.Exit();
            }
            if (Survive.Max() > 20) {
                Console.WriteLine("Error: Born List must not contain numbers greater than 20");
                Program.Exit();
            }
        }
    }
}