namespace Programmeer_Learning_App.User_Interface.CommandLabels;

public class TurnCommandLabel : CommandLabel
{
    private readonly ComboBox _cbb = new ComboBox()
    {
        DataSource = new RelativeDir[] {RelativeDir.Right , RelativeDir.Left},
        DropDownStyle = ComboBoxStyle.DropDownList
    };

    public TurnCommandLabel() : base()
    {
        this.Controls.Add(_cbb);
    }

    public TurnCommandLabel(RelativeDir relDir) : this()
    {
        _cbb.SelectedItem = relDir; // Doesn't function properly
    }

    public override Command ConvertLabel()
        => new TurnCommand((RelativeDir)_cbb.SelectedValue!);

    public override string ToString()
        => "Turn";

    public override void OnResize(object? o, EventArgs? ea)
    {
        base.OnResize(o, ea);
        _cbb.Size = new Size(this.Width / 3, this.Height);
        _cbb.Location = new Point(this.Width - _cbb.Width, this.Height - _cbb.Height);
    }
}