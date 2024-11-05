namespace Programmeer_Learning_App.User_Interface.CommandLabels;

public class TurnCommandLabel : CommandLabel
{
    private readonly ComboBox cbb = new ComboBox()
    {
        DataSource = new RelativeDir[] {RelativeDir.Right , RelativeDir.Left},
    };

    public override Command ConvertLabel()
        => new TurnCommand((RelativeDir)cbb.SelectedValue!);
}