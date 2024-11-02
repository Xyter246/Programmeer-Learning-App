namespace Programmeer_Learning_App.Commands;
public abstract class LoopCommand : Command
{
   public List<Command> Commands = new List<Command>();
}