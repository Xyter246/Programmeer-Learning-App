using Xunit.Sdk;

namespace Programmeer_Learning_App.User_Interface
{
    public class RunWindow : Panel
    {
        private Program _program;
        public RunWindow(Program program)
        {
            _program = program;

            this.Paint += DrawWorld;
        }

        private void DrawWorld(object? o, PaintEventArgs pea)
        {
            Brush boxColor;
            Graphics gr = pea.Graphics;
            Size gridSize = _program.MaxGridSize(Player.Empty).Item2;
            Size BoxSize = new Size(Width / gridSize.Width, Height / gridSize.Height);

            for (int x = 0; x < gridSize.Width; x++) {
                for (int y = 0; y < gridSize.Height; y++) {
                    if ((x + y) % 2 == 0) boxColor = Brushes.Green;
                    else boxColor = Brushes.GreenYellow;
                    gr.FillRectangle(boxColor, x * BoxSize.Width, y * BoxSize.Height, BoxSize.Width, BoxSize.Height);
                    gr.DrawRectangle(Pens.Black, x * BoxSize.Width, y * BoxSize.Height, BoxSize.Width - BoxSize.Width / 100, BoxSize.Height - BoxSize.Height / 100);

                }
            }
        }
        public void OnResize(object? o, EventArgs? ea)
        {
            if (o is GameWindow gamewindow) {
                this.Size = new Size((gamewindow.Size.Width / 2) - 16, gamewindow.UsableHeight);
                this.Location = new Point(gamewindow.Width / 2, gamewindow.UsableStartLocation);
                Invalidate();
            }
        }
    }
}
