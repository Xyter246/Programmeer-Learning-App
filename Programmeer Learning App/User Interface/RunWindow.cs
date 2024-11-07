using Programmeer_Learning_App.Exercises;

namespace Programmeer_Learning_App.User_Interface;

public class RunWindow : Panel
{
    public bool RunHasFinished = true;
    private bool _forceStop = true;
    private const int _programStepDelay = 200; // in milliseconds
    private Exercise? _exercise;
    private Player _player;
    private readonly List<Point> _tracerPoints = new List<Point>();
    private readonly Size _baseSize = new Size(5, 5);
    private Size _gridSize;
    private Size _boxSize;
    private Brush _tracerBrush = new SolidBrush(Color.Brown);

    public RunWindow()
    {
        _exercise = null;
        _player = Player.Empty;

        ChangeSize();

        this.Paint += DrawWorld;
    }

    public void SetExercise(Exercise exercise)
    {
        _exercise = exercise;
        _player = (Player)_exercise.Player.Clone();
        ChangeSize();

        this.Paint += DrawExercise;
        Invalidate();
    }

    private void DrawExercise(object? o, PaintEventArgs pea)
    {
        DrawWorld(o, pea);
        DrawEntities(pea.Graphics);

        if (_exercise is PathFindingExercise pfe) 
            DrawEndPoint(pea.Graphics, pfe.EndPoint);
    }

    private void DrawWorld(object? o, PaintEventArgs pea)
    {
        Graphics gr = pea.Graphics;

        for (int x = 0; x < _gridSize.Width; x++)
            for (int y = 0; y > -_gridSize.Height; y--) {
                Brush boxColor = (x + y) % 2 == 0 ? new SolidBrush(Color.FromArgb(0x60, 0xcc, 0x35)) : new SolidBrush(Color.FromArgb(0x31, 0x82, 0x33));
                gr.FillRectangle(boxColor, x * _boxSize.Width, -y * _boxSize.Height, _boxSize.Width, _boxSize.Height);
                gr.DrawRectangle(Pens.Black, x * _boxSize.Width, -y * _boxSize.Height, _boxSize.Width - _boxSize.Width / 100, _boxSize.Height - _boxSize.Height / 100);
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
        Point playerOffset = new Point(_player.Pos.X * _boxSize.Width, -_player.Pos.Y * _boxSize.Height);
        
        gr.FillPolygon(new SolidBrush(Color.FromArgb(0x2b, 0x2d, 0x31)), playerPolygon.Select(p => p with {X = p.X + playerOffset.X, Y = p.Y + playerOffset.Y}).ToArray());
        return;

        Point[] CalcPlayerPolygon(CardinalDir cardDir)
        {
            return cardDir switch
            {
                CardinalDir.West => new Point[] {
                    new Point((_boxSize.Width / 3) * 2, bufferSize),                     // Top point
                    new Point(bufferSize, _boxSize.Height / 2),                          // Left point
                    new Point((_boxSize.Width / 3) * 2, _boxSize.Height - bufferSize),    // Bottom point
                },
                CardinalDir.North => new Point[] {
                    new Point(_boxSize.Width / 2, bufferSize),                           // Top point
                    new Point(_boxSize.Width - bufferSize, (_boxSize.Height) / 3 * 2),    // Right point
                    new Point(bufferSize, (_boxSize.Height) / 3 * 2),                    // Left point
                },
                CardinalDir.East => new Point[] {
                    new Point(_boxSize.Width / 3, bufferSize),                           // Top point
                    new Point(_boxSize.Width - bufferSize, _boxSize.Height / 2),          // Right point
                    new Point(_boxSize.Width / 3, _boxSize.Height - bufferSize),          // Bottom point
                },
                CardinalDir.South => new Point[] {
                    new Point(_boxSize.Width / 2, _boxSize.Height - bufferSize),          // Bottom point
                    new Point(_boxSize.Width - bufferSize, (_boxSize.Height) / 3),        // Right point
                    new Point(bufferSize, (_boxSize.Height) / 3),                        // Left point
                },
                _ => new Point[] {new Point(_boxSize.Width / 2, _boxSize.Height / 2)}
            };
        }
    }

    private void DrawEntities(Graphics gr)
    {
        for (int x = 0; x < _gridSize.Width; x++)
            for (int y = 0; y > -_gridSize.Height; y--) {
                switch (_exercise!.Grid[x, -y]) {
                    case Blockade: DrawBlockade(gr, new Point(x, y));
                        break;
                    default: continue;
                }
            }
    }

    private void DrawBlockade(Graphics gr, Point location)
    {
        gr.FillRectangle(new SolidBrush(Color.FromArgb(0xda, 0x37, 0x3c)), location.X * _boxSize.Width, -location.Y * _boxSize.Height, _boxSize.Width, _boxSize.Height);
    }

    private void DrawEndPoint(Graphics gr, Point location)
    {
        gr.FillEllipse(new SolidBrush(Color.FromArgb(0x2b, 0x2d, 0x31)), location.X * _boxSize.Width, location.Y * _boxSize.Height, _boxSize.Width, _boxSize.Height);
    }

    public void OnResize(object? o, EventArgs? ea)
    {
        if (o is not GameWindow gamewindow) return;
        this.Size = new Size((gamewindow.Size.Width / 2) - 16, gamewindow.UsableHeight);
        this.Location = new Point(gamewindow.Width / 2, gamewindow.UsableStartLocation);
        ChangeSize();
        this.Invalidate();
    }

    private void ChangeSize()
    {
        _gridSize = _exercise?.GridSize ?? _baseSize;
        _boxSize = new Size(this.Width / _gridSize.Width, this.Height / _gridSize.Height);
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