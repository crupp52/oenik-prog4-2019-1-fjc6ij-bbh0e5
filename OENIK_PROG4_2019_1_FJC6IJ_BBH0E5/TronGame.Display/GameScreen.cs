﻿namespace TronGame.Display
{
    using System;
    using System.Windows;
    using System.Windows.Media;
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
            drawingContext.DrawRectangle(Brushes.Red, new Pen(Brushes.Black, 2), this.model.Player1.Area);
            drawingContext.DrawRectangle(Brushes.Blue, new Pen(Brushes.Black, 2), this.model.Player2.Area);

            foreach (var item in this.model.Obstacles)
            {
                drawingContext.DrawRectangle(Brushes.Black, null, item.Area);
            }

            foreach (var item in this.model.Turbos)
            {
                drawingContext.DrawRectangle(Brushes.Green, null, item.Area);
            }
        }

        private void GameScreen_Loaded(object sender, RoutedEventArgs e)
        {
            this.logic = new GameLogic(this.model);
            this.logic.ScreenRefresh += this.Logic_ScreenRefresh;
            Window window = Window.GetWindow(this);
            window.KeyDown += this.Window_KeyDown;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(33.3333333);
            timer.Tick += this.Timer_Tick;
            timer.Start();
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
            throw new NotImplementedException();
        }
    }
}
