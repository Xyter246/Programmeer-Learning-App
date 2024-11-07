namespace Programmeer_Learning_App.User_Interface.CommandLabels;

public class MoveCommandLabel : CommandLabel
{
    private readonly NumericUpDown _nup = new NumericUpDown() {
        Increment = 1,
        Minimum = 1,
        Maximum = int.MaxValue,
        Value = 1
    };

    public MoveCommandLabel() : base()
    {
        this.Controls.Add(_nup);
    }

    public MoveCommandLabel(int value) : this()
    {
        _nup.Value = value;
    }

    public override Command ConvertLabel()
        => new MoveCommand((int)_nup.Value);

    public override string ToString()
        => "Move";

    public override void OnResize(object? o, EventArgs? ea)
    {
        base.OnResize(o, ea);
        _nup.Size = new Size(this.Width / 3, this.Height);
        _nup.Location = new Point(this.Width - _nup.Width, this.Height - _nup.Height);
    }
}