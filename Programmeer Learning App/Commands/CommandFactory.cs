namespace Programmeer_Learning_App.Commands;

public static class CommandFactory
{
    private static readonly Dictionary<string, Func<string[], Command?>> commandMap = new Dictionary<string, Func<string[], Command?>>() {
        {"Move"  , (words) => new MoveCommand(0).FromString(words)},
        {"Turn"  , (words) => new TurnCommand(RelativeDir.Right).FromString(words)},
        {"Repeat", (words) => new RepeatCommand(0).FromString(words)}
    };

    public static Command? CreateInstance(string[] words)
        => commandMap.ContainsKey(words[0]) ? commandMap[words[0]].Invoke(words) : null;
}