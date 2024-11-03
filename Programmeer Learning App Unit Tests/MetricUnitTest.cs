namespace Programmeer_Learning_App_Unit_Tests;

public class MetricUnitTest
{
    [Fact]
    public void NumOfCommands()
    {
        // Arrange
        Program program = new Program();
        program.Add(new TurnCommand(RelativeDir.Right));
        program.Add(new MoveCommand(1));
        program.Add(new RepeatCommand(4, new List<Command>() {
            new TurnCommand(RelativeDir.Right),
            new MoveCommand(3)
        }));

        // Act
        int commandCount = program.NumOfCommands();

        // Assert
        Assert.Equal(5, commandCount);
    }

    [Fact]
    public void MaxNestingDepth()
    {
        // Arrange
        Program program = new Program();
        program.Add(
        new RepeatCommand(2, new List<Command>() {
            new TurnCommand(RelativeDir.Right),
            new MoveCommand(3),
            new RepeatCommand(2, new List<Command>() {
                new TurnCommand(RelativeDir.Left),
                new MoveCommand(3)
            }),
            new RepeatCommand(2, new List<Command>() {
                new TurnCommand(RelativeDir.Right),
                new MoveCommand(3)
            })
        }));
        program.Add(
        new RepeatCommand(2, new List<Command>() {
            new TurnCommand(RelativeDir.Right),
            new MoveCommand(3)
        }));

        // Act
        int commandCount = program.MaxNestingDepth();

        // Assert
        Assert.Equal(2, commandCount);
    }

    [Fact]
    public void NumOfRepeatCommands()
    {
        // Arrange
        Program program = new Program();
        program.Add(
        new RepeatCommand(1, new List<Command>() { 
            new RepeatCommand(1, new List<Command>() {
                new RepeatCommand(1, new List<Command>() {
                    new MoveCommand(1)
                })
            }),
            new RepeatCommand(1, new List<Command>(){
                new RepeatCommand(1, new List<Command>() { 
                    new RepeatCommand(1, new List <Command>() {
                        new TurnCommand(RelativeDir.Right)
                    }) 
                })
            })
        }));

        // Act
        int commandCount = program.NumOfRepeatCommands();

        // Assert
        Assert.Equal(6, commandCount);
    }

    [Fact]
    public void MaxGridSize()
    {
        // Arrange
        Player player = new Player(new Point(0, 0), CardinalDir.East);
        Program program = new Program(new List<Command>() {
            new MoveCommand(5),
            new TurnCommand(RelativeDir.Right),
            new MoveCommand(2),
            new TurnCommand(RelativeDir.Right),
            new MoveCommand(2),
            new TurnCommand(RelativeDir.Left),
            new MoveCommand(5),
            new TurnCommand(RelativeDir.Right),
            new MoveCommand(4)
        });

        // Act
        (Point, Size) results = program.MaxGridSize(player);

        // Assert
        Assert.Equal(new Point(-1, 0), results.Item1);
        Assert.Equal(new Size(6, 7), results.Item2);
    }
}