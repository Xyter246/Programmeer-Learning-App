using System;
using System.Drawing;
using Programmeer_Learning_App.Enums;

namespace Programmeer_Learning_App;

public class Player : ICloneable
{
    public static Player EmptyPlayer = new Player(CardinalDir.North);

    public Point Pos;
    public CardinalDir FacingDir;

    public Player(Point pos, CardinalDir facingDir)
    {
        Pos = pos;
        FacingDir = facingDir;
    }

    public Player(CardinalDir cardDir) : this(Point.Empty, cardDir) { }
    public object Clone()
    {
        return (Player)base.MemberwiseClone();
    }
}
