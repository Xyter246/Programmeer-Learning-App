using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programmeer_Learning_App.Commands;
using Programmeer_Learning_App.Enums;

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
                if (cmd is RepeatCommand rptcmd)
                    count = RecursiveRepeat(rptcmd.Commands, count);
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
                if (cmd is RepeatCommand rptcmd)
                    maxDepth = Math.Max(maxDepth, RecursiveRepeat(rptcmd.Commands, currentDepth + 1));            
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

    /// <summary>
    /// Calculates the maximally needed BoardSize for a given Program.
    /// </summary>
    /// <param name="program">The Program which is being tested.</param>
    /// <param name="p">The Player instance which is used for this Program.</param>
    /// <returns>A Point and Size tuple. The Point is the Top-Left corner Point in the Grid, and the Size is the minimal Size of the Grid from that Point.</returns>
    public static (Point, Size) MaxGridSize(Program program, Player p)
    {
        Player player = (Player)p.Clone();
        int minX = player.Pos.X, maxX = player.Pos.X;
        int minY = player.Pos.Y, maxY = player.Pos.Y;

        while (!program.HasEnded) {
            program.StepOnce(player);
            minX = Math.Min(minX, player.Pos.X);
            minY = Math.Min(minY, player.Pos.Y);
            maxX = Math.Max(maxX, player.Pos.X);
            maxY = Math.Max(maxY, player.Pos.Y);
        }

        return (new Point(minX, maxY), new Size(maxX - minX, maxY - minY));
    }
}