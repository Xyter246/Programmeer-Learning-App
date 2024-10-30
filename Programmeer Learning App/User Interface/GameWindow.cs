using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Programmeer_Learning_App.User_Interface;

public class GameWindow : Form
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Starting up...");
        Application.Run(new GameWindow());
    }

    private readonly CommandWindow _cmdWindow;
    private readonly BlockWindow _blockWindow;
    private readonly TopBar _topBar;
    private RunWindow? runWindow;
    private bool running = false;

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
        _cmdWindow = new CommandWindow(this);
        _cmdWindow.Location = new Point(0, UsableStartLocation);
        this.Controls.Add(_cmdWindow);
        #endregion

        #region BlockWindow
        _blockWindow = new BlockWindow(this);
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
        this.runWindow?.OnResize(this, ea);
    }

    public void runButton_Click(object? o, EventArgs ea)
    {
        runWindow = new RunWindow(new Size(4, 4));
        this.Controls.Add(runWindow);
        this.OnResize(null, null);
    }
}