namespace TronGame.Display
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;
    using TronGame.BusinessLogic;
    using TronGame.Model;

    public class GameScreen : FrameworkElement
    {
        private IGameModel model;
        private IBusinessLogic logic;

        public GameScreen()
        {
            this.model = new GameModel();
            this.Loaded += this.GameScreen_Loaded;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            ImageBrush obstacleTexture = new ImageBrush(new BitmapImage(new Uri(@"../../../TronGame.Repository/Images/obstacle.png", UriKind.Relative)));
            ImageBrush turboTexture = new ImageBrush(new BitmapImage(new Uri(@"../../../TronGame.Repository/Images/speedup.png", UriKind.Relative)));
            drawingContext.DrawRectangle(Brushes.Red, new Pen(Brushes.Black, 2), this.model.Player1.Area);
            drawingContext.DrawRectangle(Brushes.Blue, new Pen(Brushes.Black, 2), this.model.Player2.Area);

            foreach (var item in this.model.Obstacles)
            {
                drawingContext.DrawRectangle(obstacleTexture, null, item.Area);
            }

            foreach (var item in this.model.Turbos)
            {
                drawingContext.DrawRectangle(turboTexture, null, item.Area);
            }
        }

        private void GameScreen_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            if (window != null)
            {
                this.logic = new GameLogic(this.model);
                this.logic.ScreenRefresh += this.Logic_ScreenRefresh;
                window.KeyDown += this.Window_KeyDown;

                this.logic.SaveGameState();

                DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromMilliseconds(33.3333333);
                timer.Tick += this.Timer_Tick;
                timer.Start();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.Logic_ScreenRefresh(sender, e);
        }

        private void Logic_ScreenRefresh(object sender, EventArgs e)
        {
            this.InvalidateVisual();
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // throw new NotImplementedException();
            switch (e.Key)
            {
                case Key.W: this.logic.MovePlayer(this.model.Player1, Repository.MovingDirection.Up); break;
                case Key.A: this.logic.MovePlayer(this.model.Player1, Repository.MovingDirection.Left); break;
                case Key.S: this.logic.MovePlayer(this.model.Player1, Repository.MovingDirection.Down); break;
                case Key.D: this.logic.MovePlayer(this.model.Player1, Repository.MovingDirection.Rigth); break;

                case Key.Up: this.logic.MovePlayer(this.model.Player2, Repository.MovingDirection.Up); break;
                case Key.Left: this.logic.MovePlayer(this.model.Player2, Repository.MovingDirection.Left); break;
                case Key.Down: this.logic.MovePlayer(this.model.Player2, Repository.MovingDirection.Down); break;
                case Key.Right: this.logic.MovePlayer(this.model.Player2, Repository.MovingDirection.Rigth); break;

                default: break;
            }

        }
    }
}
