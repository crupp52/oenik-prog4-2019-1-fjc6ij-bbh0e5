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

            this.sw = new Stopwatch();
            rnd = new Random();
        }

        public GameLogic(IRepository repository)
        {
            this.GameRepository = repository;

            this.sw = new Stopwatch();
            rnd = new Random();
        }

        public event EventHandler ScreenRefresh;

        public IRepository GameRepository { get; set; }

        public void AddNameToPlayers(string player1Name, string player2Name)
        {
            this.GameRepository.Player1.Name = player1Name;
            this.GameRepository.Player2.Name = player2Name;
        }

        public void NewGame()
        {
            this.GameRepository.Player1 = new Player();
            this.GameRepository.Player2 = new Player();
        }

        public void NewRound()
        {
            this.SetPlayerStartPositon(this.GameRepository.Player1);
            this.SetPlayerStartPositon(this.GameRepository.Player2);
            this.GameRepository.Obstacles.Clear();
            this.GameRepository.Turbos.Clear();
            this.SetObstacles();
            this.SetTurbos();

            this.sw.Restart();
        }

        public void EndGame()
        {
            this.sw.Stop();
        }

        public void MovePlayer(Player player, MovingDirection direction)
        {
            player.Move(direction);
        }

        public void PickUp(Player player, ObjectType objectType)
        {
            switch (objectType)
            {
                case ObjectType.Player:
                    this.DiePlayer(player);
                    break;
                case ObjectType.Turbo:
                    this.UseTurbo(player);
                    break;
                case ObjectType.Obstacle:
                    this.DiePlayer(player);
                    break;
            }
        }

        public void SaveGameState()
        {
            XmlSerializer x = new XmlSerializer(this.GameRepository.GetType());
            string filename = string.Format($"save{DateTime.Now:yyyyMMddHHmmss}.xml");
            using (StreamWriter sw = new StreamWriter(filename, false, Encoding.UTF8))
            {
                x.Serialize(sw, this.GameRepository);
            }
        }

        public void LoadGameState(string filename)
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

        private void DiePlayer(Player player)
        {
            if (player == this.GameRepository.Player1)
            {
                this.WinRound(this.GameRepository.Player2);
            }
            else
            {
                this.WinRound(this.GameRepository.Player1);
            }
        }

        private void WinRound(Player player)
        {
            if (++player.NumberOfWins == 5)
            {
                this.EndGame();
            }
        }

        private void UseTurbo(Player player)
        {
            if (player.NumberOfTurbos > 0)
            {
                player.SpeedUp();
            }
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

        private void SetPlayerStartPositon(Player player)
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
            this.GameRepository.GameField[posY, posX] = player;
        }
    }
}
