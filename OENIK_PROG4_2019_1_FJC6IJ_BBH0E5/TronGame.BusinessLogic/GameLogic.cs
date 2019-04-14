namespace TronGame.BusinessLogic
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Xml.Serialization;
    using TronGame.Repository;

    public class GameLogic : IBusinessLogic
    {
        private static Random rnd;
        private Stopwatch sw;

        public GameLogic()
        {
            this.GameRepository = new GameRepository();
            this.InitialLogic();
        }

        public GameLogic(IRepository repository)
        {
            this.GameRepository = repository;
            this.InitialLogic();
        }

        public event EventHandler ScreenRefresh;

        public IRepository GameRepository { get; set; }

        public void SetNewGame()
        {
            this.ResetToDefaultValues();
            this.SetObstacles();
            this.GenerateTurbos(4);
        }

        public void CreateNotification(int type, string message)
        {
            throw new NotImplementedException();
        }

        public void Die(Player player)
        {
            if (player == this.GameRepository.Player1)
            {
                this.GameRepository.Player2.NumberOfWins++;
            }
            else
            {
                this.GameRepository.Player1.NumberOfWins++;
            }
        }

        public void MovePlayer(MovingDirection direction, Player player)
        {
            player.Move(direction);
        }

        public void PauseTimer()
        {
            this.sw.Stop();
        }

        public void ResetAfterRoundWin()
        {
            this.GameRepository.GameField = new GameObject[100, 100];
            this.SetPlayersPositon(this.GameRepository.Player1);
            this.SetPlayersPositon(this.GameRepository.Player2);
            this.GameRepository.GameField[this.GameRepository.Player1.PosY, this.GameRepository.Player1.PosX] = this.GameRepository.Player1;
            this.GameRepository.GameField[this.GameRepository.Player2.PosY, this.GameRepository.Player2.PosX] = this.GameRepository.Player2;
        }

        public void ResetTimer()
        {
            this.sw.Reset();
        }

        public void ResetToDefaultValues()
        {
            this.GameRepository.Obstacles.Clear();
            this.GameRepository.Turbos.Clear();
            this.GameRepository.Player1 = new Player();
            this.GameRepository.Player2 = new Player();
            this.ResetAfterRoundWin();
        }

        public void StartTimer()
        {
            this.sw.Start();
        }

        public void PickUp(ObjectType objectType, Player player)
        {
            switch (objectType)
            {
                case ObjectType.Player:
                    this.Die(player);
                    break;
                case ObjectType.Turbo:
                    this.UseTurbo(player);
                    break;
                case ObjectType.Obstacle:
                    this.Die(player);
                    break;
            }
        }

        public void UseTurbo(Player player)
        {
            if (player.NumberOfTurbos > 0)
            {
                player.SpeedUp();
            }
        }

        public void SaveGamestate()
        {
            XmlSerializer x = new XmlSerializer(this.GameRepository.GetType());
            string filename = string.Format($"save{DateTime.Now:yyyyMMddHHmmss}.xml");
            using (StreamWriter sw = new StreamWriter(filename, false, Encoding.UTF8))
            {
                x.Serialize(sw, this.GameRepository);
            }
        }

        public void LoadGamestate(string filename)
        {
            if (File.Exists(filename))
            {
                XmlSerializer x = new XmlSerializer(this.GameRepository.GetType());
                using (StreamReader sr = new StreamReader(filename, Encoding.UTF8))
                {
                    this.GameRepository = (GameRepository)x.Deserialize(sr);
                }
            }
        }

        private void InitialLogic()
        {
            this.sw = new Stopwatch();
            rnd = new Random();

            this.ResetToDefaultValues();
            this.SetObstacles();
            this.SetTurbos();
        }

        private void SetObstacles()
        {
            switch (this.GameRepository.Difficulty)
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

        private void SetTurbos()
        {
            switch (this.GameRepository.Difficulty)
            {
                case Difficulty.Easy:
                    this.GenerateTurbos(7);
                    break;
                case Difficulty.Medium:
                    this.GenerateTurbos(5);
                    break;
                case Difficulty.Hard:
                    this.GenerateTurbos(3);
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
                if (this.GameRepository.GameField[posY, posX] == null)
                {
                    ObstacleObject o = new ObstacleObject() { PosX = posX, PosY = posY };
                    this.GameRepository.Obstacles.Add(o);
                    this.GameRepository.GameField[posY, posX] = o;
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
                if (this.GameRepository.GameField[posY, posX] == null)
                {
                    TurboObject o = new TurboObject() { PosX = posX, PosY = posY };
                    this.GameRepository.Turbos.Add(o);
                    this.GameRepository.GameField[posY, posX] = o;
                    i++;
                }
            }
        }

        private void SetPlayersPositon(Player player)
        {
            int posX = rnd.Next(0, 100);
            int posY = rnd.Next(0, 100);
            while (this.GameRepository.GameField[posY, posX] != null)
            {
                posX = rnd.Next(0, 100);
                posY = rnd.Next(0, 100);
            }

            player.PosX = posX;
            player.PosY = posY;
        }
    }
}
