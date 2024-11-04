using Programmeer_Learning_App.Entities;

namespace Programmeer_Learning_App.Commands;

public class MoveCommand : Command
{
    public override string Name => "Move";

    public int MoveAmount;

    public MoveCommand(int moveAmount)
    {
        MoveAmount = moveAmount;
    }

    public override void Execute(Player player)
    {
        Vector2 vector = Functions.CardinalDirToVector2(player.FacingDir);
        Vector2 scaledVector = MoveAmount * vector;
        player.Pos = new Point(player.Pos.X + (int)scaledVector.X, player.Pos.Y + (int)scaledVector.Y);
    }

    public override string ToString()
        => $"MoveCommand {MoveAmount}";

    public override Command? FromString(string[] words)
    {
        try {
            int i = int.Parse(words[0]);
            return new MoveCommand(i);
        } catch { return null; }
    }
}
