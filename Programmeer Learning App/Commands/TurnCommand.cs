using Programmeer_Learning_App.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmeer_Learning_App.Commands;

public class TurnCommand : Command
{
    private readonly RelativeDir _relativeDir;

    public TurnCommand(RelativeDir relativeDir)
    {
        _relativeDir = relativeDir;
    }

    public override void Execute(Player player)
        => player.FacingDir = (CardinalDir) (((int)player.FacingDir + (int)_relativeDir) % 4);
                                                            // 4 being the number of CardinalDirs

    public override string ToString()
        => $"TurnCommand {_relativeDir}";
}