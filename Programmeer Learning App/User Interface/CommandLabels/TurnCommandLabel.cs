namespace Programmeer_Learning_App.User_Interface.CommandLabels;

public class TurnCommandLabel : CommandLabel
{
    private readonly ComboBox _cbb = new ComboBox()
    {
        DataSource = new RelativeDir[] {RelativeDir.Right , RelativeDir.Left},
        SelectedIndex = 0,
    };

    public override Command ConvertLabel()
        => new TurnCommand((RelativeDir)_cbb.SelectedValue!);
}