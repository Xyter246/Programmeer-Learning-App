namespace Programmeer_Learning_App.Commands;

public class TurnCommand : Command
{
    private readonly RelativeDir _relativeDir;

    public TurnCommand(RelativeDir relativeDir)
    {
        _relativeDir = relativeDir;
    }
    public override void Execute(Player player)
        => player.FacingDir = (CardinalDir) (((int)player.FacingDir + (int)_relativeDir) % Enum.GetNames(typeof(CardinalDir)).Length);

    public override string ToString()
        => $"TurnCommand {_relativeDir}";

    public override Command? FromString(string[] words)
        => Enum.TryParse(words[1], out RelativeDir relDir) ? new TurnCommand(relDir) : null;
}