namespace TronGame.Display
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using TronGame.BusinessLogic;

    public class GameScreen : FrameworkElement
    {
        private GameModel model;
        private IBusinessLogic logic;

        public GameScreen()
        {
            this.model = new GameModel();
            Loaded += GameScreen_Loaded;
        }

        private void GameScreen_Loaded(object sender, RoutedEventArgs e)
        {
            this.logic = new GameLogic();
        }
    }
}
