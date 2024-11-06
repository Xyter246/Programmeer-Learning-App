namespace Programmeer_Learning_App.Commands;

public class RepeatUntilCommand : LoopCommand
{
    public Predicate<Player> Predicate;

    public RepeatUntilCommand(Predicate<Player> predicate)
    {
        Predicate = predicate;
    }

    public override void Execute(Player player)
    {
        while (Predicate.Invoke(player))
            foreach (Command command in Commands)
                command.Execute(player);
    }

    public override string ToString()
        => $"RepeatUntil {Predicate}";

    public override Command? FromString(string[] words)
    {
        //try {
        //    Predicate<Player> predicate = words[1];
        //    return new RepeatUntilCommand(predicate);
        //} catch {
        return null;
        //}
    }

    public override CommandLabel ToLabel()
    {
        throw new NotImplementedException();
    }
}
