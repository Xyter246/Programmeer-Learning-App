using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programmeer_Learning_App.Commands;

namespace Programmeer_Learning_App;

public class Program
{
    public List<Command> Commands;
    private int _currentIndex;
    public bool HasEnded => _currentIndex >= this.Commands.Count;

    public Program(List<Command> commands)
    {
        Commands = commands;
    }

    public Program() : this (new List<Command>()) { }

    /// <summary>
    /// Executes one command of the Program on the given player.
    /// </summary>
    /// <param name="player">The Player instance which gets updated.</param>
    public void StepOnce(Player player)
    {
        if (!HasEnded)
            Commands[_currentIndex++].Execute(player);
    }

    public void ResetProgram()
    {
        _currentIndex = 0;
    }

    public void Add(Command command)
        => Commands.Add(command);
}