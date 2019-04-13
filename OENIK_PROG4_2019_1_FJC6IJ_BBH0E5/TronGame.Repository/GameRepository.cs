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

    public class GameRepository : IRepository
    {
        private static Random rnd;
        private Player player1;
        private Player player2;
        private Difficulty gameDifficulty;
        private GameObject[,] gameField;
        private int highScore;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameRepository"/> class.
        /// </summary>
        public GameRepository()
        {
            this.player1 = new Player();
            this.player2 = new Player();
            this.SetPlayerPositon(1);
            this.SetPlayerPositon(2);

            this.gameDifficulty = Difficulty.Medium;
            this.gameField = new GameObject[100, 100];
            this.highScore = 0;

            rnd = new Random();
        }

        private void SetPlayerPositon(int numOfPlayer)
        {
            int posX = rnd.Next(0, 100);
            int posY = rnd.Next(0, 100);
            while (this.gameField[posY, posX] != null)
            {
                posX = rnd.Next(0, 100);
                posY = rnd.Next(0, 100);
            }

            if (numOfPlayer == 1)
            {
                this.player1.PosX = posX;
                this.player1.PosY = posY;
            }
            else
            {
                this.player2.PosX = posX;
                this.player2.PosY = posY;
            }
        }

        public Difficulty GetDifficulty()
        {
            return this.gameDifficulty;
        }

        public GameObject[,] GetGameField()
        {
            return this.gameField;
        }

        public int GetHighScore(int score)
        {
            return this.highScore;
        }

        public int GetHighScore()
        {
            throw new NotImplementedException();
        }

        public Player GetPlayer(int numOfPlayer)
        {
            if (numOfPlayer == 1)
            {
                return this.player1;
            }
            else
            {
                return this.player2;
            }
        }

        public void SetDifficulty(int difficulty)
        {
            switch (difficulty)
            {
                case 0:
                    this.gameDifficulty = Difficulty.Easy;
                    break;
                case 1:
                    this.gameDifficulty = Difficulty.Medium;
                    break;
                case 2:
                    this.gameDifficulty = Difficulty.Hard;
                    break;
                default:
                    this.gameDifficulty = Difficulty.Medium;
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
                this.player1.Name = name;
            }
            else
            {
                this.player2.Name = name;
            }
        }

        public void SetNewObjectOnField(ObjectType objectType, GameObject item)
        {
            switch (objectType)
            {
                case ObjectType.Player:
                    this.gameField[item.PosY, item.PosX] = (Player)item;
                    break;
                case ObjectType.Turbo:
                    this.gameField[item.PosY, item.PosX] = (TurboObject)item;
                    break;
                case ObjectType.Obstacle:
                    this.gameField[item.PosY, item.PosX] = (ObstacleObject)item;
                    break;
            }
        }

        public void ResetGameField()
        {
            this.gameField = new GameObject[100, 100];
        }

        public void ResetPlayers()
        {
            this.player1 = new Player();
            this.player1 = new Player();
        }

        public void SaveGamestate()
        {
            XmlSerializer x = new XmlSerializer(this.GetType());
            string filename = string.Format($"save{DateTime.Now:yyyyMMddHHmmss}.xml");
            using (StreamWriter sw = new StreamWriter(filename, false, Encoding.UTF8))
            {
                x.Serialize(sw, this);
            }
        }

        public void LoadGamestate(string filename)
        {
            if (File.Exists(filename))
            {
                XDocument xdoc = XDocument.Load(filename);
            }
        }
    }
}
