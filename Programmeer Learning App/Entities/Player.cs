namespace Programmeer_Learning_App.Entities;

public class Player : Entity, ICloneable
{
    public static Player Empty = new Player(CardinalDir.East);

    public CardinalDir FacingDir;

    public Player(Point pos, CardinalDir facingDir)
    {
        Pos = pos;
        FacingDir = facingDir;
    }

    public Player(CardinalDir cardDir) : this(Point.Empty, cardDir) { }

    /// <summary>
    /// Creates a copy of the Player instance.
    /// </summary>
    /// <returns>A Memberwise Copy of this Player Instance.</returns>
    public object Clone()
        => (Player)MemberwiseClone();
}
