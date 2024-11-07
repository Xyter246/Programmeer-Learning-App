namespace Programmeer_Learning_App.User_Interface.CommandLabels;

public class RepeatUntilCommandLabel : LoopCommandLabel
{
    public new readonly string Name = "RepeatUntil";

    private readonly ComboBox _cbb = new ComboBox() {
        DataSource = new string[] {"Facing a Wall", "At the Edge of the Grid"}
    };

    public override Command ConvertLabel()
    {
        throw new NotImplementedException();
    }

    public override string ToString()
        => "RepeatUntil";
}