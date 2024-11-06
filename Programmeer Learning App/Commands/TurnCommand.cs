namespace Programmeer_Learning_App.Commands;

public class TurnCommand : Command
{
    public RelativeDir TurnDir;

    public TurnCommand(RelativeDir turnDir)
    {
        TurnDir = turnDir;
    }

    public override void Execute(Player player)
        => player.FacingDir = (CardinalDir) (((int)player.FacingDir + (int)TurnDir + 4) % Enum.GetNames(typeof(CardinalDir)).Length);

    public override string ToString()
        => $"Turn {TurnDir}";

    public override Command? FromString(string[] words)
        => Enum.TryParse(words[1], out RelativeDir relDir) ? new TurnCommand(relDir) : null;

    public override CommandLabel ToLabel()
    // Constructor of TurnCommandLabel doesn't convert input properly
        => new TurnCommandLabel(TurnDir);
}