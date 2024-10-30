using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmeer_Learning_App.Exporting;
internal interface IFileWriter
{
    void WriteFile(string filepath);
}
