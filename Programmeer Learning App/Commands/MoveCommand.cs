namespace Programmeer_Learning_App.Commands;

public class MoveCommand : Command
{
    public int MoveAmount = 0;

    public MoveCommand(int moveAmount)
    {
        if (moveAmount > 0)
            MoveAmount = moveAmount;
    }

    public override void Execute(Player player)
    {
        Vector2 vector = CardinalDirToVector2(player.FacingDir);
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

    /// <summary>
    /// Converts a CardinalDir to a Vector2 from the Assumption that North is Up.
    /// </summary>
    /// <param name="cdir">Any Cardinal Direction.</param>
    /// <returns>A Unit Vector.</returns>
    private static Vector2 CardinalDirToVector2(CardinalDir cdir)
        => cdir switch {
            CardinalDir.North => Vector2.UnitY,
            CardinalDir.South => -Vector2.UnitY,
            CardinalDir.East => Vector2.UnitX,
            CardinalDir.West => -Vector2.UnitX,
            _ => Vector2.Zero,
        };
}