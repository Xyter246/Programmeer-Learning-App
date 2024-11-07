namespace Programmeer_Learning_App.Commands;

public class MoveCommand : Command
{
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
        => $"Move {MoveAmount}";

    public override Command? FromString(string[] words)
    {
        try {
            int i = int.Parse(words[1]);
            return new MoveCommand(i);
        } catch { return null; }
    }

    public override CommandLabel ToLabel()
        => new MoveCommandLabel(MoveAmount);
}