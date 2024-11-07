namespace Programmeer_Learning_App.Exporting;

public class TXTFileWriter : IFileWriter
{
    /// <summary>
    /// Writes a Program to Disc using a Windows Dialog.
    /// </summary>
    /// <param name="program">The Program to be exported to *.txt</param>
    public static void WriteFile(Program program)
    {
        // Open a FileDialog
        SaveFileDialog sfd = new SaveFileDialog();
        sfd.Filter = @"Text Documents (*.txt)|*.txt|All files (*.*)|*.*";
        if (sfd.ShowDialog() != DialogResult.OK) return;

        // Write it to Disc
        StreamWriter sw = new StreamWriter(sfd.FileName);
        WriteList(sw, program.Commands, 0);
        sw.Close();
    }

    /// <summary>
    /// Writes a List using a predetermined StreamWriter and indentation.
    /// </summary>
    /// <param name="sw">StreamWriter instance used to write to disc.</param>
    /// <param name="commands">List of Commands to be written to file.</param>
    /// <param name="indentCount">current indentation count.</param>
    private static void WriteList(StreamWriter sw, List<Command> commands, int indentCount)
    {
        foreach (Command command in commands) {
            // Calculate indentation
            string indentation = "";
            for (int i = 0; i < indentCount; i++)
                indentation += "\t";
            
            sw.WriteLine(indentation + command);
            
            // If it was a LoopCommand, there might be more commands to write to file. Add those as well.
            if (command is LoopCommand lpcmd)
                WriteList(sw, lpcmd.Commands, indentCount + 1);
        }
    }
}
