namespace Programmeer_Learning_App.Exercises;

public abstract class Exercise
{
    public readonly Entity?[,] Grid;

    protected Exercise(Size size)
    {
        Grid = new Entity?[size.Width, size.Height];
    }
}