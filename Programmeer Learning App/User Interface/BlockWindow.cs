namespace Programmeer_Learning_App.User_Interface;

internal class BlockWindow : Panel
{
    private readonly Panel _blockPanel; // Inner panel for scrollable commands
    private readonly LinkedList<Command> _commandLabels; // Linked list to track command labels
    private Point _labelLocation; // Tracks where to add the next label

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

        // Initialize the linked list for command labels
        _commandLabels = new LinkedList<Command>();
        _labelLocation = new Point(10, 10); // Starting position for labels
    }

    // Method to add a new command label to the panel
    public void AddCommand(Command commandName)
    {
        _commandLabels.AddLast(commandName);
        UpdateScreen(_commandLabels);
    }

    // Optional: Method to remove a command label by command name
    public void RemoveCommand(string commandName)
    {
        throw new NotImplementedException();
        UpdateScreen(_commandLabels);
    }

    // Helper method to re-align labels after a removal
    private void UpdatePositions()
    {
        throw new NotImplementedException();
        UpdateScreen(_commandLabels);
    }

    private void UpdateScreen(LinkedList<Command> commandList)
    {
        // Clear existing labels from the panel
        _blockPanel.Controls.Clear();

        // Reset label location to start from the top
        Point labelLocation = new Point(10, 10);

        // Traverse the command list and create labels
        foreach (var command in commandList) {
            // Create a new label for each command
            var commandLabel = new Label {
                Text = command.Name, // Assuming Command has a Name property
                AutoSize = true,
                Location = labelLocation,
                BackColor = Color.LightGray,
                Padding = new Padding(5),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Add the label to the panel
            _blockPanel.Controls.Add(commandLabel);

            // Update the location for the next label
            labelLocation.Y += commandLabel.Height + 10;
        }
    }


    // Resize handler to adjust BlockWindow size and location based on GameWindow
    public void OnResize(object? o, EventArgs? ea, int cmdWindowWidth)
    {
        if (o is not GameWindow gamewindow) return;

        this.Size = new Size(gamewindow.Size.Width / 4, gamewindow.UsableHeight);
        this.Location = new Point(cmdWindowWidth, gamewindow.UsableStartLocation);
    }

    /// <summary>
    /// Converts the Command Labels of the BlockWindow to a useable Program instance.
    /// </summary>
    /// <returns>A new Program instance with the current Label Commands.</returns>
    public Program Program() 
        => new Program(_commandLabels.ToList());
}