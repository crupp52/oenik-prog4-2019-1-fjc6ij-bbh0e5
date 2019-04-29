﻿namespace TronGame.Display
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;
    using GalaSoft.MvvmLight.CommandWpf;
    using TronGame.BusinessLogic;
    using TronGame.Model;

    /// <summary>
    /// Game controlling methods
    /// </summary>
    public class GameControl : FrameworkElement
    {
        //private IGameModel model;
        private IBusinessLogic logic;
        private GameDisplay display;
        private object _lock;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameControl"/> class.
        /// </summary>
        public GameControl()
        {
            this._lock = new object();
            this.Loaded += this.GameControl_Loaded;
        }

        public ICommand NewGameCommand { get; private set; }

        public IGameModel GameModel { get; set; }

        /// <summary>
        /// Render the generated Drawings.
        /// </summary>
        /// <param name="drawingContext">DrawingContext parameter</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            if (this.logic != null && this.ActualWidth != 0)
            {
                drawingContext.DrawDrawing(this.display.GetDrawings());
            }
        }

        private void GameControl_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            this.GameModel = new GameModel();
            this.logic = new GameLogic(this.GameModel);
            this.display = new GameDisplay(this.GameModel, 1000, 600);

            this.logic.ScreenRefresh += this.Logic_ScreenRefresh;
            this.logic.SaveGameState();

            this.NewGameCommand = new RelayCommand(() => { this.logic.NewGame(); MessageBox.Show("gecc"); });

            window.KeyDown += this.Window_KeyDown;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(33.3333333);
            timer.Tick += this.Timer_Tick;
            timer.Start();


        }

        private void Logic_ScreenRefresh(object sender, EventArgs e)
        {
            lock (this._lock)
            {
                this.InvalidateVisual();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this.Logic_ScreenRefresh(sender, e);
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Up: this.logic.MovePlayer(this.GameModel.Player1, Repository.MovingDirection.Up); break;
                case Key.Down: this.logic.MovePlayer(this.GameModel.Player1, Repository.MovingDirection.Down); break;
                case Key.Left: this.logic.MovePlayer(this.GameModel.Player1, Repository.MovingDirection.Left); break;
                case Key.Right: this.logic.MovePlayer(this.GameModel.Player1, Repository.MovingDirection.Rigth); break;

                case Key.W: this.logic.MovePlayer(this.GameModel.Player2, Repository.MovingDirection.Up); break;
                case Key.S: this.logic.MovePlayer(this.GameModel.Player2, Repository.MovingDirection.Down); break;
                case Key.A: this.logic.MovePlayer(this.GameModel.Player2, Repository.MovingDirection.Left); break;
                case Key.D: this.logic.MovePlayer(this.GameModel.Player2, Repository.MovingDirection.Rigth); break;

                case Key.Enter: this.logic.UseTurbo(this.GameModel.Player1); break;
                case Key.Space: this.logic.UseTurbo(this.GameModel.Player2); break;
            }
        }
    }
}
