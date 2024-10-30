using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmeer_Learning_App.User_Interface;
internal class BlockWindow : Panel
{
    private readonly Label _label = new Label();

    public BlockWindow(GameWindow gameWindow)
    {
        this.AutoSize = false;
        this.BackColor = Color.Pink;
    }

    public void OnResize(object? o, EventArgs? ea, int cmdWindowWidth)
    {
        if (o is GameWindow gamewindow) {
            this.Size = new Size(gamewindow.Size.Width / 4, gamewindow.UsableHeight);
            this.Location = new Point(cmdWindowWidth, gamewindow.UsableStartLocation);
        }
    }
}
