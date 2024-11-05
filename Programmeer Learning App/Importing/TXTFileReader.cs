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
        (LinkedList<Command> commands, _) = Readlist(sr, 0);
        
        sr.Close();
        return new Program(commands);
    }

    private static (LinkedList<Command>, string?) Readlist(StreamReader sr, int currentIndent)
    {
        LinkedList<Command> commands = new LinkedList<Command>();
        string? line = sr.ReadLine();
        while (line is not null) {
            // Instantiate the String as a Command.
            string[] words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int indent = CalcIndent(words);
            Command? cmd = ConvertCommand(words.RemoveTabs());

            if (cmd is null)
                throw new ArgumentException($"Text file contains invalid contents: {words}");

            // if the new Command is less indented than the current Command, go back once.
            if (indent < currentIndent)
                return (commands, line);
            commands.AddLast(cmd);

            // if this is a LoopCommand, add new Commands as part of this Command.
            if (cmd is LoopCommand lpcmd) {
                (lpcmd.Commands, line) = Readlist(sr, currentIndent + 1);
                continue;
            }
            line = sr.ReadLine();
        }
        return (commands, null);
    }

    private static int CalcIndent(string[] words) 
        => words[0].Split('\t').Length - 1;

    /// <summary>
    /// Creates a Command instance from a series of strings.
    /// </summary>
    /// <param name="words">A series of strings to convert to a Command.</param>
    /// <returns>A Command instance, or Null if the string is invalid.</returns>
    private static Command? ConvertCommand(string[] words) 
        => CommandFactory.CreateInstance(words) ?? null;
}

file static class Func
{
    public static string[] RemoveTabs(this string[] words)
    {
        words[0] = words[0].Split('\t')[^1];
        return words;
    }
}