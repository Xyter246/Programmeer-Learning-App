using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmeer_Learning_App.Importing;

public class TXTFileReader : IFileReader
{
    public Program Readfile(string filename)
    {
        Program program = new Program();
        StreamReader sr = new StreamReader(filename);
        string? line = sr.ReadLine();

        while (line != null) {
            // READ LINE and convert to Command
        }

        return program;
    }
}
