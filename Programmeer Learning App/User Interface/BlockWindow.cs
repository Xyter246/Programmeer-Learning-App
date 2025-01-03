﻿namespace Programmeer_Learning_App.User_Interface;

// This classes layout had been hand made, methods return types, parameters etc. 
// After that the layout has been put through ChatGPT to use it to fill in the code inside of methods
// After that we changed the code again to fit our vision of the class (and to fix it mostly ;( )
public class BlockWindow : Panel
{
    private readonly Panel _blockPanel; // Inner panel for scrollable commands
    private List<CommandLabel> _commandList; // List to track command labels
    private bool isHovering; // Tracks if the mouse is inside the label or triangles

    public BlockWindow()
    {
        this.BackColor = Color.FromArgb(0x31, 0x33, 0x38);
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
    }

    /// <summary>
    /// Method to add a new command label to the panel
    /// </summary>
    /// <param name="cmdLabel">Which CommandLabel to add.</param>
    public void AddCommand(CommandLabel cmdLabel)
    {
        cmdLabel.Location = new Point(10, 0);
        this.Resize += cmdLabel.OnResize;
        cmdLabel.OnResize(this, null);

        cmdLabel.MouseEnter += OnHover;
        cmdLabel.Click += RemoveCommand;

        _commandList.Add(cmdLabel);
        UpdateScreen();
    }

    public void RemoveCommand(object? o, EventArgs ea)
    {
        if (o is not CommandLabel cmdLabel) return;

        FindLabel(_commandList, cmdLabel);

        UpdateScreen();
        return;

        void FindLabel(List<CommandLabel> commandList, CommandLabel cmndLabel)
        {
            // Checks Depth-First-Search along all CommandLabels if it can find the one it seeks.
            for (int i = 0; i < commandList.Count; i++)
                if (commandList[i] == cmndLabel) {
                    commandList.RemoveAt(i);
                } else if (commandList[i] is LoopCommandLabel loopCmd) {
                    // Recursive check within nested loops
                    FindLabel(loopCmd.CommandLabels, cmndLabel);
                }
        }
    }

    /// <summary>
    /// Method to remove a command label by command reference
    /// </summary>
    public void ClearCommands()
    {
        _commandList.Clear();
        UpdateScreen();
    }

    /// <summary>
    /// Method to re-align labels after moving a command up or down
    /// </summary>
    /// <param name="cmdLabel">What CommandLabel to update.</param>
    /// <param name="moveUp">Which direction it must go.</param>
    private void UpdatePositions(CommandLabel cmdLabel, bool moveUp)
    {
        FindCommand(_commandList, cmdLabel, moveUp);
        UpdateScreen();
    }

    /// <summary>
    /// Finds a command recusively in a list and moves command accordingly.
    /// </summary>
    /// <param name="commandLabels"></param>
    /// <param name="cmdLabel"></param>
    /// <param name="moveUp"></param>
    /// <returns>True if command has been found and needs to be moved into a parent list.</returns>
    private bool FindCommand(List<CommandLabel> commandLabels, CommandLabel cmdLabel, bool moveUp)
    {
        for (int i = 0; i < commandLabels.Count; i++) {
            if (commandLabels[i] == cmdLabel) {
                return MoveCommand(commandLabels, cmdLabel, i, moveUp);
            } else if (commandLabels[i] is LoopCommandLabel loopCmd) {
                // Recursive check within nested loops
                if (!FindCommand(loopCmd.CommandLabels, cmdLabel, moveUp)) continue;
                // Move cmdLabel outside of the nested loop to the appropriate position
                if (moveUp) {
                    commandLabels.Insert(i, cmdLabel);
                } else {
                    commandLabels.Insert(i + 1, cmdLabel);
                }
                return false;
            }
        }
        return false; // If not found within the nested loop, return false
    }

    /// <summary>
    /// Moves the label up or down depending on parameters.
    /// </summary>
    /// <param name="commandLabels"></param>
    /// <param name="cmdLabel"></param>
    /// <param name="index"></param>
    /// <param name="moveUp"></param>
    /// <returns>True if command needs to be moved into a parent list.</returns>
    private bool MoveCommand(List<CommandLabel> commandLabels, CommandLabel cmdLabel, int index, bool moveUp)
    {
        if (moveUp) {
            if (index == 0) // Move cmdLabel out of this loop and set it to be placed above parent
            {
                if (commandLabels == _commandList) return false; // If there is no parent we dont do anything
                commandLabels.RemoveAt(index);
                return true;
            } else if (commandLabels[index - 1] is LoopCommandLabel nestedLoop) {
                // Move cmdLabel to the end of the nested loop's list
                commandLabels.RemoveAt(index);
                nestedLoop.CommandLabels.Add(cmdLabel);
                return false;
            } else {
                // Simple swap within the loop
                commandLabels.RemoveAt(index);
                commandLabels.Insert(index - 1, cmdLabel);
                return false;
            }
        } else { // moving down
            if (index == commandLabels.Count - 1) // Move cmdLabel out of this loop and set it to be placed below parent
            {
                if (commandLabels == _commandList) return false; // If there is no parent we dont do anything
                commandLabels.RemoveAt(index);
                return true;
            } else if (commandLabels[index + 1] is LoopCommandLabel nestedLoop) {
                // Move cmdLabel to the start of the nested loop's list
                commandLabels.RemoveAt(index);
                nestedLoop.CommandLabels.Insert(0, cmdLabel);
                return false;
            } else {
                // Simple swap within the loop
                commandLabels.RemoveAt(index);
                commandLabels.Insert(index + 1, cmdLabel);
                return false;
            }
        }
    }

    /// <summary>
    /// Updates the Screen to reflect the current situation.
    /// </summary>
    private void UpdateScreen()
    {
        // Clears all CommandLabels.
        _blockPanel.Controls.Clear();
        Point startingLabelLocation = new Point(10, 10);

        // Draws all new CommandLabels within this function.
        DrawCommandLabels(_commandList, ref startingLabelLocation);
        return;

        // Draw a List of CommandLabels recursively until all CommandLabels have been redrawn.
        void DrawCommandLabels(List<CommandLabel> CommandLabels, ref Point labelLocation)
        {
            foreach (CommandLabel cmd in CommandLabels) {
                cmd.Location = labelLocation;
                _blockPanel.Controls.Add(cmd);
                labelLocation.Y += cmd.Height + 10;

                if (cmd is not LoopCommandLabel loopCmd) continue;
                labelLocation.X += 10;
                DrawCommandLabels(loopCmd.CommandLabels, ref labelLocation);
                labelLocation.X -= 10;
            }
        }
    }

    /// <summary>
    /// Event Handler for CommandLabels for what to do if the Mouse hovers over it.
    /// </summary>
    /// <param name="o">Calling object.</param>
    /// <param name="ea">Empty EventArgs</param>
    private void OnHover(object? o, EventArgs? ea)
    {
        if (o is not CommandLabel cmdLabel) return;

        cmdLabel.BackColor = Color.FromArgb(0xda, 0x37, 0x3c);

        // Create the panels for the up and down triangles
        Panel upArrow = CreateTriangleBox(true);
        Panel downArrow = CreateTriangleBox(false);
        
        // Move the command up in the list
        upArrow.Click += (sender, e) => {
            if (sender is Panel panel)
                UpdatePositions(cmdLabel, true);
            upArrow.Visible = false; // the mouse will hit a new label within a millisecond, therefore the old arrows wont be hidden
            downArrow.Visible = false;
        };

        // Move the command down in the list
        downArrow.Click += (sender, e) => {
            if (sender is Panel panel)
                UpdatePositions(cmdLabel, false);
            upArrow.Visible = false; // the mouse will hit a new label within a millisecond, therefore the old arrows wont be hidden
            downArrow.Visible = false;
        };

        // Add the boxes to the label's parent container so they overlay correctly
        cmdLabel.Controls.Add(upArrow);
        cmdLabel.Controls.Add(downArrow);

        // Position the up and down boxes on the label
        PositionBoxes(cmdLabel, upArrow, downArrow);

        ShowControls(o, ea);

        // Attach events to both the label and the triangles/panels
        cmdLabel.MouseLeave += HideControls;
        upArrow.MouseEnter += ShowControls;
        upArrow.MouseLeave += HideControls;
        downArrow.MouseEnter += ShowControls;
        downArrow.MouseLeave += HideControls;
        return;

        void ShowControls(object? sender, EventArgs? e)
        {
            isHovering = true;
            upArrow.Visible = true;
            downArrow.Visible = true;
        }

        void HideControls(object? sender, EventArgs? e)
        {
            if (sender is CommandLabel cmndLabel)
                cmndLabel.BackColor = Color.FromArgb(0x60, 0xcc, 0x35);

            isHovering = false;
            Task.Delay(1).ContinueWith(_ => {
                if (isHovering) return;
                upArrow.Visible = false;
                downArrow.Visible = false;
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        Panel CreateTriangleBox(bool isUp)
        {
            Panel box = new Panel {
                Size = new Size(20, 10), // Small size for the triangle box
                BackColor = Color.Transparent, // Set transparent so only the triangle shows
                Visible = false, // Initially hidden
            };

            box.Paint += (_, e) => {
                Point[] trianglePoints;
                if (isUp) {
                    trianglePoints = new[] {
                        new Point(box.Width / 2, 0),
                        new Point(0, box.Height),
                        new Point(box.Width, box.Height)
                    };
                } else {
                    trianglePoints = new[] {
                        new Point(0, 0),
                        new Point(box.Width, 0),
                        new Point(box.Width / 2, box.Height)
                    };
                }
                e.Graphics.FillPolygon(Brushes.Black, trianglePoints);
            };
            return box;
        }

        void PositionBoxes(CommandLabel cmndLabel, Panel upBox, Panel downBox)
        {
            int centerX = (cmndLabel.Width - upBox.Width) / 2;
            int topY = cmndLabel.Height / 4 - upBox.Height / 2;
            int bottomY = (3 * cmndLabel.Height) / 4 - downBox.Height / 2;

            upBox.Location = new Point(centerX, topY);
            downBox.Location = new Point(centerX, bottomY);
        }
    }

    /// <summary>
    /// Resizes this instance upon a Window Resize.s
    /// </summary>
    /// <param name="o"></param>
    /// <param name="ea"></param>
    /// <param name="cmdWindowWidth"></param>
    public void OnResize(object? o, EventArgs? ea, int cmdWindowWidth)
    {
        if (o is not GameWindow gamewindow) return;

        this.Size = new Size(gamewindow.Size.Width / 4, gamewindow.UsableHeight);
        this.Location = new Point(cmdWindowWidth, gamewindow.UsableStartLocation);
    }

    /// <summary>
    /// Converts a List of CommandLabels to a useable Program instance.
    /// </summary>
    /// <returns>A Program instance.</returns>
    public Program Program() 
        => new Program(_commandList.Select(x => x.ConvertLabel()).ToList());

    /// <summary>
    /// Sets the current Commands to that of a List of CommandLabels, most likely converted from a IFileReader instance.
    /// </summary>
    /// <param name="labels">A list of CommandLabels.</param>
    public void SetProgram(List<CommandLabel>? labels)
    {
        if (labels == null) return;

        _commandList = labels;
        addList(labels);
        UpdateScreen();
        return;

        void addList(List<CommandLabel> cmdLabels)
        {
            foreach (CommandLabel cmdLabel in cmdLabels) {
                this.Resize += cmdLabel.OnResize;

                cmdLabel.OnResize(this, null);

                cmdLabel.MouseEnter += OnHover;
                cmdLabel.Click += RemoveCommand;

                if (cmdLabel is LoopCommandLabel loopLabel) {
                    addList(loopLabel.CommandLabels);
                }
            }
        }
    }
}