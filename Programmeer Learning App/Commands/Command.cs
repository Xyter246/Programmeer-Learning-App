using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmeer_Learning_App.Commands;

public abstract class Command
{
    /// <summary>
    /// Executes the corresponding command upon a Player instance
    /// </summary>
    /// <param name="player">The player which executes the command.</param>
    public abstract void Execute(Player player);

    public abstract override string ToString();
}