using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programmeer_Learning_App.Enums;

namespace Programmeer_Learning_App.Commands;
public static class CommandFactory
{
    private static readonly Dictionary<string, Func<string[], Command>> commandMap = new Dictionary<string, Func<string[], Command>>() {
        {"MoveCommand"  , (string[] words) => new MoveCommand(0).FromString(words)},
        {"TurnCommand"  , (string[] words) => new TurnCommand(RelativeDir.Right).FromString(words)},
        {"RepeatCommand", (string[] words) => new RepeatCommand(0).FromString(words)}
    };

    public static Command? CreateInstance(string[] words) 
        => commandMap.ContainsKey(words[0]) ? commandMap[words[0]].Invoke(words) : null;
}