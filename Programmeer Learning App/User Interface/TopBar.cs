namespace Programmeer_Learning_App.User_Interface
{
    public class TopBar : Label
    {
        private readonly Size _bufferSize = new Size(5, 25);
        private readonly Size _buttonSize = new Size(60, 25);
        private Button _runButton;
        private Button _exportButton;
        private Button _importButton;

        public TopBar(GameWindow gameWindow)
        {
            this.BackColor = Color.LightGray;
            this.Height = 30;
            this.Dock = DockStyle.Top;

            MakeRunButton();
            MakeExportButton();
            MakeImportButton();
            return;

            void MakeRunButton()
            {
                _runButton = new Button();
                _runButton.FlatStyle = FlatStyle.Flat;
                _runButton.BackColor = Color.Gray;
                _runButton.Text = @"RUN";
                _runButton.Size = _buttonSize;
                _runButton.Location = ButtonOffsetRight(new Button() { Location = new Point(0, 0), Size = new Size(0,0)});
                _runButton.Click += gameWindow.runButton_Click;
                this.Controls.Add(_runButton);
            }

            void MakeExportButton()
            {
                _exportButton = new Button();
                _exportButton.FlatStyle = FlatStyle.Flat;
                _exportButton.BackColor = Color.Gray;
                _exportButton.Text = @"EXPORT";
                _exportButton.Size = _buttonSize;
                _exportButton.Location = ButtonOffsetRight(_exportButton);
                _exportButton.Click += gameWindow.exportButton_Click;
                this.Controls.Add(_exportButton);
            }

            void MakeImportButton()
            {
                _importButton = new Button();
                _importButton.FlatStyle = FlatStyle.Flat;
                _importButton.BackColor = Color.Gray;
                _importButton.Text = @"IMPORT";
                _importButton.Size = _buttonSize;
                _importButton.Location = ButtonOffsetRight(_exportButton!);
                _importButton.Click += gameWindow.importButton_Click;
                this.Controls.Add(_importButton);
            }
        }

        private Point ButtonOffsetRight(Button btn) 
            => btn.Location with { X = btn.Location.X + btn.Size.Width + _bufferSize.Width };
    }
}