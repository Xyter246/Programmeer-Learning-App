namespace Programmeer_Learning_App.Exercises;

public abstract class Exercise
{
    public readonly Entity?[,] Grid;
    public readonly Size GridSize;
    public Player Player;

    protected Exercise(Size size)
    {
        Grid = new Entity?[size.Width, size.Height];
        GridSize = size;
        Player = Player.Empty;
    }

    /// <summary>
    /// Determines if an Exercises is completed by an instance of a Player.
    /// </summary>
    /// <param name="player">A Player instance.</param>
    /// <returns>True if Player fulfills all requirements, otherwise False.</returns>
    public abstract bool IsCompleted(Player player);

    /// <summary>
    /// Determines what should happen if the Exercise has been completed.
    /// </summary>
    public abstract void OnSuccess();
}