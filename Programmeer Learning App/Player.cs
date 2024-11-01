using System;
using System.Drawing;
using Programmeer_Learning_App.Enums;

namespace Programmeer_Learning_App;

public class Player : ICloneable
{
    public static Player Empty = new Player(CardinalDir.East);

    public Point Pos;
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
        => (Player)base.MemberwiseClone();
}
