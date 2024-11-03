using Programmeer_Learning_App.Entities;

namespace Programmeer_Learning_App_Unit_Tests.Commands;

public class RepeatCommandUnitTest
{
    [Fact]
    public void RepeatsCommands()
    {
        // Arrange
        RepeatCommand rptcmd = new RepeatCommand(4);
        rptcmd.Add(new TurnCommand(RelativeDir.Right));
        rptcmd.Add(new MoveCommand(3));

        Player player = Player.Empty;

        // Act
        rptcmd.Execute(player);

        // Assert
        Assert.Equal(Point.Empty, player.Pos);
    }
}