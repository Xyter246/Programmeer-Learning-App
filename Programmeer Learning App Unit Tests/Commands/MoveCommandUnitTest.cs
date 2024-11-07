using Programmeer_Learning_App.Entities;

namespace Programmeer_Learning_App_Unit_Tests.Commands;

public class MoveCommandUnitTest
{
    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(0)]
    [InlineData(-1)]
    public void MoveForward(int moveAmount)
    {
        // Arrange
        MoveCommand moveCommand = new MoveCommand(moveAmount);
        Player player = new Player(CardinalDir.North);

        // Act
        moveCommand.Execute(player);

        // Assert
        moveAmount = moveAmount > 0 ? moveAmount : 0;
        Assert.Equal(new Point(0, moveAmount), player.Pos);
    }
}