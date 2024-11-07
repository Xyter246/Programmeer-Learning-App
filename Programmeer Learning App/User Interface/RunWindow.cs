using Programmeer_Learning_App.Exercises;

namespace Programmeer_Learning_App.User_Interface;

public class RunWindow : Panel
{
    public bool RunHasFinished = true;
    private bool _forceStop = true;
    private const int _programStepDelay = 200; // in milliseconds
    private readonly Exercise? _exercise;
    private Player _player;
    private readonly List<Point> _tracerPoints = new List<Point>();
    private readonly Size _baseSize = new Size(5, 5);
    private Brush _tracerBrush = new SolidBrush(Color.Brown);

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

        DrawPath(gr, boxSize);
        DrawPlayer(gr, boxSize);
    }

    private void DrawPath(Graphics gr, Size boxSize)
    {
        int tracerwidth = 10;
        for (int i = 1; i < _tracerPoints.Count; i++) {
            Point p1 = _tracerPoints[i-1];
            Point p2 = _tracerPoints[i];
            p1.Y = -p1.Y;
            p2.Y = -p2.Y;

            // have p1 have the minimal values of the 2 points, and p2 the maximum values.
            if (p1.X > p2.X) 
                (p1.X, p2.X) = (p2.X, p1.X);
            if (p1.Y > p2.Y)
                (p1.Y, p2.Y) = (p2.Y, p1.Y);
            
            Point p1Tranformed = TransformPoint(p1);
            Point p2Tranformed = TransformPoint(p2);
            int width = p2Tranformed.X - p1Tranformed.X + tracerwidth/2;
            int height = p2Tranformed.Y - p1Tranformed.Y + tracerwidth/2;
            gr.FillRectangle(_tracerBrush, p1Tranformed.X - tracerwidth/2, p1Tranformed.Y - tracerwidth/2, width, height);
        }
        return;

        Point TransformPoint(Point p)
            => new Point(p.X * boxSize.Width + boxSize.Width / 2, p.Y * boxSize.Height + boxSize.Height / 2);
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
        while (!program.HasEnded && ! _forceStop) {
            await Task.Delay(_programStepDelay);
            if (!_forceStop) {
                program.StepOnce(_player);
                _tracerPoints.Add(_player.Pos);
            }
            this.Invalidate();
        }
        RunHasFinished = true;
    }

    public void ResetRun()
    {
        _player = (Player)_exercise?.Player.Clone() ?? (Player)Player.Empty.Clone();
        _tracerPoints.Clear();
        _tracerPoints.Add(_player.Pos);
        _forceStop = true;
        this.Invalidate();
    }
}