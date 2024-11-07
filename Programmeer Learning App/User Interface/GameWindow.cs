using Programmeer_Learning_App.Exercises;
using Programmeer_Learning_App.Exporting;
using Programmeer_Learning_App.Importing;

namespace Programmeer_Learning_App.User_Interface;

public class GameWindow : Form
{
    [STAThread] // Required for Importing and Exporting functionality.
    public static void Main(string[] args)
    {
        Console.WriteLine(@"Starting up...");
        Application.Run(new GameWindow());
    }

    private readonly CommandWindow _cmdWindow;
    private readonly BlockWindow _blockWindow;
    private readonly TopBar _topBar;
    private RunWindow _runWindow;
    private bool _running => !this._runWindow.RunHasFinished;

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

        #region BlockWindow
        _blockWindow = new BlockWindow();
        this.Controls.Add(_blockWindow);
        #endregion

        #region CommandWindow
        _cmdWindow = new CommandWindow(_blockWindow);
        _cmdWindow.Location = new Point(0, UsableStartLocation);
        this.Controls.Add(_cmdWindow);
        #endregion

        #region RunWindow
        _runWindow = new RunWindow();
        _runWindow.Location = new Point(0, UsableStartLocation);
        this.Controls.Add(_runWindow);
        #endregion

        this.OnResize(null, null);
    }

    private void OnResize(object? o, EventArgs? ea)
    {
        this.UsableHeight = ClientSize.Height - _topBar.Height;
        this.UIScalingFactor = this.Size.Width / (double)this.Size.Height;
        this._cmdWindow.OnResize(this, ea);
        this._blockWindow.OnResize(this, ea, _cmdWindow.Width);
        this._runWindow.OnResize(this, ea);
    }

    public void runButton_Click(object? o, EventArgs ea)
    {
        if (this._running) return;
        _runWindow.Run(_blockWindow.Program());
    }

    public void resetButton_Click(object? o, EventArgs ea)
    {
        _runWindow.ResetRun();
        _blockWindow.ClearCommands();
    }

    public void exportButton_Click(object? o, EventArgs ea) 
        => TXTFileWriter.WriteFile(_blockWindow.Program());

    public void importButton_Click(object? o, EventArgs ea)
        // Returns an Program instance, but is unused as this function's return type is void.
        => _blockWindow.AddProgram(TXTFileReader.Readfile());

    public void exerciseButton_Click(object? o, EventArgs ea)
    {
        OpenFileDialog ofd = new OpenFileDialog();
        ofd.Filter = @"Text Documents (*.txt)|*.txt|All files (*.*)|*.*";
        if (ofd.ShowDialog() != DialogResult.OK || ofd.FileName == string.Empty)
            return;

        StreamReader sr = new StreamReader(ofd.FileName);
        List<string> lines = new List<string>();
        while (!sr.EndOfStream)
            lines.Add(sr.ReadLine()!);

        PathFindingExercise pfe =  PathFindingExercise.Generate(lines.ToArray());
        _runWindow.SetExercise(pfe);
        _blockWindow.ClearCommands();
    }
}