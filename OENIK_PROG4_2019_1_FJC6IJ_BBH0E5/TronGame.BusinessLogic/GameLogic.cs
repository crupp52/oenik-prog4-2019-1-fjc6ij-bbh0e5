namespace TronGame.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TronGame.Repository;

    public enum MovingDirection
    {
        Up, Down, Left, Rigth
    }

    public class GameLogic : IBusinessLogic
    {
        private static Random rnd;
        private IRepository gameRepo;
        private Stopwatch sw;

        public GameLogic()
        {
            this.sw = new Stopwatch();
            rnd = new Random();
            this.gameRepo = new GameRepository();
        }

        public void SetNewGame()
        {
            this.SetObstacles();
            this.GenerateTurbos(4);
        }

        private void SetObstacles()
        {
            switch (this.gameRepo.GetDifficulty())
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
                int posX = rnd.Next(0, 100);
                int posY = rnd.Next(0, 100);
                if (this.gameRepo.GetGameField()[posY, posX] == null)
                {
                    this.gameRepo.SetNewObjectOnField(ObjectType.Obstacle, new ObstacleObject(posX, posY));
                    i++;
                }
            }
        }

        private void GenerateTurbos(int num)
        {
            int i = 0;
            while (i != num)
            {
                int posX = rnd.Next(0, 100);
                int posY = rnd.Next(0, 100);
                if (this.gameRepo.GetGameField()[posY, posX] == null)
                {
                    this.gameRepo.SetNewObjectOnField(ObjectType.Turbo, new ObstacleObject(posX, posY));
                    i++;
                }
            }
        }

        public void CreateNotification(int type, string message)
        {
            throw new NotImplementedException();
        }

        public void Die(int numOfPlayer)
        {
            throw new NotImplementedException();
        }

        public void MovePlayer(MovingDirection direction, int numOfPlayer)
        {
            switch (direction)
            {
                case MovingDirection.Up:
                    break;
                case MovingDirection.Down:
                    break;
                case MovingDirection.Left:
                    break;
                case MovingDirection.Rigth:
                    break;
                default:
                    break;
            }
        }

        public void PauseTimer()
        {
            this.sw.Stop();
        }

        public void ResetAfterRoundWin()
        {
            this.gameRepo.ResetGameField();
            this.gameRepo.SetNewObjectOnField(ObjectType.Player, this.gameRepo.GetPlayer(1));
            this.gameRepo.SetNewObjectOnField(ObjectType.Player, this.gameRepo.GetPlayer(2));
        }

        public void ResetTimer()
        {
            this.sw.Reset();
        }

        public void ResetToDefaultValues()
        {
            this.gameRepo.ResetGameField();
            this.gameRepo.ResetPlayers();
        }

        public void StartTimer()
        {
            this.sw.Start();
        }

        public void PickUp(ObjectType objectType, int numOfPlayer)
        {
            switch (objectType)
            {
                case ObjectType.Player:
                    this.Die(numOfPlayer);
                    break;
                case ObjectType.Turbo:
                    this.IncrementTurbo(numOfPlayer);
                    break;
                case ObjectType.Obstacle:
                    this.Die(numOfPlayer);
                    break;
            }
        }

        public void UseTurbo(int numOfPlayer)
        {
            if (this.gameRepo.GetPlayer(numOfPlayer).NumberOfTurbos > 0)
            {
                this.DecrementTurbo(numOfPlayer);
            }
        }

        private void IncrementTurbo(int numOfPlayer)
        {
            if (this.gameRepo.GetPlayer(numOfPlayer).NumberOfTurbos > 0)
            {
                this.gameRepo.GetPlayer(numOfPlayer).NumberOfTurbos++;
            }
        }

        private void DecrementTurbo(int numOfPlayer)
        {
            this.gameRepo.GetPlayer(numOfPlayer).NumberOfTurbos--;
        }
    }
}
