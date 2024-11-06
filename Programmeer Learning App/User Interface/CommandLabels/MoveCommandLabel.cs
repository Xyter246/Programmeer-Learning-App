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
        this.Text = Name;
    }

    public override Command ConvertLabel()
        => new MoveCommand((int)_nup.Value);
}