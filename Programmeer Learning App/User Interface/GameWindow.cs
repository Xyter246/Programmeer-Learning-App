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
    public double UIScalingFactor;

    public GameWindow()
    {
        #region Resizing the Window
        this.AutoSize = false;
        this.Size = new Size(600, 400);
        this.Resize += OnResize;
        #endregion

        #region CommandWindow
        _cmdWindow = new CommandWindow(this);
        _cmdWindow.Location = new Point(0, 0);
        this.Controls.Add(_cmdWindow);
        #endregion

        this.OnResize(null, null);
    }

    private void OnResize(object? o, EventArgs? ea)
    {
        this.UIScalingFactor = this.Size.Width / (double)this.Size.Height;
        this._cmdWindow.OnResize(this, ea);
    }
}