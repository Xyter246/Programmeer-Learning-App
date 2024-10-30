using System.Drawing;
using Programmeer_Learning_App;
using Programmeer_Learning_App.Commands;
using Programmeer_Learning_App.Enums;

namespace Programmeer_Learning_App_Unit_Tests.Commands;

public class RepeatCommandUnitTest
{
    [Fact]
    public void RepeatsCommands()
    {
        // Arrange
        RepeatCommand rptcmd = new(4);
        rptcmd.Add(new TurnCommand(RelativeDir.Right));
        rptcmd.Add(new MoveCommand(3));

        Player player = Player.EmptyPlayer;

        // Act
        rptcmd.Execute(player);

        // Assert
        Assert.Equal(Point.Empty, player.Pos);
    }
}