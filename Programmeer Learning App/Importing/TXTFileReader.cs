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
    public static Program Readfile()
    {
        OpenFileDialog ofd = new OpenFileDialog();
        ofd.Filter = @"Text Documents (*.txt)|*.txt|All files (*.*)|*.*";
        if (ofd.ShowDialog() != DialogResult.OK) 
            throw new ArgumentNullException();

        StreamReader sr = new StreamReader(ofd.FileName);
        List<Command> commands = new List<Command>();
        string? line = sr.ReadLine();
        while (line is not null) {
            string[] words = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            commands.Add(ConvertCommand(words));
        }

        sr.Close();
        return new Program(commands);
    }

    private static Command ConvertCommand(string[] words) 
        => CommandFactory.CreateInstance(words) ?? throw new NotImplementedException();
}
