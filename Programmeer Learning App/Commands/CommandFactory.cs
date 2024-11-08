namespace Programmeer_Learning_App.Commands;

public static class CommandFactory
{
    private static readonly Dictionary<string, Func<string[], Command?>> commandMap = new Dictionary<string, Func<string[], Command?>>() {
        {"Move"       , (words) => new MoveCommand(0).FromString(words)},
        {"Turn"       , (words) => new TurnCommand(RelativeDir.Right).FromString(words)},
        {"Repeat"     , (words) => new RepeatCommand(0).FromString(words)},
        {"RepeatUntil", (words) => new RepeatUntilCommand(_ => false).FromString(words)}
    };

    /// <summary>
    /// Creates an instance from an array of strings.
    /// </summary>
    /// <param name="words">Array of strings, in certain order</param>
    /// <returns>An instance of Command, created from the argument. Or null if the argument does not match expected format.</returns>
    public static Command? CreateInstance(string[] words)
        => commandMap.ContainsKey(words[0]) ? commandMap[words[0]].Invoke(words) : null;
}