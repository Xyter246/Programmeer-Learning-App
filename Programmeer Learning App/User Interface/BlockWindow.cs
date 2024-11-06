namespace Programmeer_Learning_App.User_Interface;

// This classes layout had been hand made, methods return types, parameters etc. 
// After that the layout has been put through ChatGPT to use it to fill in the code inside of methods
// After that we changed the code again to fit our vision of the class
public class BlockWindow : Panel
{
    private readonly Panel _blockPanel; // Inner panel for scrollable commands
    private readonly List<CommandLabel> _commandList; // List to track command labels
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
        _commandList = new List<CommandLabel>();
        _labelLocation = new Point(10, 10); // Starting position for labels
    }

    // Method to add a new command label to the panel
    public void AddCommand(CommandLabel cmdLabel)
    {
        _commandList.Add(cmdLabel);
        UpdateScreen(_commandList);
    }

    // Method to remove a command label by command reference
    public void RemoveCommand(CommandLabel cmdLabel)
    {
        if (_commandList.Remove(cmdLabel)) {
            UpdateScreen(_commandList); // Refresh the display
        }
    }

    // Method to re-align labels after moving a command up or down
    private void UpdatePositions(Command command, bool moveUp)
    {
        throw new NotImplementedException();
    }

    private void ChangeCount(Command command, int amount)
    {
        throw new NotImplementedException();
    }

    private void UpdateScreen(List<CommandLabel> commandList)
    {
        throw new NotImplementedException();
    }

    private void CommandLabel_Click(object? o, EventArgs ea)
    {
        throw new NotImplementedException();
    }

    private void OnHover(object? o, EventArgs ea)
    {
        throw new NotImplementedException();
    }

    private NumericUpDown CreateNumericBox()
    {
        throw new NotImplementedException();
    }

    private Panel CreateTriangleBox(bool isUp)
    {
        Panel box = new Panel {
            Size = new Size(20, 10), // Small size for the triangle box
            BackColor = Color.Transparent, // Set transparent so only the triangle shows
            Visible = false, // Initially hidden
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

    public Program Program() => new Program(_commandList.Select(x => x.ConvertLabel()).ToList());
}
