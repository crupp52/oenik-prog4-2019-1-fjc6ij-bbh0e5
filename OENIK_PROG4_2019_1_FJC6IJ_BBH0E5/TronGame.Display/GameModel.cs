namespace TronGame.Display
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TronGame.Repository;

    public class GameModel : IModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameModel"/> class.
        /// </summary>
        public GameModel()
        {
            this.Obstacles = new List<ObstacleObject>();
            this.Turbos = new List<TurboObject>();
            this.Player1 = new Player();
            this.Player2 = new Player();
            this.Difficulty = Difficulty.Medium;
            this.HighScore = new HighScore();
        }

        public Difficulty Difficulty { get; set; }

        public HighScore HighScore { get; set; }

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

        public List<ObstacleObject> Obstacles { get; set; }

        public List<TurboObject> Turbos { get; set; }

        public GameObject[,] GameField { get; set; }
    }
}
