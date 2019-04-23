﻿namespace TronGame.Model
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;
    using TronGame.Repository;

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
            this.GameField = new GameObject[643, 984];
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
