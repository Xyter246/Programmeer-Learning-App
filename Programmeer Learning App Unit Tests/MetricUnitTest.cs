using Programmeer_Learning_App.Commands;
using Programmeer_Learning_App.Enums;
using Programmeer_Learning_App;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmeer_Learning_App_Unit_Tests;

public class MetricUnitTest
{
    [Fact]
    public void NumOfCommands()
    {
        // Arrange
        Program program = new Program(Player.EmptyPlayer);
        program.Add(new TurnCommand(RelativeDir.Right));
        program.Add(new MoveCommand(1));
        program.Add(new RepeatCommand(4, new List<Command>() {
            new TurnCommand(RelativeDir.Right),
            new MoveCommand(3)
        }));

        // Act
        int commandCount = Metric.NumOfCommands(program);

        // Assert
        Assert.Equal(5, commandCount);
    }

    [Fact]
    public void MaxNestingDepth()
    {
        // Arrange
        Program program = new Program(Player.EmptyPlayer);
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
        int commandCount = Metric.MaxNestingDepth(program);

        // Assert
        Assert.Equal(2, commandCount);
    }

    [Fact]
    public void NumOfRepeatCommands()
    {
        // Arrange
        Program program = new Program(Player.EmptyPlayer);
        program.Add(
        new RepeatCommand(1, new List<Command>() { 
            new RepeatCommand(1, new List<Command>() {
                new RepeatCommand(1, new List<Command>())
            }),
            new RepeatCommand(1, new List<Command>(){
                new RepeatCommand(1, new List<Command>() { 
                    new RepeatCommand(1, new List <Command>()) 
                })
            })
        }));

        // Act
        int commandCount = Metric.NumOfRepeatCommands(program);

        // Assert
        Assert.Equal(6, commandCount);
    }
}