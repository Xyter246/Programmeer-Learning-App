using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programmeer_Learning_App.Commands;

namespace Programmeer_Learning_App;

public class Program
{
    public List<Command> Commands = new List<Command>();
    private Player _player;

    public Program(Player player, List<Command> commands)
    {
        Commands = commands;
        _player = player;
    }

    public Program(Player player) 
    {
        _player = player;
    }

    public Program() : this(null!) { }

    public void Execute()
    {
        if (_player is null)
            throw new ArgumentNullException();

        foreach (Command command in Commands)
            command.Execute(_player);
    }

    public void Add(Command command)
        => Commands.Add(command);

    public void Bind(Player player) 
        => _player = player;
}