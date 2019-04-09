namespace TronGame.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TronGame.Repository;

    public class GameLogic : IBusinessLogic
    {
        public GameLogic()
        {
            this.GameRepo = new GameRepository();
        }

        public IRepository GameRepo { get; private set; }

        public void SetNewGame()
        {
            this.GameRepo.GameField = new GameObject[100, 100];
            this.SetObstacles();
            this.GenerateTurbos(4);
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

        public void CreateNotification(int type, string message)
        {
            throw new NotImplementedException();
        }

        public void Die()
        {
            throw new NotImplementedException();
        }

        public void MovePlayer()
        {
            throw new NotImplementedException();
        }

        public void PauseTimer()
        {
            throw new NotImplementedException();
        }

        public void PickUp(int itemType)
        {
            throw new NotImplementedException();
        }

        public void ResetAfterRoundWin()
        {
            throw new NotImplementedException();
        }

        public void ResetTimer()
        {
            throw new NotImplementedException();
        }

        public void ResetToDefaultValues()
        {
            throw new NotImplementedException();
        }

        public void StartTimer()
        {
            throw new NotImplementedException();
        }
    }
}
