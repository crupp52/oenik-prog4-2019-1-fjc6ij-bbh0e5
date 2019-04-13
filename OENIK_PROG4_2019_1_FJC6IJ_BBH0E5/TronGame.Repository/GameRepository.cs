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
            this.ResetGameField();
            this.SetPlayerPositon(1);
            this.SetPlayerPositon(2);
            this.GenerateObjects();

            this.Difficulty = Difficulty.Medium;
            this.HighScore = new HighScore();
        }

        [XmlElement]
        public Difficulty Difficulty { get; set; }

        public HighScore HighScore { get; set; }

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }

        [XmlIgnore]
        public GameObject[,] GameField { get; set; }

        public List<ObstacleObject> Obstacles { get; set; }

        public List<TurboObject> Turbos { get; set; }

        public void SetDifficulty(int difficulty)
        {
            switch (difficulty)
            {
                case 0:
                    this.Difficulty = Difficulty.Easy;
                    break;
                case 1:
                    this.Difficulty = Difficulty.Medium;
                    break;
                case 2:
                    this.Difficulty = Difficulty.Hard;
                    break;
                default:
                    this.Difficulty = Difficulty.Medium;
                    break;
            }
        }

        public void SetHighScore(string playerOneName, string playerTwoName, int firstPlayerScore, int secondPlayersScore, double time)
        {
            throw new NotImplementedException();
        }

        public void SetVolume(double value)
        {
            throw new NotImplementedException();
        }

        public void SetPlayerName(int numOfPlayer, string name)
        {
            if (numOfPlayer == 1)
            {
                this.Player1.Name = name;
            }
            else
            {
                this.Player2.Name = name;
            }
        }

        public void SetNewObjectOnField(ObjectType objectType, GameObject item)
        {
            switch (objectType)
            {
                case ObjectType.Player:
                    this.GameField[item.PosY, item.PosX] = (Player)item;
                    break;
                case ObjectType.Turbo:
                    this.GameField[item.PosY, item.PosX] = (TurboObject)item;
                    break;
                case ObjectType.Obstacle:
                    this.GameField[item.PosY, item.PosX] = (ObstacleObject)item;
                    break;
            }
        }

        public void ResetGameField()
        {
            this.GameField = new GameObject[100, 100];
        }

        public void ResetPlayers()
        {
            this.Player1 = new Player();
            this.Player2 = new Player();
        }

        private void SetPlayerPositon(int numOfPlayer)
        {
            int posX = rnd.Next(0, 100);
            int posY = rnd.Next(0, 100);
            while (this.GameField[posY, posX] != null)
            {
                posX = rnd.Next(0, 100);
                posY = rnd.Next(0, 100);
            }

            if (numOfPlayer == 1)
            {
                this.Player1.PosX = posX;
                this.Player1.PosY = posY;
            }
            else
            {
                this.Player2.PosX = posX;
                this.Player2.PosY = posY;
            }
        }

        private void GenerateObjects()
        {
            for (int i = 0; i < 5; i++)
            {
                this.Obstacles.Add(new ObstacleObject());
            }

            for (int i = 0; i < 3; i++)
            {
                this.Turbos.Add(new TurboObject());
            }
        }
    }
}
