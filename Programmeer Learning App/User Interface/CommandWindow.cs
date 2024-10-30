using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmeer_Learning_App.User_Interface;

public class CommandWindow : Label
{
    public CommandWindow()
    {
        this.AutoSize = false;
        this.BackColor = Color.Blue;
    }

    public void OnResize(object? o, EventArgs? ea)
    {
        if (o is GameWindow gamewindow)
            this.Size = new Size(gamewindow.Size.Width / 4, gamewindow.UsableHeight);
    }
}