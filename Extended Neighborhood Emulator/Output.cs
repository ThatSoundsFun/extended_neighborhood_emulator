using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

static class Output {
    private const string extension = ".RULE";

    private static readonly string fileName;
    private static HashSet<string> output;

    static Output() {
        fileName = string.Format("B{0}_S{1}_D2", String.Join(",", Input.Born.Select(i => i.ToString())),
                                                 String.Join(",", Input.Survive.Select(i => i.ToString()))
        );
        if (File.Exists(fileName + extension)) {
            Console.WriteLine("Error: Output-file already exist.");
            Program.Exit();
        }

        output = new HashSet<string>();
    }

    //Adds a line that will added later to the output-file.
    //Duplicate lines will be removed as all the output is stored in a hashset.
    public static void AddLine(string str) {
        output.Add(str);
    }

    //Writes all the retrieved outputs to the output-file.
    public static void WriteToFile() {
        var file = new StreamWriter(fileName + extension, append: true);
        file.AutoFlush = true;

        file.WriteLine(Template.TopPart, fileName);
        foreach (string line in output) {
            file.WriteLine(line);
        }
        file.WriteLine(Template.BottomPart);
    }
}
