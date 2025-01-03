﻿using Programmeer_Learning_App.Entities;

namespace Programmeer_Learning_App.Commands;

public abstract class Command
{
    /// <summary>
    /// The Name of the Command, in short hand
    /// </summary>
    public virtual string Name => ToString().Split(' ')[0];

    /// <summary>
    /// Executes the Command upon a given Player instance.
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

    /// <summary>
    /// Converts a Command to its Label variant.
    /// </summary>
    /// <returns>A CommandLabel of the same type as this Command.</returns>
    public abstract CommandLabel ToLabel();
}