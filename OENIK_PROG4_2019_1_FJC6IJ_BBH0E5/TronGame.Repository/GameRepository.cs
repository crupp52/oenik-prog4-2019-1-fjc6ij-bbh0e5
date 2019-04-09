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

    public class GameRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameRepository"/> class.
        /// </summary>
        public GameRepository()
        {
            this.Rnd = new Random();
        }

        public Random Rnd { get; private set; }

        public Player Player1 { get; private set; }

        public Player Player2 { get; private set; }

        public Difficulty GameDifficulty { get; set; }

        public GameObject[,] GameField { get; private set; }

        public void SetNewGame()
        {
            this.GameField = new GameObject[100, 100];
        }

        private void SetObstacles()
        {
            switch (this.GameDifficulty)
            {
                case Difficulty.Easy:
                    this.GenerateObstacles(3);
                    break;
                case Difficulty.Medium:
                    this.GenerateObstacles(5);
                    break;
                case Difficulty.Hard:
                    this.GenerateObstacles(7);
                    break;
            }
        }

        private void GenerateObstacles(int num)
        {
            int i = 0;
            while (i != num)
            {
                int posX = this.Rnd.Next(0, 100);
                int posY = this.Rnd.Next(0, 100);
                if (this.GameField[posY, posX] == null)
                {
                    ObstacleObject obstacle = new ObstacleObject(posX, posY);
                    i++;
                }
            }
        }

        private void GenerateTurbos(int num)
        {
            int i = 0;
            while (i != num)
            {
                int posX = this.Rnd.Next(0, 100);
                int posY = this.Rnd.Next(0, 100);
                if (this.GameField[posY, posX] == null)
                {
                    TurboObject obstacle = new TurboObject(posX, posY);
                    i++;
                }
            }
        }
    }
}
