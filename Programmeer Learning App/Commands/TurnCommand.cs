using Programmeer_Learning_App.Entities;

namespace Programmeer_Learning_App.Commands;

public class TurnCommand : Command
{
    public RelativeDir RelativeDir;

    public TurnCommand(RelativeDir relativeDir)
    {
        RelativeDir = relativeDir;
    }

    public override void Execute(Player player)
        => player.FacingDir = (CardinalDir) (((int)player.FacingDir + (int)RelativeDir) % Enum.GetNames(typeof(CardinalDir)).Length);

    public override string ToString()
        => $"Turn {RelativeDir}";

    public override Command? FromString(string[] words)
        => Enum.TryParse(words[1], out RelativeDir relDir) ? new TurnCommand(relDir) : null;
}