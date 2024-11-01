﻿using System.Drawing;
using Programmeer_Learning_App;
using Programmeer_Learning_App.Commands;
using Programmeer_Learning_App.Enums;

namespace Programmeer_Learning_App_Unit_Tests.Commands;

public class MoveCommandUnitTest
{
    [Fact]
    public void MoveForward()
    {
        // Arrange
        MoveCommand moveCommand = new MoveCommand(10);
        Player player = new Player(CardinalDir.North);

        // Act
        moveCommand.Execute(player);

        // Assert
        Assert.Equal(new Point(0, 10), player.Pos);
    }
}