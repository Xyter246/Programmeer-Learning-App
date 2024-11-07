using Programmeer_Learning_App.User_Interface.CommandLabels;

namespace Programmeer_Learning_App.User_Interface;

// This classes layout had been hand made, methods return types, parameters etc. 
// After that the layout has been put through ChatGPT to use it to fill in the code inside of methods
// After that we changed the code again to fit our vision of the class

public class CommandWindow : Panel
{
    private readonly Panel _buttonPanel;
    private readonly BlockWindow _blockWindow;

    public CommandWindow(BlockWindow blockWindow)
    {
        _blockWindow = blockWindow;

        // Setup CommandWindow properties
        this.AutoScroll = true; // Enable scrolling on CommandWindow
        this.BackColor = Color.FromArgb(0x2b, 0x2d, 0x31);

        // Create a scrollable inner panel
        _buttonPanel = new Panel();
        _buttonPanel.AutoSize = true;
        _buttonPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _buttonPanel.Location = new Point(0, 0);
        _buttonPanel.Dock = DockStyle.Top; // Dock to top so it grows vertically
        this.Controls.Add(_buttonPanel);

        // Initialize buttons and add them to _buttonPanel
        Point buttonLocation = new Point(10, 10);
        InitializeButton(new MoveCommandLabel());   // MoveButton
        InitializeButton(new RepeatCommandLabel()); // RepeatButton
        InitializeButton(new TurnCommandLabel());   // TurnButton
        return;

        // Method to initialize and position buttons
        Button InitializeButton(CommandLabel cmd)
        {
            Button button = new Button();
            button.BackColor = Color.FromArgb(0x60, 0xcc, 0x35);
            button.Text = cmd.ToString();
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Location = buttonLocation;
            button.Click += ButtonClicked;
            button.Tag = cmd;

            // Update button location for next button
            buttonLocation = new Point(buttonLocation.X, buttonLocation.Y + button.Height + 10);
            _buttonPanel.Controls.Add(button); // Add button to _buttonPanel

            return button;
        }
    }

    // Resize handler to keep button panel within bounds
    public void OnResize(object? o, EventArgs? ea)
    {
        if (o is GameWindow gamewindow)
            this.Size = new Size(gamewindow.Size.Width / 4, gamewindow.UsableHeight);
    }

    private void ButtonClicked(object? o, EventArgs ea)
    {
        if (o is not Button button) return;

        switch(button.Tag!) {
            case MoveCommandLabel:
                _blockWindow.AddCommand(new MoveCommandLabel());
                break;
            case RepeatCommandLabel:
                _blockWindow.AddCommand(new RepeatCommandLabel());
                break;
            case TurnCommandLabel:
                _blockWindow.AddCommand(new TurnCommandLabel());
                break;
            default: return;
        }
    }
}
