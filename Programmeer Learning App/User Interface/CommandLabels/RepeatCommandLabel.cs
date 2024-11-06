namespace Programmeer_Learning_App.User_Interface.CommandLabels;

public class RepeatCommandLabel : LoopCommandLabel
{
    private readonly NumericUpDown _nup = new NumericUpDown() {
        Increment = 1,
        Minimum = 1,
        Maximum = int.MaxValue,
        Value = 1
    };

    public RepeatCommandLabel() : base()
    {
        this.Controls.Add(_nup);
    }

    public override Command ConvertLabel() 
        => new RepeatCommand((int)_nup.Value, CommandLabels.Select(cmdl => cmdl.ConvertLabel()).ToList());

    public override string ToString()
        => "Repeat";

    public override void OnResize(object? o, EventArgs? ea)
    {
        base.OnResize(o, ea);
        _nup.Size = new Size(this.Width / 3, this.Height);
        _nup.Location = new Point(this.Width - _nup.Width, this.Height - _nup.Height);
    }
}