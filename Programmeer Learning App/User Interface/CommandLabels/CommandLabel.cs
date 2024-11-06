namespace Programmeer_Learning_App.User_Interface.CommandLabels;

public abstract class CommandLabel : Label
{
    public new readonly string Name;

    public CommandLabel()
    {
        Name = ConvertLabel().Name;
    }

    public abstract Command ConvertLabel();
}