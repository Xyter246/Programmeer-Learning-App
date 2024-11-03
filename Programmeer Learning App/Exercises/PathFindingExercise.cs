namespace Programmeer_Learning_App.Exercises;

public class PathFindingExercise : Exercise
{
    public Point PlayerPos;
    public Point EndPoint;

    private PathFindingExercise(Size size) : base(size) { }

    public static PathFindingExercise Generate(string[] path)
    {
        PathFindingExercise pfe = new PathFindingExercise(new Size(path[0].Length, path.Length));
        Point? playerPos = null;
        Point? endPoint = null;

        for (int x = 0; x < path[0].Length; x++)
            for (int y = 0; y < path.Length; y++) {

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
                }
            }

        pfe.PlayerPos = playerPos ?? new Point(0, 0);
        pfe.EndPoint = endPoint ?? throw new ArgumentException(@"There must be an EndPoint for an Exercise.");
        return pfe;
    }

}