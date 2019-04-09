namespace TronGame.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public enum Difficulty
    {
        Easy, Medium, Hard
    }

    public class GameRepository : IRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameRepository"/> class.
        /// </summary>
        public GameRepository()
        {
        }

        public Player Player1 { get; private set; }

        public Player Player2 { get; private set; }

        public Difficulty GameDifficulty { get; set; }

        public GameObject[,] GameField { get; private set; }

        public int HighScore { get; private set; }

        public Difficulty GetDifficulty()
        {
            return this.GameDifficulty;
        }

        public GameObject[,] GetGameField()
        {
            return this.GameField;
        }

        public int GetHighScore(int score)
        {
            return this.HighScore;
        }

        public Player GetPlayer(int numOfPlayer)
        {
            if (numOfPlayer == 1)
            {
                return this.Player1;
            }
            else
            {
                return this.Player2;
            }
        }

        public void SetDifficulty(int difficulty)
        {
            switch (difficulty)
            {
                case 0:
                    this.GameDifficulty = Difficulty.Easy;
                    break;
                case 1:
                    this.GameDifficulty = Difficulty.Medium;
                    break;
                case 2:
                    this.GameDifficulty = Difficulty.Hard;
                    break;
                default:
                    this.GameDifficulty = Difficulty.Medium;
                    break;
            }
        }

        public void SetHighScore(string playerOneName, string playerTwoName, int firstPlayerScore, int secondPlayersScore, double time)
        {
            throw new NotImplementedException();
        }

        public void SetPlayersName(string playerOneName, string playerTwoName)
        {
            this.Player1.Name = playerOneName;
            this.Player2.Name = playerTwoName;
        }

        public void SetVolume(double value)
        {
            throw new NotImplementedException();
        }
    }
}
