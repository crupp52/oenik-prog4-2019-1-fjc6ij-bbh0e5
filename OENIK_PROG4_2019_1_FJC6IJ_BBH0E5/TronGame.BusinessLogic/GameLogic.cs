namespace TronGame.BusinessLogic
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Windows;
    using System.Xml.Serialization;
    using TronGame.Model;
    using TronGame.Repository;

    public class GameLogic : IBusinessLogic
    {
        private static Random rnd;
        private Stopwatch sw;
        private IGameModel gameModel;

        public GameLogic(IGameModel model)
        {
            this.gameModel = model;

            this.sw = new Stopwatch();
            rnd = new Random();

            this.TestGame();
        }

        public event EventHandler ScreenRefresh;

        private void TestGame()
        {
            this.AddNameToPlayers("Karcsi", "Kata");
            this.NewGame();
            this.NewRound();
        }

        public void AddNameToPlayers(string player1Name, string player2Name)
        {
            this.gameModel.Player1.Name = player1Name;
            this.gameModel.Player2.Name = player2Name;
        }

        public void NewGame()
        {
            this.gameModel.Player1 = new Player();
            this.gameModel.Player2 = new Player();
        }

        public void NewRound()
        {
            this.SetPlayerStartPositon(this.gameModel.Player1);
            this.SetPlayerStartPositon(this.gameModel.Player2);
            this.gameModel.Obstacles.Clear();
            this.gameModel.Turbos.Clear();
            this.SetObstacles();
            this.SetTurbos();

            this.sw.Restart();
            this.ScreenRefresh?.Invoke(this, EventArgs.Empty);
        }

        public void EndGame()
        {
            this.sw.Stop();
        }

        public void MovePlayer(Player player, MovingDirection direction)
        {
            player.Move(direction);
            this.ScreenRefresh?.Invoke(this, EventArgs.Empty);
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

            this.ScreenRefresh?.Invoke(this, EventArgs.Empty);
        }

        public void SaveGameState()
        {
            XmlSerializer x = new XmlSerializer(this.gameModel.GetType());
            string filename = string.Format($"save{DateTime.Now:yyyyMMddHHmmss}.xml");
            using (StreamWriter sw = new StreamWriter(filename, false, Encoding.UTF8))
            {
                x.Serialize(sw, this.gameModel);
            }
        }

        public void LoadGameState(string filename)
        {
            if (File.Exists(filename))
            {
                XmlSerializer x = new XmlSerializer(this.gameModel.GetType());
                using (StreamReader sr = new StreamReader(filename, Encoding.UTF8))
                {
                    this.gameModel = (GameModel)x.Deserialize(sr);
                }
            }
        }

        private void DiePlayer(Player player)
        {
            if (player == this.gameModel.Player1)
            {
                this.WinRound(this.gameModel.Player2);
            }
            else
            {
                this.WinRound(this.gameModel.Player1);
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
            switch (this.gameModel.Difficulty)
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
            switch (this.gameModel.Difficulty)
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
                int posX = rnd.Next(0, 1000);
                int posY = rnd.Next(0, 500);
                if (this.gameModel.GameField[posY, posX] == null)
                {
                    ObstacleObject o = new ObstacleObject() { PosX = posX, PosY = posY, Area = new Rect(posX, posY, 40, 40) };
                    this.gameModel.Obstacles.Add(o);
                    this.gameModel.GameField[posY, posX] = o;
                    i++;
                }
            }
        }

        private void GenerateTurbos(int num)
        {
            int i = 0;
            while (i != num)
            {
                int posX = rnd.Next(0, 1000);
                int posY = rnd.Next(0, 500);
                if (this.gameModel.GameField[posY, posX] == null)
                {
                    TurboObject o = new TurboObject() { PosX = posX, PosY = posY, Area = new Rect(posX, posY, 40, 40) };
                    this.gameModel.Turbos.Add(o);
                    this.gameModel.GameField[posY, posX] = o;
                    i++;
                }
            }
        }

        private void SetPlayerStartPositon(Player player)
        {
            int posX = rnd.Next(0, 1000);
            int posY = rnd.Next(0, 500);
            while (this.gameModel.GameField[posY, posX] != null)
            {
                posX = rnd.Next(0, 1000);
                posY = rnd.Next(0, 500);
            }

            player.PosX = posX;
            player.PosY = posY;
            player.Area = new Rect(posX, posY, 40, 40);
            this.gameModel.GameField[posY, posX] = player;
        }
    }
}
