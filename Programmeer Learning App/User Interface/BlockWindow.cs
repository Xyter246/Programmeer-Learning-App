namespace Programmeer_Learning_App.User_Interface;

internal class BlockWindow : Panel
{
    private readonly Panel _blockPanel; // Inner panel for scrollable commands
    private readonly List<Command> _commandList; // List to track command labels
    private Point _labelLocation; // Tracks where to add the next label
    private bool isHovering; // Tracks if the mouse is inside the label or triangles

    public BlockWindow()
    {
        this.BackColor = Color.Pink;
        this.AutoScroll = true;

        // Initialize the inner panel for command labels
        _blockPanel = new Panel {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            Dock = DockStyle.Top, // Ensures it grows vertically
        };
        this.Controls.Add(_blockPanel); // Add the inner panel to BlockWindow

        // Initialize the list for command labels
        _commandList = new List<Command>();
        _labelLocation = new Point(10, 10); // Starting position for labels
    }

    // Method to add a new command label to the panel
    public void AddCommand(Command command)
    {
        _commandList.Add(command);
        UpdateScreen(_commandList);
    }

    // Method to remove a command label by command reference
    public void RemoveCommand(Command command)
    {
        if (_commandList.Remove(command)) {
            UpdateScreen(_commandList); // Refresh the display
        }
    }

    // Method to re-align labels after moving a command up or down
    private void UpdatePositions(Command command, bool moveUp)
    {
        int index = _commandList.IndexOf(command);
        if (index == -1) return; // Command not found in the list

        if (moveUp && index > 0) {
            // Swap command with the previous one to move it up
            (_commandList[index], _commandList[index - 1]) = (_commandList[index - 1], _commandList[index]);
        } else if (!moveUp && index < _commandList.Count - 1) {
            // Swap command with the next one to move it down
            (_commandList[index], _commandList[index + 1]) = (_commandList[index + 1], _commandList[index]);
        }

        // Update the panel display to reflect the new order
        UpdateScreen(_commandList);
    }

    private void UpdateScreen(List<Command> commandList)
    {
        _blockPanel.Controls.Clear();
        _labelLocation = new Point(10, 10);

        foreach (Command command in commandList) {
            Label commandLabel = new Label {
                Text = command.Name,
                AutoSize = true,
                Location = _labelLocation,
                BackColor = Color.LightGray,
                Padding = new Padding(5),
                BorderStyle = BorderStyle.FixedSingle,
                Tag = command // Store the command in the label’s Tag property
            };

            // Add click event handler to the label
            commandLabel.Click += CommandLabel_Click;
            commandLabel.MouseEnter += OnHover;

            _blockPanel.Controls.Add(commandLabel);
            _labelLocation.Y += commandLabel.Height + 10;
        }
    }

    private void CommandLabel_Click(object? o, EventArgs ea)
    {
        throw new NotImplementedException();
    }

    private void OnHover(object? o, EventArgs ea)
    {
        if (o is not Label label || label.Tag is not Command command) return;

        // Create the panels for the up and down triangles
        Panel upArrow = CreateTriangleBox(true, command);
        Panel downArrow = CreateTriangleBox(false, command);

        // Move the command up in the list
        upArrow.Click += (sender, e) => {
            if (sender is Panel panel && panel.Tag is Command cmd)
                UpdatePositions(cmd, true);
        };

        // Move the command down in the list
        downArrow.Click += (sender, e) => {
            if (sender is Panel panel && panel.Tag is Command cmd)
                UpdatePositions(cmd, false);
        };

        // Add the boxes to the label's parent container so they overlay correctly
        label.Controls.Add(upArrow);
        label.Controls.Add(downArrow);

        // Position the boxes on the label
        PositionBoxes(label, upArrow, downArrow);

        ShowTriangles(o, ea);

        void ShowTriangles(object? sender, EventArgs e)
        {
            isHovering = true;
            upArrow.Visible = true;
            downArrow.Visible = true;
        }

        void HideTriangles(object? sender, EventArgs e)
        {
            isHovering = false;
            Task.Delay(1).ContinueWith(_ => {
                if (!isHovering) {
                    upArrow.Visible = false;
                    downArrow.Visible = false;
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        // Attach events to both the label and the triangles
        label.MouseLeave += HideTriangles;
        upArrow.MouseEnter += ShowTriangles;
        upArrow.MouseLeave += HideTriangles;
        downArrow.MouseEnter += ShowTriangles;
        downArrow.MouseLeave += HideTriangles;
    }

    private Panel CreateTriangleBox(bool isUp, Command command)
    {
        var box = new Panel {
            Size = new Size(20, 10), // Small size for the triangle box
            BackColor = Color.Transparent, // Set transparent so only the triangle shows
            Visible = false, // Initially hidden
            Tag = command // access to the command of the parent label
        };

        box.Paint += (sender, e) => {
            Point[] trianglePoints;
            if (isUp) {
                trianglePoints = new[]
                {
                    new Point(box.Width / 2, 0),
                    new Point(0, box.Height),
                    new Point(box.Width, box.Height)
                };
            } else {
                trianglePoints = new[]
                {
                    new Point(0, 0),
                    new Point(box.Width, 0),
                    new Point(box.Width / 2, box.Height)
                };
            }

            e.Graphics.FillPolygon(Brushes.Black, trianglePoints);
        };

        return box;
    }

    private void PositionBoxes(Label label, Panel upBox, Panel downBox)
    {
        int centerX = (label.Width - upBox.Width) / 2;
        int topY = label.Height / 4 - upBox.Height / 2;
        int bottomY = (3 * label.Height) / 4 - downBox.Height / 2;

        upBox.Location = new Point(centerX, topY);
        downBox.Location = new Point(centerX, bottomY);
    }

    public void OnResize(object? o, EventArgs? ea, int cmdWindowWidth)
    {
        if (o is not GameWindow gamewindow) return;

        this.Size = new Size(gamewindow.Size.Width / 4, gamewindow.UsableHeight);
        this.Location = new Point(cmdWindowWidth, gamewindow.UsableStartLocation);
    }

    public Program Program() => new Program(_commandList);
}
