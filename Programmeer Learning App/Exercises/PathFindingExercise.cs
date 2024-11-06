namespace Programmeer_Learning_App.Exercises;

public class PathFindingExercise : Exercise
{
    public Point EndPoint;

    private PathFindingExercise(Size size) : base(size) { }

    /// <summary>
    /// Generate an Instance of PathFindingExercise based on a string[].
    /// </summary>
    /// <param name="path">the string[] which contains a path the player must follow.</param>
    /// <returns>An instance of PathFindingExercise.</returns>
    /// <exception cref="ArgumentException">If there is no EndPoint given, then this is not a valid Exercise and will throw an Exception.</exception>
    public static PathFindingExercise Generate(string[] path)
    {
        PathFindingExercise pfe = new PathFindingExercise(new Size(path[0].Length, path.Length));
        Point? playerPos = null;
        Point? endPoint = null;

        // For every item in the string[]...
        for (int x = 0; x < path[0].Length; x++)
            for (int y = 0; y < path.Length; y++) {

                // ...determine what Entity it must be.
                switch (path[y][x]) {
                    case 's':
                        playerPos = new Point(x, y);
                        break;
                    case '+':
                        pfe.Grid[x, y] = new Blockade();
                        break;
                    case 'x':
                        endPoint = new Point(x, y);
                        break;
                    // 'o' doesn't need a case as it's already the default.
                }
            }

        // If there are multiple or no PlayerPos given, it'd be the last one iterated over or the first square (as default).
        pfe.Player = playerPos is not null ? new Player((Point)playerPos, CardinalDir.East) : Player.Empty;

        pfe.EndPoint = endPoint ?? throw new ArgumentException(@"There must be an EndPoint for an Exercise.");
        return pfe;
    }
}