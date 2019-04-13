namespace TronGame.Repository
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using System.Xml.Serialization;

    public enum ObjectType
    {
        Player, Turbo, Obstacle
    }

    public enum Difficulty
    {
        Easy, Medium, Hard
    }

    [XmlRoot]
    public class GameRepository : IRepository
    {
        private static Random rnd;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameRepository"/> class.
        /// </summary>
        public GameRepository()
        {
            rnd = new Random();

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

        [XmlIgnore]
        public GameObject[,] GameField { get; set; }
    }
}
