using Programmeer_Learning_App.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmeer_Learning_App.Importing;

public class TXTFileReader : IFileReader
{
    public Program Readfile(string filepath)
    {
        Program program = new Program();
        StreamReader sr = new StreamReader(filepath);
        
        for (string? line = sr.ReadLine(); 
             line != null; 
             line = sr.ReadLine()) 
        {
            string[] words = line.Split(' ');
            program.Add(ConvertCommand(words));
        }

        return program;
    }

    private Command ConvertCommand(string[] words)
    {
        Command command = null!;

        return command!;
    }
}
