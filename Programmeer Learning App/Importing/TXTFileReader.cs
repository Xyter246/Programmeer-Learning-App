using Programmeer_Learning_App.User_Interface;

namespace Programmeer_Learning_App.Importing;

public class TXTFileReader : IFileReader
{
    /// <summary>
    /// Creates a Program instance from a Saved Text Document on the User's device.
    /// </summary>
    /// <returns>A Program instance, or Null if no valid file or format was found.</returns>
    public static List<CommandLabel>? Readfile()
    {
        // Open a FileDialog
        OpenFileDialog ofd = new OpenFileDialog();
        ofd.Filter = @"Text Documents (*.txt)|*.txt|All files (*.*)|*.*";
        if (ofd.ShowDialog() != DialogResult.OK || ofd.FileName == string.Empty)
            return null;
        
        // Start reading from file.
        StreamReader sr = new StreamReader(ofd.FileName);
        (List<Command>? commands, _) = Readlist(sr, 0);
        
        sr.Close();
        return commands?.Select(c => c.ToLabel()).ToList();
    }

    /// <summary>
    /// Read a list of Commands from a File.
    /// </summary>
    /// <param name="sr">StreamReader which is used throught a process.</param>
    /// <param name="currentIndent">The current indentation of commands.</param>
    /// <returns>A tuple of a list of Commands and a string. The former is required for output; The latter is used internally for recursive calls.</returns>
    private static (List<Command>?, string?) Readlist(StreamReader sr, int currentIndent)
    {
        List<Command> commands = new List<Command>();
        string? line = sr.ReadLine();
        while (line is not null) {
            // Instantiate the String as a Command.
            string[] words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int indent = CalcIndent(words);
            Command? cmd = ConvertCommand(words.RemoveTabs());

            if (cmd is null) {
                GameWindow.ShowError($"Text file contains invalid contents: {string.Concat(words)}");
                return (null, null);
            }

            // if the new Command is less indented than the current Command, go back once.
            if (indent < currentIndent)
                return (commands, line);
            commands.Add(cmd);

            // if this is a LoopCommand, add new Commands as part of this Command.
            if (cmd is LoopCommand lpcmd) {
                (List<Command>? list, line) = Readlist(sr, currentIndent + 1);
                if (list is null)
                    return (null, null);
                lpcmd.Commands = list;
                continue;
            }
            line = sr.ReadLine();
        }
        return (commands, null);
    }

    /// <summary>
    /// Calculates how many Tabs there are at the start of a string[].
    /// </summary>
    /// <param name="words"></param>
    /// <returns>The number of Tabs at the start of the string[].</returns>
    private static int CalcIndent(string[] words)
        // 1 being the word itself.
        => words[0].Split('\t').Length - 1; 

    /// <summary>
    /// Creates a Command instance from a series of strings.
    /// </summary>
    /// <param name="words">A series of strings to convert to a Command.</param>
    /// <returns>A Command instance, or Null if the string is invalid.</returns>
    private static Command? ConvertCommand(string[] words) 
        => CommandFactory.CreateInstance(words) ?? null;
}

/// <summary>
/// File-scoped static class for a pretty looking method-extension of string
/// </summary>
file static class Func
{
    /// <summary>
    /// Remove tabs at the start of a string[]
    /// </summary>
    /// <param name="words"></param>
    /// <returns>The same string[] but without any '\t' at the start.</returns>
    public static string[] RemoveTabs(this string[] words)
    {
        words[0] = words[0].Split('\t')[^1];
        return words;
    }
}