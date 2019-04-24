namespace TronGame.Display
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media;
    using TronGame.BusinessLogic;
    using TronGame.Model;

    public class GameControl : FrameworkElement
    {
        private IGameModel model;
        private IBusinessLogic logic;
        private GameScreen display;

        public GameControl()
        {
            this.Loaded += this.GameControl_Loaded;
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

        protected override void OnRender(DrawingContext drawingContext)
        {
            if (this.logic != null && this.ActualWidth != 0)
            {
                drawingContext.DrawDrawing(null);
            }
        }
    }
}
