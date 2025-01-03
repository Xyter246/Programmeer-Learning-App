﻿namespace Programmeer_Learning_App.Commands;

public class TurnCommand : Command
{
    public RelativeDir TurnDir;
    private const int _overflowProtection = 4;

    public TurnCommand(RelativeDir turnDir)
    {
        TurnDir = turnDir;
    }

    public override void Execute(Player player)
        => player.FacingDir = (CardinalDir) (((int)player.FacingDir + (int)TurnDir + _overflowProtection) % Enum.GetNames(typeof(CardinalDir)).Length);

    public override string ToString()
        => $"Turn {TurnDir}";

    public override Command? FromString(string[] words)
        => Enum.TryParse(words[1], out RelativeDir relDir) ? new TurnCommand(relDir) : null;

    public override CommandLabel ToLabel()
        => new TurnCommandLabel(TurnDir);
}