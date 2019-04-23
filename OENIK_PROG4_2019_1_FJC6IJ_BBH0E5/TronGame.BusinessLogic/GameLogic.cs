namespace TronGame.BusinessLogic
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Windows;
    using System.Windows.Media;
    using System.Xml.Serialization;
    using TronGame.Model;
    using TronGame.Repository;

    public class GameLogic : IBusinessLogic
    {
        private static Random rnd;
        private Stopwatch sw;
        private MediaPlayer backgroundMediaPlayer;

        public GameLogic(IGameModel model)
        {
            this.GameModel = model;

            this.sw = new Stopwatch();
            rnd = new Random();
            this.backgroundMediaPlayer = new MediaPlayer();
            this.backgroundMediaPlayer.Open(new Uri(@"../../../TronGame.Repository/Sounds/background.wav", UriKind.Relative));


            this.TestGame();
        }

        public event EventHandler ScreenRefresh;

        public IGameModel GameModel { get; private set; }

        private void TestGame()
        {
            this.AddNameToPlayers("Karcsi", "Kata");
            this.NewGame();
            this.NewRound();
            this.StartBackgroundSong();
        }

        public void AddNameToPlayers(string player1Name, string player2Name)
        {
            this.GameModel.Player1.Name = player1Name;
            this.GameModel.Player2.Name = player2Name;
        }

        public void NewGame()
        {
            this.GameModel.Player1 = new Player();
            this.GameModel.Player2 = new Player();
        }

        public void NewRound()
        {
            this.SetPlayerStartPositon(this.GameModel.Player1);
            this.SetPlayerStartPositon(this.GameModel.Player2);
            this.GameModel.Obstacles.Clear();
            this.GameModel.Turbos.Clear();
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
                    player.NumberOfTurbos++;
                    break;
                case ObjectType.Obstacle:
                    this.DiePlayer(player);
                    break;
            }

            this.ScreenRefresh?.Invoke(this, EventArgs.Empty);
        }

        public void UseTurbo(Player player)
        {
            if (player.NumberOfTurbos > 0)
            {
                player.SpeedUp();
            }
        }

        public void SaveGameState()
        {
            XmlSerializer x = new XmlSerializer(this.GameModel.GetType());
            string filename = string.Format($"save{DateTime.Now:yyyyMMddHHmmss}.xml");
            using (StreamWriter sw = new StreamWriter(filename, false, Encoding.UTF8))
            {
                x.Serialize(sw, this.GameModel);
            }
        }

        public void LoadGameState(string filename)
        {
            if (File.Exists(filename))
            {
                XmlSerializer x = new XmlSerializer(this.GameModel.GetType());
                using (StreamReader sr = new StreamReader(filename, Encoding.UTF8))
                {
                    this.GameModel = (GameModel)x.Deserialize(sr);
                }
            }
        }

        private void DiePlayer(Player player)
        {
            if (player == this.GameModel.Player1)
            {
                this.WinRound(this.GameModel.Player2);
            }
            else
            {
                this.WinRound(this.GameModel.Player1);
            }
        }

        private void WinRound(Player player)
        {
            if (++player.NumberOfWins == 5)
            {
                this.EndGame();
            }
        }

        private void SetObstacles()
        {
            switch (this.GameModel.Difficulty)
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
            switch (this.GameModel.Difficulty)
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
                int posX = rnd.Next(0, 944);
                int posY = rnd.Next(0, 603);
                if (this.GameModel.GameField[posY, posX] == null)
                {
                    ObstacleObject o = new ObstacleObject() { PosX = posX, PosY = posY, Area = new Rect(posX, posY, 40, 40) };
                    this.GameModel.Obstacles.Add(o);
                    this.GameModel.GameField[posY, posX] = o;
                    i++;
                }
            }
        }

        private void GenerateTurbos(int num)
        {
            int i = 0;
            while (i != num)
            {
                int posX = rnd.Next(0, 944);
                int posY = rnd.Next(0, 603);
                if (this.GameModel.GameField[posY, posX] == null)
                {
                    TurboObject o = new TurboObject() { PosX = posX, PosY = posY, Area = new Rect(posX, posY, 40, 40) };
                    this.GameModel.Turbos.Add(o);
                    this.GameModel.GameField[posY, posX] = o;
                    i++;
                }
            }
        }

        private void SetPlayerStartPositon(Player player)
        {
            int posX = rnd.Next(0, 944);
            int posY = rnd.Next(0, 603);
            while (this.GameModel.GameField[posY, posX] != null)
            {
                posX = rnd.Next(0, 944);
                posY = rnd.Next(0, 603);
            }

            player.PosX = posX;
            player.PosY = posY;
            player.Area = new Rect(posX, posY, 40, 40);
            this.GameModel.GameField[posY, posX] = player;
        }

        private void StartBackgroundSong()
        {
            backgroundMediaPlayer.Play();
        }
    }
}
