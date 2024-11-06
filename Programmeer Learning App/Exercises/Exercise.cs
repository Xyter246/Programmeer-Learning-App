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
}