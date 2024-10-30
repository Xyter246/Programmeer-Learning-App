using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmeer_Learning_App.Exporting;
public interface IFileWriter
{
    static abstract void WriteFile(Program program);
}
