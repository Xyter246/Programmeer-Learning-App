namespace Programmeer_Learning_App.User_Interface.CommandLabels;

public abstract class CommandLabel : Label
{
    public new string Name => ConvertLabel().Name;

    public CommandLabel()
    {
        this.Text = Name;
        this.Size = new Size(80, 30);
        this.BackColor = Color.White;
    }

    public abstract Command ConvertLabel();
}