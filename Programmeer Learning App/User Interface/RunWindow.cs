using Programmeer_Learning_App.Exercises;

namespace Programmeer_Learning_App.User_Interface;

public class RunWindow : Panel
{
    public bool RunHasFinished = true;
    private bool _forceStop = true;
    private const int _programStepDelay = 200; // in milliseconds
    private readonly Exercise? _exercise;
    private Player _player;
    private readonly Size _baseSize = new Size(5, 5);

    public RunWindow(Exercise exercise)
    {
        _exercise = exercise;
        _player = (Player)_exercise.Player.Clone();

        this.Paint += _exercise switch {
            PathFindingExercise => DrawPathExercise,
            _ => DrawWorld
        };
    }

    public RunWindow()
    {
        _exercise = null;
        _player = Player.Empty;
        this.Paint += DrawWorld;
    }

    private void DrawPathExercise(object? o, PaintEventArgs pea)
    {
        PathFindingExercise pfe = (PathFindingExercise)_exercise!;
        DrawWorld(o, pea);
    }

    private void DrawWorld(object? o, PaintEventArgs pea)
    {
        Graphics gr = pea.Graphics;
        Size gridSize = _exercise?.GridSize ?? _baseSize;
        Size boxSize = new Size(this.Width / gridSize.Width, this.Height / gridSize.Height);

        for (int x = 0; x < gridSize.Width; x++)
            for (int y = 0; y > -gridSize.Height; y--) {
                Brush boxColor = (x + y) % 2 == 0 ? new SolidBrush(Color.FromArgb(0x60, 0xcc, 0x35)) : new SolidBrush(Color.FromArgb(0x31, 0x82, 0x33));
                gr.FillRectangle(boxColor, x * boxSize.Width, -y * boxSize.Height, boxSize.Width, boxSize.Height);
                gr.DrawRectangle(Pens.Black, x * boxSize.Width, -y * boxSize.Height, boxSize.Width - boxSize.Width / 100, boxSize.Height - boxSize.Height / 100);
            }

        DrawPlayer(gr, boxSize);
    }

    private void DrawPlayer(Graphics gr, Size boxSize)
    {
        const int bufferSize = 5;
        Point[] playerPolygon = CalcPlayerPolygon(_player.FacingDir);
        Point playerOffset = new Point(_player.Pos.X * boxSize.Width, -_player.Pos.Y * boxSize.Height);
        
        gr.FillPolygon(new SolidBrush(Color.FromArgb(0x2b, 0x2d, 0x31)), playerPolygon.Select(p => p with {X = p.X + playerOffset.X, Y = p.Y + playerOffset.Y}).ToArray());
        return;

        Point[] CalcPlayerPolygon(CardinalDir cardDir)
        {
            return cardDir switch
            {
                CardinalDir.West => new Point[] {
                    new Point((boxSize.Width / 3) * 2, bufferSize),                     // Top point
                    new Point(bufferSize, boxSize.Height / 2),                          // Left point
                    new Point((boxSize.Width / 3) * 2, boxSize.Height - bufferSize),    // Bottom point
                },
                CardinalDir.North => new Point[] {
                    new Point(boxSize.Width / 2, bufferSize),                           // Top point
                    new Point(boxSize.Width - bufferSize, (boxSize.Height) / 3 * 2),    // Right point
                    new Point(bufferSize, (boxSize.Height) / 3 * 2),                    // Left point
                },
                CardinalDir.East => new Point[] {
                    new Point(boxSize.Width / 3, bufferSize),                           // Top point
                    new Point(boxSize.Width - bufferSize, boxSize.Height / 2),          // Right point
                    new Point(boxSize.Width / 3, boxSize.Height - bufferSize),          // Bottom point
                },
                CardinalDir.South => new Point[] {
                    new Point(boxSize.Width / 2, boxSize.Height - bufferSize),          // Bottom point
                    new Point(boxSize.Width - bufferSize, (boxSize.Height) / 3),        // Right point
                    new Point(bufferSize, (boxSize.Height) / 3),                        // Left point
                },
                _ => new Point[] {new Point(boxSize.Width / 2, boxSize.Height / 2)}
            };
        }
    }

    public void OnResize(object? o, EventArgs? ea)
    {
        if (o is not GameWindow gamewindow) return;
        this.Size = new Size((gamewindow.Size.Width / 2) - 16, gamewindow.UsableHeight);
        this.Location = new Point(gamewindow.Width / 2, gamewindow.UsableStartLocation);
        this.Invalidate();
    }

    public async void Run(Program program)
    {
        ResetRun();
        _forceStop = false;
        while (!program.HasEnded) {
            await Task.Delay(_programStepDelay);
            if (_forceStop) {
                this.Invalidate();
                break;
            }
            program.StepOnce(_player);
            this.Invalidate();
        }
        RunHasFinished = true;
    }

    public void ResetRun()
    {
        _player = (Player)_exercise?.Player.Clone()! ?? (Player)Player.Empty.Clone();
        _forceStop = true;
        this.Invalidate();
    }
}