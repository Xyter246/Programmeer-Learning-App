using Programmeer_Learning_App.Entities;

namespace Programmeer_Learning_App.Commands;

public class RepeatCommand : LoopCommand
{
    public int RepeatCount;
    
    public RepeatCommand(int repeatCount, List<Command> commands)
    {
        RepeatCount = repeatCount;
        Commands = commands;
    }

    public RepeatCommand(int repeatCount) : this(repeatCount, new List<Command>()) { }

    public override void Execute(Player player)
    {
        for (int i = 0; i < RepeatCount; i++)
            ExecuteCycle(player);
        return;

        void ExecuteCycle(Player player)
        {
            foreach (Command command in Commands)
                command.Execute(player);
        }
    }
    
    public override string ToString() 
        => $"Repeat {RepeatCount}";

    public override Command? FromString(string[] words)
    {
        try {
            int i = int.Parse(words[0]);
            return new RepeatCommand(i);
        } catch { return null; }
    }
}