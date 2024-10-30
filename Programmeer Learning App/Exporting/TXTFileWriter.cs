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
        OpenFileDialog ofd = new OpenFileDialog();
        if (ofd.ShowDialog() != DialogResult.OK) return;

        StreamWriter sw = new StreamWriter(ofd.FileName);
        WriteList(program.Commands);
        sw.Close();
        return;

        void WriteList(List<Command> commands)
        {
            foreach (Command command in commands)
                sw.WriteLine(command.ToString());
        }
    }
}
