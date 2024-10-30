using System.Drawing;
using Programmeer_Learning_App;
using Programmeer_Learning_App.Commands;
using Programmeer_Learning_App.Enums;

namespace Programmeer_Learning_App_Unit_Tests.Commands;

public class TurnCommandUnitTest
{
    [Fact]
    public void TurnLeft()
    {
        // Arrange
        Player p = new Player(new Point(0, 0), CardinalDir.North);
        TurnCommand tc = new TurnCommand(RelativeDir.Left);

        // Act
        tc.Execute(p);

        // Assert
        Assert.Equal(CardinalDir.West, p.FacingDir);
    }

    [Fact]
    public void TurnLeftOverflow()
    {
        // Arrange
        Player p = new Player(new Point(0, 0), CardinalDir.West);
        TurnCommand tc = new TurnCommand(RelativeDir.Left);

        // Act
        tc.Execute(p);

        // Assert
        Assert.Equal(CardinalDir.South, p.FacingDir);
    }

    [Fact]
    public void TurnRight()
    {
        // Arrange
        Player p = new Player(CardinalDir.East);
        TurnCommand tc = new TurnCommand(RelativeDir.Right);

        // Act
        tc.Execute(p);

        // Assert
        Assert.Equal(CardinalDir.South, p.FacingDir);
    }

    [Fact]
    public void TurnRightOverflow()
    {
        // Arrange
        Player p = new Player(new Point(0, 0), CardinalDir.South);
        TurnCommand tc = new TurnCommand(RelativeDir.Right);

        // Act
        tc.Execute(p);

        // Assert
        Assert.Equal(CardinalDir.West, p.FacingDir);
    }
}