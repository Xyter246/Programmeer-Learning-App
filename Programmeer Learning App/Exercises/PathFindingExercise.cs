using Programmeer_Learning_App.User_Interface;

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
    public static PathFindingExercise? Generate(string[] path)
    {
        PathFindingExercise pfe = new PathFindingExercise(new Size(path[0].Length, path.Length));
        Point? playerPos = null;
        Point? endPoint = null;

        // For every item in the string[]...
        for (int y = 0; y < path.Length; y++)
            for (int x = 0; x < path[y].Length; x++) {
                
                // ...determine what Entity it must be.
                switch (path[y][x]) {
                    case 's':
                        playerPos = new Point(x, -y);
                        break;
                    case '+' or ' ':
                        pfe.Grid[x, y] = new Blockade();
                        break;
                    case 'x':
                        endPoint = new Point(x, -y);
                        break;
                    // 'o' doesn't need a case as it's already the default.
                }
            }

        // Error handling
        if (endPoint is null) {
            GameWindow.ShowError("There was no EndPoint found whilst Generating this PathFindingExercise.\n" +
                                 "Please add at least one 'x' to your PathFindingExercise text file.");
            return null;
        }

        // If there are multiple or no PlayerPos given, it'd be the last one iterated over or the first square (as default).
        pfe.Player = playerPos is not null ? new Player((Point)playerPos, CardinalDir.East) : Player.Empty;
        pfe.EndPoint = (Point)endPoint;
        return pfe;
    }

    public override bool IsCompleted(Player player)
        => player.Pos == this.EndPoint;

    public override void OnSuccess()
    {
        MessageBox.Show(@"Good job solving this exercise", @"Exercise");
    }
}