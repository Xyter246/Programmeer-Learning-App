namespace Programmeer_Learning_App.Commands;
public abstract class LoopCommand : Command
{
   public List<Command> Commands = new List<Command>();

   /// <summary>
   /// Convert this Command to a String. Does not include sub-commands if it has any.
   /// </summary>
   /// <returns>A String starting with the name of the Command, followed by any properties.</returns>
    public abstract override string ToString();

    /// <summary>
    /// Adds an object to the end of the RepeatCommand.
    /// </summary>
    /// <param name="command">Command which is to be added.</param>
    public virtual void Add(Command command)
        => this.Commands.Add(command);
}