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
        private readonly Size _bufferSize = new Size(5, 25);
        private Button _runButton;
        private Button _exportButton;

        public TopBar(GameWindow gameWindow)
        {
            this.BackColor = Color.LightGray;
            this.Height = 30;
            this.Dock = DockStyle.Top;

            MakeRunButton();
            MakeExportButton();
            return;

            void MakeRunButton()
            {
                _runButton = new Button();
                _runButton.FlatStyle = FlatStyle.Flat;
                _runButton.BackColor = Color.Gray;
                _runButton.Text = @"RUN";
                _runButton.Size = new Size(60, 25);
                _runButton.Location = new Point(5, 2);
                _runButton.Click += gameWindow.runButton_Click;
                this.Controls.Add(_runButton);
            }

            void MakeExportButton()
            {
                _exportButton = new Button();
                _exportButton.FlatStyle = FlatStyle.Flat;
                _exportButton.BackColor = Color.Gray;
                _exportButton.Text = @"EXPORT";
                _exportButton.Size = new Size(60, 25);
                _exportButton.Location = _runButton!.Location with { X = _runButton.Location.X + _runButton.Size.Width + _bufferSize.Width };
                _exportButton.Click += gameWindow.exportButton_Click;
                this.Controls.Add(_exportButton);
            }
        }
    }
}