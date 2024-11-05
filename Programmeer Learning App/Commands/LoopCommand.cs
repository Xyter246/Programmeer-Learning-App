namespace Programmeer_Learning_App.Commands;
public abstract class LoopCommand : Command
{
   public LinkedList<Command> Commands = new LinkedList<Command>();

   /// <summary>
   /// Convert this Command to a String. Does not include sub-commands if it has any.
   /// </summary>
   /// <returns>A String starting with the name of the Command, followed by any properties.</returns>
    public abstract override string ToString();
}