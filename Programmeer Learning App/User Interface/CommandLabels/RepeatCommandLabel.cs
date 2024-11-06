namespace Programmeer_Learning_App.User_Interface.CommandLabels;

public class RepeatCommandLabel : LoopCommandLabel
{
    private readonly NumericUpDown _nup = new NumericUpDown() {
        Increment = 1,
        Minimum = 1,
        Maximum = int.MaxValue,
    };

    public override Command ConvertLabel() 
        => new RepeatCommand((int)_nup.Value, CommandLabels.Select(cmdl => cmdl.ConvertLabel()).ToList());
}