namespace TronGame.Display
{
    using System;
    using System.Windows;
    using System.Windows.Media;
    using TronGame.BusinessLogic;
    using TronGame.Model;

    /// <summary>
    /// Game controlling methods
    /// </summary>
    public class GameControl : FrameworkElement
    {
        private IGameModel model;
        private IBusinessLogic logic;
        private GameScreen display;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameControl"/> class.
        /// </summary>
        public GameControl()
        {
            this.Loaded += this.GameControl_Loaded;
        }

        /// <summary>
        /// Render the generated Drawings.
        /// </summary>
        /// <param name="drawingContext">DrawingContext parameter</param>
        protected override void OnRender(DrawingContext drawingContext)
        {
            if (this.logic != null && this.ActualWidth != 0)
            {
                drawingContext.DrawDrawing(null);
            }
        }

        private void GameControl_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            if (window != null)
            {
                this.model = new GameModel();
                this.logic = new GameLogic(this.model);
                this.display = new GameScreen();

                window.KeyDown += this.Window_KeyDown;
            }
        }

        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
