namespace TronGame.Model
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using System.Xml.Serialization;
    using TronGame.Repository;

    /// <summary>
    /// Contains the instances of GameObjects
    /// </summary>
    public class GameModel : IGameModel
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
            this.GameField = new GameObject[30, 50];
        }

        /// <summary>
        /// Gets or sets difficulty of the game
        /// </summary>
        public Difficulty Difficulty { get; set; }

        /// <summary>
        /// Gets or sets HighScore
        /// </summary>
        public HighScore HighScore { get; set; }

        /// <summary>
        /// Gets or sets Player1
        /// </summary>
        public Player Player1 { get; set; }

        /// <summary>
        /// Gets or sets Player2
        /// </summary>
        public Player Player2 { get; set; }

        /// <summary>
        /// Gets or sets Obstacles
        /// </summary>
        public List<ObstacleObject> Obstacles { get; set; }

        /// <summary>
        /// Gets or sets Turbos
        /// </summary>
        public List<TurboObject> Turbos { get; set; }

        /// <summary>
        /// Gets or sets GameField
        /// </summary>
        [XmlIgnore]
        public GameObject[,] GameField { get; set; }
    }
}
