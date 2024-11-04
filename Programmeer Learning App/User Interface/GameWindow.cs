using Programmeer_Learning_App.Exporting;
using Programmeer_Learning_App.Importing;

namespace Programmeer_Learning_App.User_Interface;

public class GameWindow : Form
{
    [STAThread] // Required for Importing and Exporting.
    public static void Main(string[] args)
    {
        Console.WriteLine(@"Starting up...");
        Application.Run(new GameWindow());
    }

    private readonly CommandWindow _cmdWindow;
    private readonly BlockWindow _blockWindow;
    private readonly TopBar _topBar;
    private RunWindow? _runWindow;
    private bool _running = false;

    public int UsableHeight;
    public int UsableStartLocation;
    public double UIScalingFactor;

    public GameWindow()
    {
        #region Resizing the Window
        this.AutoSize = false;
        this.Size = new Size(600, 400);
        this.Resize += OnResize;
        #endregion

        #region TopBar
        _topBar = new TopBar(this);
        this.Controls.Add(_topBar);
        this.UsableHeight = ClientSize.Height - _topBar.Height;
        this.UsableStartLocation = _topBar.Height;
        #endregion

        #region CommandWindow
        _cmdWindow = new CommandWindow();
        _cmdWindow.Location = new Point(0, UsableStartLocation);
        this.Controls.Add(_cmdWindow);
        #endregion

        #region BlockWindow
        _blockWindow = new BlockWindow();
        this.Controls.Add(_blockWindow);
        #endregion

        this.OnResize(null, null);
    }

    private void OnResize(object? o, EventArgs? ea)
    {
        this.UsableHeight = ClientSize.Height - _topBar.Height;
        this.UIScalingFactor = this.Size.Width / (double)this.Size.Height;
        this._cmdWindow.OnResize(this, ea);
        this._blockWindow.OnResize(this, ea, _cmdWindow.Width);
        this._runWindow?.OnResize(this, ea);
    }

    public void runButton_Click(object? o, EventArgs ea)
    {

        if (!this._running) {
            _runWindow = new RunWindow(new Size(4, 4));
            this.Controls.Add(_runWindow);
        }

        else {
            this.Controls.Remove(_runWindow);
            _runWindow?.Dispose();
            _runWindow = null;
        }

        this.OnResize(null, null);

        this._running = !this._running;
    }

    public void exportButton_Click(object? o, EventArgs ea) 
        => TXTFileWriter.WriteFile(_blockWindow.Program());

    public void importButton_Click(object? o, EventArgs ea)
    // Returns an Program instance, but is unused as this function's return type is void.
        => TXTFileReader.Readfile(); 
}