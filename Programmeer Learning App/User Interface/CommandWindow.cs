using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmeer_Learning_App.User_Interface;

public class CommandWindow : Panel
{   

    // Class written by AI
    private readonly Panel _buttonPanel;
    private readonly Button _moveCmdButton;
    private readonly Button _repeatCmdButton;
    private readonly Button _turnCmdButton;

    public CommandWindow()
    {
        // Setup CommandWindow properties
        this.AutoScroll = true; // Enable scrolling on CommandWindow
        this.Anchor = AnchorStyles.Top | AnchorStyles.Bottom; // Anchor to make it resizable
        this.BackColor = Color.Blue;

        // Create a scrollable inner panel
        _buttonPanel = new Panel();
        _buttonPanel.AutoSize = true;
        _buttonPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
        _buttonPanel.Location = new Point(0, 0);
        _buttonPanel.Dock = DockStyle.Top; // Dock to top so it grows vertically
        this.Controls.Add(_buttonPanel);

        // Initialize buttons and add them to _buttonPanel
        Point buttonLocation = new Point(10, 10);
        _moveCmdButton = InitializeButton("Move");
        _repeatCmdButton = InitializeButton("Repeat");
        _turnCmdButton = InitializeButton("Turn");

        // Method to initialize and position buttons
        Button InitializeButton(string name)
        {
            Button button = new Button();
            button.BackColor = Color.AliceBlue;
            button.Text = name;
            button.FlatStyle = FlatStyle.Flat;
            button.Location = buttonLocation;
            button.Click += ButtonClicked;

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

        switch (button.Text) {
            case "Move":
                ///BlockWindow.AddCommand(new Commands.MoveCommand(1));
                break;
            case "Repeat":
                //BlockWindow.AddCommand(new Commands.RepeatCommand(1));
                break;
            case "Turn":
                //BlockWindow.AddCommand(new Commands.TurnCommand(Enums.RelativeDir.Left));
                break;
            default:
                return;
        }
    }
}
