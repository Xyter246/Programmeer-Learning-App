namespace Programmeer_Learning_App;

public class Program
{
    public List<Command> Commands;
    private List<Command> _concatCommands = new List<Command>();
    public bool HasEnded => _currentIndex >= this._concatCommands.Count;
    private int _currentIndex;

    public Program(List<Command> commands)
    {
        Commands = commands;
    }

    public Program() : this (new List<Command>()) { }

    /// <summary>
    /// Executes one command of the Program on the given player.
    /// </summary>
    /// <param name="player">The Player instance which gets updated.</param>
    public void StepOnce(Player player)
    {
        if (HasEnded) return;
        _concatCommands[_currentIndex++].Execute(player);
    }

    /// <summary>
    /// Resets the Program back to the start.
    /// Doesn't take into account any changes to Player instances.
    /// </summary>
    public void ResetProgram()
    {
        _currentIndex = 0;
        _concatCommands.Clear();
    }

    public void InitializeProgram()
    {
        ResetProgram();
        _concatCommands = convertList(Commands);
        return;

        List<Command> convertList(List<Command> commands)
        {
            List<Command> resultCommands = new List<Command>();
            foreach (Command command in commands) {
                if (command is RepeatCommand rptcmd) {
                    Command[] repeatCommandArray = convertList(rptcmd.Commands).ToArray();
                    for (int i = 0; i < rptcmd.RepeatCount; i++)
                        resultCommands.AddRange(repeatCommandArray);
                }
                else resultCommands.Add(command);
            }

            return resultCommands;
        }
    }

    public void Add(Command command)
        => Commands.Add(command);
}