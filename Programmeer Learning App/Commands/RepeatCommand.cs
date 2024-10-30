using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmeer_Learning_App.Commands;

public class RepeatCommand : Command
{
    public List<Command> Commands;
    private readonly int _repeatCount;

    public RepeatCommand(int repeatCount, List<Command>? commands = null)
    {
        _repeatCount = repeatCount;

        commands ??= new List<Command>();
        Commands = commands;
    }

    public override void Execute(Player player)
    {
        for (int i = 0; i < _repeatCount; i++)
            ExecuteCycle(player);
        
        void ExecuteCycle(Player player)
        {
            foreach (Command command in Commands)
                command.Execute(player);
        }
    }

    public void Add(Command cmd)
        => this.Commands.Add(cmd);

    public override string ToString()
    {
        string rtnString = $"RepeatCommand {_repeatCount} [ ";

        foreach (Command cmd in this.Commands)
            rtnString += cmd.ToString() + ", ";

        return rtnString + " ]";
    }
}
