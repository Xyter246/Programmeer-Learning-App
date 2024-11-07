namespace Programmeer_Learning_App.User_Interface.CommandLabels;

public abstract class CommandLabel : Label
{
    protected const int _smallLabelOffset = 10;

    protected CommandLabel()
    {
        this.Text = this.ToString();
        this.Size = new Size(80, 30);
        this.BackColor = Color.FromArgb(0x60, 0xcc, 0x35);
    }

    public abstract Command ConvertLabel();

    public abstract override string ToString();

    public virtual void OnResize(object? o, EventArgs? ea)
    {
        this.Size = this.Size with {Width = ((Control)o!).Width - this.Location.X - _smallLabelOffset};
    }
}