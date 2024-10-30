using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmeer_Learning_App.User_Interface
{
    public class TopBar : Label
    {
        private Button runButton;

        public TopBar(GameWindow gameWindow)
        {
            this.BackColor = Color.LightGray;
            this.Height = 30;
            this.Dock = DockStyle.Top;

            #region RunButton
            runButton = new Button();
            runButton.FlatStyle = FlatStyle.Flat;
            runButton.BackColor = Color.Gray;
            runButton.Text = "RUN"; 
            runButton.Size = new Size(60, 25); 
            runButton.Location = new Point(5, 2);
            runButton.Click += gameWindow.runButton_Click;
            this.Controls.Add(runButton);
            #endregion
        }
    }
}
