using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmeer_Learning_App.Commands;

public class RepeatCommand : LoopCommand
{
    private readonly int _repeatCount;

    public RepeatCommand(int repeatCount, List<Command> commands)
    {
        _repeatCount = repeatCount;
        Commands = commands;
    }

    public RepeatCommand(int repeatCount) : this(repeatCount, new List<Command>()) { }

    public override void Execute(Player player)
    {
        for (int i = 0; i < _repeatCount; i++)
            ExecuteCycle(player);
        return;

        void ExecuteCycle(Player player)
        {
            foreach (Command command in Commands)
                command.Execute(player);
        }
    }

    public void Add(Command cmd)
        => this.Commands.Add(cmd);

    public override string ToString() 
        => $"RepeatCommand {_repeatCount}";

    public override Command FromString(string[] words)
        => new RepeatCommand(int.Parse(words[1]));
}
