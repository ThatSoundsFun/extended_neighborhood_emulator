using System;
using System.IO;
using System.Linq;
using System.Text;

//Handles the output file and all of the data that will be added to it.
static class Output {
    private const string extension = ".RULE";

    private static readonly string fileName;
    private static StringBuilder output = new StringBuilder();

    static Output() {
        fileName = string.Format(
            "B{0}_S{1}_D2", 
            String.Join(",", Input.Born.Select(i => i.ToString())),
            String.Join(",", Input.Survive.Select(i => i.ToString()))
        );

        if (File.Exists(Input.Directory + fileName + extension)) {
            Console.WriteLine("Error: Output-file already exist.");
            Program.Exit();
        }
    }

    //Adds a line to the output.
    public static void AddLine(string line) {
        output.AppendLine(line);
    }

    //Writes all the lines to the output-file.
    public static void WriteRuleTable() {
        using (var file = new StreamWriter(Input.Directory + fileName + extension, append: true)) {
            file.WriteLine(Template.TopPart, fileName);
            file.WriteLine(output);
            file.WriteLine(Template.BottomPart);
        }
    }
}
