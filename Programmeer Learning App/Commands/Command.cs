using Programmeer_Learning_App.Entities;

namespace Programmeer_Learning_App.Commands;

public abstract class Command
{
    public abstract string Name { get; } // name of the command

    /// <summary>
    /// Executes the corresponding command upon a Player instance
    /// </summary>
    /// <param name="player">The player which executes the command.</param>
    public abstract void Execute(Player player);

    /// <summary>
    /// Convert this Command to a String.
    /// </summary>
    /// <returns>A String starting with the name of the Command, followed by any properties.</returns>
    public abstract override string ToString();

    /// <summary>
    /// Converts an array of strings to a Command instance.
    /// </summary>
    /// <param name="words">the array of strings which are to be converted.</param>
    /// <returns>A Command instance of the current type, or Null if the string is invalid.</returns>
    public abstract Command? FromString(string[] words);
}