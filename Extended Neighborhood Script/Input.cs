using Newtonsoft.Json;
using System.IO;
using System;

static class Input {
    const string InputFileName = "Input.json";
    const string Default =
@"{
  ""OutputFileName"": """",
  ""Born"": [],
  ""Survive"": []
}";

    //Returns the data input.
    //Exits out application if the input-file does not exist or is unreadable.
    public static object getInput() {
        try {
            return JsonConvert.DeserializeObject(File.ReadAllText(InputFileName));
        } catch (FileNotFoundException) {
            Console.WriteLine("Error: Input-file not found.");
            Console.WriteLine("Creating new input-file.");

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
        return null;
    }
}