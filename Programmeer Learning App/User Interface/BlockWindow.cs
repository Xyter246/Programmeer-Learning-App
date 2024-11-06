﻿using Programmeer_Learning_App.Commands;
using System.Drawing.Text;
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace Programmeer_Learning_App.User_Interface;

// This classes layout had been hand made, methods return types, parameters etc. 
// After that the layout has been put through ChatGPT to use it to fill in the code inside of methods
// After that we changed the code again to fit our vision of the class
public class BlockWindow : Panel
{
    private readonly Panel _blockPanel; // Inner panel for scrollable commands
    private readonly List<CommandLabel> _commandList; // List to track command labels
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
    private void UpdatePositions(CommandLabel cmdLabel, bool moveUp)
    {
        // First, check if the CommandLabel is in the top-level list
        int index = _commandList.IndexOf(cmdLabel);
        if (index == -1) // If not found in the main list, search recursively in loop command lists
        {
            if (UpdatePositionsInLoop(_commandList, cmdLabel, moveUp, out bool needsParentMove) && needsParentMove) {
                // If cmdLabel is moved out of a loop list and needs to move outside the parent, position it in the main list
                int parentIndex = _commandList.FindIndex(cl => cl is LoopCommandLabel && ((LoopCommandLabel)cl).CommandLabels.Contains(cmdLabel));
                if (moveUp && parentIndex != -1) {
                    _commandList.Insert(parentIndex, cmdLabel); // Insert it above the parent loop
                } else if (!moveUp && parentIndex != -1) {
                    _commandList.Insert(parentIndex + 1, cmdLabel); // Insert it below the parent loop
                }
            }
            return;
        }

        if (moveUp) {
            // Standard up-move within the top-level list
            if (index > 0 && _commandList[index - 1] is LoopCommandLabel loopCmd) {
                // Move cmdLabel to the end of the previous LoopCommandLabel's list
                loopCmd.CommandLabels.Add(cmdLabel);
                _commandList.RemoveAt(index);
            } else if (index > 0) {
                // Simple swap within the top-level list
                _commandList.RemoveAt(index);
                _commandList.Insert(index - 1, cmdLabel);
            }
        } else // move down
          {
            // Standard down-move within the top-level list
            if (index < _commandList.Count - 1 && _commandList[index + 1] is LoopCommandLabel loopCmd) {
                // Move cmdLabel to the start of the next LoopCommandLabel's list
                loopCmd.CommandLabels.Insert(0, cmdLabel);
                _commandList.RemoveAt(index);
            } else if (index < _commandList.Count - 1) {
                // Simple swap within the top-level list
                _commandList.RemoveAt(index);
                _commandList.Insert(index + 1, cmdLabel);
            }
        }

        // Refresh display after reordering
        UpdateScreen(_commandList);
    }

    private bool UpdatePositionsInLoop(List<CommandLabel> commandLabels, CommandLabel cmdLabel, bool moveUp)
    {
        for (int i = 0; i < commandLabels.Count; i++) {
            if (commandLabels[i] == cmdLabel) {
                if (moveUp) {
                    if (i == 0) // Move cmdLabel out of this loop and set it to be placed above parent
                    {
                        return true;
                    } else if (commandLabels[i - 1] is LoopCommandLabel nestedLoop) {
                        // Move cmdLabel to the end of the nested loop's list
                        nestedLoop.CommandLabels.Add(cmdLabel);
                        commandLabels.RemoveAt(i);
                        return false;
                    } else {
                        // Simple swap within the loop
                        commandLabels.RemoveAt(i);
                        commandLabels.Insert(i - 1, cmdLabel);
                        return false;
                    }
                } else // moving down
                  {
                    if (i == commandLabels.Count - 1) // Move cmdLabel out of this loop and set it to be placed below parent
                    {
                        return true;
                    } else if (commandLabels[i + 1] is LoopCommandLabel nestedLoop) {
                        // Move cmdLabel to the start of the nested loop's list
                        nestedLoop.CommandLabels.Insert(0, cmdLabel);
                        commandLabels.RemoveAt(i);
                        return false;
                    } else {
                        // Simple swap within the loop
                        commandLabels.RemoveAt(i);
                        commandLabels.Insert(i + 1, cmdLabel);
                        return false;
                    }
                }
            } else if (commandLabels[i] is LoopCommandLabel loopCmd) {
                // Recursive check within nested loops
                if (UpdatePositionsInLoop(loopCmd.CommandLabels, cmdLabel, moveUp) {
                    // Move cmdLabel outside of the nested loop to the appropriate position
                    if (moveUp) {
                        commandLabels.Insert(i, cmdLabel);
                    } else {
                        commandLabels.Insert(i + 1, cmdLabel);
                    }
                    return false;
                }
            }
        }
        return false; // If not found within the nested loop, return false
    }



    private void UpdateScreen(List<CommandLabel> commandList)
    {
        _blockPanel.Controls.Clear();
        Point labelLocation = new Point(10, 10);

        DrawCommandLabels(commandList, ref labelLocation);

        void DrawCommandLabels(List<CommandLabel> CommandLabels, ref Point labelLocation)
        {
            foreach (CommandLabel cmd in CommandLabels) {

                cmd.Location = labelLocation;
                cmd.MouseEnter += OnHover;
                _blockPanel.Controls.Add(cmd);
                labelLocation.Y += cmd.Height + 10;

                if (cmd is LoopCommandLabel loopCmd) {
                    labelLocation.X += 10;
                    DrawCommandLabels(loopCmd.CommandLabels, ref labelLocation);
                    labelLocation.X -= 10;
                }
            }
        }
    }

    private void CommandLabel_Click(object? o, EventArgs ea)
    {
        throw new NotImplementedException();
    }

    private void OnHover(object? o, EventArgs ea)
    {
        if (o is not CommandLabel cmdLabel) return;

        // Create the panels for the up and down triangles
        Panel upArrow = CreateTriangleBox(true);
        Panel downArrow = CreateTriangleBox(false);


        // Move the command up in the list
        upArrow.Click += (sender, e) => {
            if (sender is Panel panel)
                UpdatePositions(cmdLabel, true);
        };

        // Move the command down in the list
        downArrow.Click += (sender, e) => {
            if (sender is Panel panel)
                UpdatePositions(cmdLabel, false);
        };

        // Add the boxes to the label's parent container so they overlay correctly
        cmdLabel.Controls.Add(upArrow);
        cmdLabel.Controls.Add(downArrow);

        // Position the up and down boxes on the label
        PositionBoxes(cmdLabel, upArrow, downArrow);

        ShowControls(o, ea);

        void ShowControls(object? sender, EventArgs e)
        {
            isHovering = true;
            upArrow.Visible = true;
            downArrow.Visible = true;
        }

        void HideControls(object? sender, EventArgs e)
        {
            isHovering = false;
            Task.Delay(1).ContinueWith(_ =>
            {
                if (!isHovering) {
                    upArrow.Visible = false;
                    downArrow.Visible = false;
                }
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        // Attach events to both the label and the triangles/panels
        cmdLabel.MouseLeave += HideControls;
        upArrow.MouseEnter += ShowControls;
        upArrow.MouseLeave += HideControls;
        downArrow.MouseEnter += ShowControls;
        downArrow.MouseLeave += HideControls;
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

    private void PositionBoxes(CommandLabel cmdLabel, Panel upBox, Panel downBox)
    {
        int centerX = (cmdLabel.Width - upBox.Width) / 2;
        int topY = cmdLabel.Height / 4 - upBox.Height / 2;
        int bottomY = (3 * cmdLabel.Height) / 4 - downBox.Height / 2;

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
