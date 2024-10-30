using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programmeer_Learning_App.Commands;

namespace Programmeer_Learning_App;

public static class Metric
{
    public static int NumOfCommands(Program program)
    {
        return RecursiveRepeat(program.Commands, count: 0);

        int RecursiveRepeat(List<Command> commands, int count)
        {
            foreach (Command cmd in commands) {
                count++;
                if (cmd is RepeatCommand rptcmd) {
                    count = RecursiveRepeat(rptcmd.Commands, count);
                }
            }
            return count;
        }
    }

    public static int MaxNestingDepth(Program program)
    {
        return RecursiveRepeat(program.Commands, currentDepth: 0);

        int RecursiveRepeat(List<Command> commands, int currentDepth)
        {
            int maxDepth = currentDepth;
            foreach (Command cmd in commands)
                if (cmd is RepeatCommand rptcmd) {
                    maxDepth = Math.Max(maxDepth, RecursiveRepeat(rptcmd.Commands, currentDepth + 1));
                }
            
            return maxDepth;
        }
    }

    public static int NumOfRepeatCommands(Program program)
    {
        return RecursiveRepeat(program.Commands, count: 0);

        int RecursiveRepeat(List<Command> commands, int count)
        {
            foreach (Command cmd in commands)
                if (cmd is RepeatCommand rptcmd)
                    count = RecursiveRepeat(rptcmd.Commands, count + 1);
            return count;
        }
    }
}