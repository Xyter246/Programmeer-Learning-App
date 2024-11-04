using Programmeer_Learning_App.Exercises;

namespace Programmeer_Learning_App_Unit_Tests.Exercises;

public class PathFindingExerciseUnitTest
{
    [Fact]
    public void FirstPathFindExample()
    {
        // Arrange
        string[] game = new string[] {
            "so+++",
            "+oo++",
            "++o++",
            "++o++",
            "++oox"
        };

        // Act
        PathFindingExercise pfe = PathFindingExercise.Generate(game);
        
        // Assert
        Assert.Equal(new Point(0, 0), pfe.PlayerPos);
        Assert.Equal(new Point(4, 4), pfe.EndPoint);
        Assert.True(NullChecker(pfe, new Point[] {
                                 new Point(1, 0), 
                                 new Point(1, 1), new Point(2, 1),
                                                  new Point(2, 2),
                                                  new Point(2, 3),
                                                  new Point(2, 4), new Point(3, 4)
        }));
        Assert.True(BlockadeChecker(pfe, new Point[] { 
                                                  new Point(2, 0), new Point(3, 0), new Point(4, 0),
                new Point(0, 1),                                   new Point(3, 1), new Point(4, 1),
                new Point(0, 2), new Point(1, 2),                  new Point(3, 2), new Point(4, 2),
                new Point(0, 3), new Point(1, 3),                  new Point(3, 3), new Point(4, 3),
                new Point(0, 4), new Point(1, 4),
        }));
    }

    private static bool NullChecker(PathFindingExercise pfe, Point[] points)
        => points.All(p => pfe.Grid[p.X, p.Y] is null);

    private static bool BlockadeChecker(PathFindingExercise pfe, Point[] points) 
        => points.All(p => pfe.Grid[p.X, p.Y]?.GetType() == typeof(Blockade));
}