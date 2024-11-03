namespace Programmeer_Learning_App;

public static class Functions
{
    /// <summary>
    /// Converts a CardinalDir to a Vector2 from the Assumption that North is Up.
    /// </summary>
    /// <param name="cdir">Any Cardinal Direction.</param>
    /// <returns>A Unit Vector.</returns>
    public static Vector2 CardinalDirToVector2(CardinalDir cdir)
        => cdir switch {
            CardinalDir.North  =>  Vector2.UnitY,
            CardinalDir.South  => -Vector2.UnitY,
            CardinalDir.East   =>  Vector2.UnitX,
            CardinalDir.West   => -Vector2.UnitX,
            _                  =>  Vector2.Zero,
        };
}