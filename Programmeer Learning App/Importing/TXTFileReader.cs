using Programmeer_Learning_App.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Programmeer_Learning_App.Importing;

public class TXTFileReader : IFileReader
{
    /// <summary>
    /// Creates a Program instance from a Saved Text Document on the User's device.
    /// </summary>
    /// <returns>A Program instance, or Null if no valid file was found.</returns>
    public static Program? Readfile()
    {
        OpenFileDialog ofd = new OpenFileDialog();
        ofd.Filter = @"Text Documents (*.txt)|*.txt|All files (*.*)|*.*";
        if (ofd.ShowDialog() != DialogResult.OK || ofd.FileName == string.Empty)
            return null;

        StreamReader sr = new StreamReader(ofd.FileName);
        List<Command> commands = Readlist(sr, 0);
        
        sr.Close();
        return new Program(commands);

    }

    private static List<Command> Readlist(StreamReader sr, int currentIndent)
    {
        List<Command> commands = new List<Command>();
        while (sr.ReadLine() is { } line) {
            string[] words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int indent = CalcIndent(words);
            Command? cmd = ConvertCommand(CleanWords(words));

            if (cmd is null)
                throw new ArgumentException($"Text file contains invalid contents: {words}");
            if (indent < currentIndent)
                return commands;
            if (cmd is LoopCommand lpcmd)
                lpcmd.Commands = Readlist(sr, currentIndent + 1);

            commands.Add(cmd);
        }

        return commands;
    }

    private static int CalcIndent(string[] words) 
        => words[0].Split('\t').Length - 1;

    private static string[] CleanWords(string[] words)
    {
        words[0] = words[0].Split('\t')[^1];
        return words;
    }

    /// <summary>
    /// Creates a Command instance from a series of strings.
    /// </summary>
    /// <param name="words">A series of strings to convert to a Command.</param>
    /// <returns>A Command instance, or Null if the string is invalid.</returns>
    private static Command? ConvertCommand(string[] words) 
        => CommandFactory.CreateInstance(words) ?? null;
}
