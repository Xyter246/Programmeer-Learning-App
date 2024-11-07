namespace Programmeer_Learning_App.User_Interface;

public class TopBar : Label
{
    private readonly Size _bufferSize = new Size(5, 25);
    private readonly Size _buttonSize = new Size(60, 25);
    private int _buttonCount = 0;

    public TopBar(GameWindow gameWindow)
    {
        this.BackColor = Color.FromArgb(0x1e, 0x1f, 0x22);
        this.Height = 30;
        this.Dock = DockStyle.Top;

        MakeButton(@"RUN", gameWindow.runButton_Click);
        MakeButton(@"RESET", gameWindow.resetButton_Click);
        MakeButton(@"EXPORT", gameWindow.exportButton_Click);
        MakeButton(@"IMPORT", gameWindow.importButton_Click);
        MakeButton(@"LEARN", gameWindow.exerciseButton_Click);
    }

    /// <summary>
    /// Create a preset Button.
    /// </summary>
    /// <param name="buttonName">Custom Button Name</param>
    /// <param name="eh">EventHandler, in case the Button gets pressed.</param>
    /// <returns>The newly created Button.</returns>
    private Button MakeButton(string buttonName, EventHandler eh)
    {
        Button button = new Button();
        button.FlatStyle = FlatStyle.Flat;
        button.BackColor = Color.FromArgb(0x31, 0x82, 0x33);
        button.Text = buttonName;
        button.Size = _buttonSize;
        button.Location = ButtonOffsetRight();
        button.Click += eh;
        this.Controls.Add(button);
        return button;
    }

    /// <summary>
    /// Function to calculate new ButtonOffset.
    /// </summary>
    /// <returns>A Point unoccupied by other Buttons.</returns>
    private Point ButtonOffsetRight()
        => new Point(_buttonCount++ * (_bufferSize.Width + _buttonSize.Width) + _bufferSize.Width, (this.Height - _buttonSize.Height) / 2);
}