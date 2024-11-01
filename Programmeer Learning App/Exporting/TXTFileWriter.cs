using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programmeer_Learning_App.Commands;

namespace Programmeer_Learning_App.Exporting;

public class TXTFileWriter : IFileWriter
{
    public static void WriteFile(Program program)
    {
        SaveFileDialog sfd = new SaveFileDialog();
        sfd.Filter = @"Text Document |*.txt";
        if (sfd.ShowDialog() != DialogResult.OK) return;

        StreamWriter sw = new StreamWriter(sfd.FileName);
        WriteList(sw, program.Commands, 0);
        sw.Close();
    }

    private static void WriteList(StreamWriter sw, List<Command> commands, int indentCount)
    {
        foreach (Command command in commands) {
            string indentation = "";
            for (int i = 0; i < indentCount; i++)
                indentation += "\t";
            sw.WriteLine(indentation + command.ToString());
            if (command is RepeatCommand rptcmd)
                WriteList(sw, rptcmd.Commands, indentCount + 1);
        }
    }
}
