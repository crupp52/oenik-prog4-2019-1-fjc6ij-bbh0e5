﻿namespace TronGame.Display
{
    using System;
    using System.Globalization;
    using System.Threading;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;
    using GalaSoft.MvvmLight.CommandWpf;
    using TronGame.BusinessLogic;
    using TronGame.Model;
    using TronGame.Repository;

    /// <summary>
    /// Game controlling methods
    /// </summary>
    public class GameControl : FrameworkElement
    {
        private IBusinessLogic logic;
        private GameDisplay display;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameControl"/> class.
        /// </summary>
        public GameControl()
        {
            this.Loaded += this.GameControl_Loaded;

            this.GameModel = new GameModel();
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
                if (!this.logic.IsGameEnded)
                {
                    if (this.logic.IsGamePaused)
                    {
                        FormattedText f = new FormattedText($"{this.GameModel.Player1.NumberOfWins} - {this.GameModel.Player2.NumberOfWins}", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 120, Brushes.White);

                        drawingContext.DrawRectangle(Brushes.Black, null, new Rect(0, 0, this.ActualWidth, this.ActualHeight));
                        drawingContext.DrawText(f, new Point((this.ActualWidth - f.Width) / 2, (this.ActualHeight - f.Height) / 2));
                    }
                    else
                    {
                        drawingContext.DrawDrawing(this.display.GetDrawings());
                    }
                }
                else
                {
                    FormattedText endText = new FormattedText($"Játék vége.", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 100, Brushes.White);
                    FormattedText resText = new FormattedText($"{this.GameModel.Player1.NumberOfWins} - {this.GameModel.Player2.NumberOfWins}", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 72, Brushes.White);
                    FormattedText winText;

                    if (this.GameModel.Player1.NumberOfWins == 5)
                    {
                        winText = new FormattedText($"Gőztes: {this.GameModel.Player1.Name}", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 50, Brushes.White);
                    }
                    else
                    {
                         winText = new FormattedText($"Gőztes: {this.GameModel.Player2.Name}", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 50, Brushes.White);
                    }

                    drawingContext.DrawRectangle(Brushes.Black, null, new Rect(0, 0, this.ActualWidth, this.ActualHeight));
                    drawingContext.DrawText(endText, new Point((this.ActualWidth - endText.Width) / 2, ((this.ActualHeight - endText.Height) / 2) - 100));
                    drawingContext.DrawText(resText, new Point((this.ActualWidth - resText.Width) / 2, (this.ActualHeight - resText.Height) / 2));
                    drawingContext.DrawText(winText, new Point((this.ActualWidth - winText.Width) / 2, ((this.ActualHeight - winText.Height) / 2) + 100));
                }
            }
        }

        /// <summary>
        /// Runs when gamecontrol is loaded
        /// </summary>
        /// <param name="sender">param</param>
        /// <param name="e">event</param>
        private void GameControl_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            this.logic = new GameLogic(this.GameModel);
            this.display = new GameDisplay(this.GameModel, 1000, 600);

            this.logic.ScreenRefresh += this.Logic_ScreenRefresh;
            this.logic.SaveGameState();

            window.KeyDown += this.Window_KeyDown;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(33.3333333);
            timer.Tick += this.Timer_Tick;
            timer.Start();
        }

        /// <summary>
        /// Screenrefresh
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void Logic_ScreenRefresh(object sender, EventArgs e)
        {
            this.InvalidateVisual();
        }

        /// <summary>
        /// Timer tick
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            this.Logic_ScreenRefresh(sender, e);
        }

        /// <summary>
        /// Keydown event
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
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

        /// <summary>
        /// Enables the music.
        /// </summary>
        public void EnableMusic()
        {
            this.logic.EnableBackgroundMusic();
        }

        /// <summary>
        /// Disables the music.
        /// </summary>
        public void DisableMusic()
        {
            this.logic.DisableBackgroundMusic();
        }

        /// <summary>
        /// Change players name
        /// </summary>
        /// <param name="name1">player1 name</param>
        /// <param name="name2">player2 name</param>
        public void ChangePlayersName(string name1, string name2)
        {
            this.logic.AddNameToPlayers(name1, name2);
        }

        /// <summary>
        /// Change difficulty
        /// </summary>
        /// <param name="difficulty">difficulty level</param>
        public void ChangeDifficulty(Difficulty difficulty)
        {
            this.logic.ChangeDifficulty(difficulty);
        }

        /// <summary>
        /// Pause game
        /// </summary>
        public void PauseGame()
        {
            this.logic.PauseGame();
        }

        /// <summary>
        /// Continue game
        /// </summary>
        public void ContinueGame()
        {
            this.logic.ContinueGame();
        }

        /// <summary>
        /// New game
        /// </summary>
        public void NewGame()
        {
            this.logic.NewGame();
        }

        /// <summary>
        /// Load game
        /// </summary>
        /// <param name="filename"></param>
        public void LoadGame(string filename)
        {
            this.logic.LoadGameState(filename);
        }
    }
}
