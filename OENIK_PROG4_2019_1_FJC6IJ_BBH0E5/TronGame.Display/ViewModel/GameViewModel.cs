namespace TronGame.Display
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using GalaSoft.MvvmLight;
    using TronGame.Model;

    /// <summary>
    /// GameViewModel viewmodel
    /// </summary>
    public class GameViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameViewModel"/> class.
        /// </summary>
        public GameViewModel()
        {
            this.GameControl = new GameControl();
            this.GameModel = new GameModel();
        }

        public GameControl GameControl { get; set; }

        public GameModel GameModel { get; set; }
    }
}
