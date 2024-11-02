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

    /// <summary>
    /// Adds an object to the end of the RepeatCommand.
    /// </summary>
    /// <param name="cmd">Command which is to be added.</param>
    public void Add(Command cmd)
        => this.Commands.Add(cmd);

    public override string ToString() 
        => $"RepeatCommand {_repeatCount}";

    public override Command? FromString(string[] words)
    {
        try {
            int i = int.Parse(words[0]);
            return new RepeatCommand(i);
        } catch { return null; }
    }
}
