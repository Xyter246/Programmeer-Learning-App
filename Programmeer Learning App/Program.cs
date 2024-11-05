namespace Programmeer_Learning_App;

public class Program
{
    public LinkedList<Command> Commands;
    public bool HasEnded = false;
    private LinkedListNode<Command> _currentNode;

    public Program(LinkedList<Command> commands)
    {
        Commands = commands;
        _currentNode = Commands.First;
    }

    public Program() : this (new LinkedList<Command>()) { }

    /// <summary>
    /// Executes one command of the Program on the given player.
    /// </summary>
    /// <param name="player">The Player instance which gets updated.</param>
    public void StepOnce(Player player)
    {
        if (_currentNode.Next is null)
            if (Commands.First is null)
                return;
            else _currentNode = Commands.First;
        _currentNode = _currentNode.Next;
        _currentNode.Value.Execute(player);
    }

    /// <summary>
    /// Resets the Program back to the start.
    /// Doesn't take into account any changes to Player instances.
    /// </summary>
    public void ResetProgram()
    {
        _currentNode = Commands.First;
    }

    public void Add(Command command)
        => Commands.AddLast(command);
}