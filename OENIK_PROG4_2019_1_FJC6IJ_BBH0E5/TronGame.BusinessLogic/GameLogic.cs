namespace TronGame.BusinessLogic
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using TronGame.Model;
    using TronGame.Repository;

    /// <summary>
    /// Business logic in the game
    /// </summary>
    public class GameLogic : IBusinessLogic
    {
        private static Random rnd;
        private Stopwatch sw;
        private MediaPlayer backgroundMediaPlayer;

        private int width;
        private int heigth;

        public event EventHandler ScreenRefresh;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameLogic"/> class.
        /// </summary>
        /// <param name="model">GameModel parameter</param>
        public GameLogic(IGameModel model)
        {
            this.GameModel = model;

            this.sw = new Stopwatch();
            rnd = new Random();
            this.backgroundMediaPlayer = new MediaPlayer();
            this.backgroundMediaPlayer.Open(new Uri(@"../../../TronGame.Repository/Sounds/background.wav", UriKind.Relative));

            this.width = 50;
            this.heigth = 30;

            this.ResetField();

            this.MovePlayers();
        }

        /// <summary>
        /// Gets GameModel
        /// </summary>
        public IGameModel GameModel { get; private set; }

        /// <summary>
        /// Set the names of the players
        /// </summary>
        /// <param name="player1Name">Name of Player1</param>
        /// <param name="player2Name">Name of Player2</param>
        public void AddNameToPlayers(string player1Name, string player2Name)
        {
            this.GameModel.Player1.Name = player1Name;
            this.GameModel.Player2.Name = player2Name;

            var xml = XDocument.Load(@"../../../TronGame.Repository/XMLs/settings.xml");
            xml.Root.SetElementValue("player1name", player1Name);
            xml.Root.SetElementValue("player2name", player2Name);
            xml.Save(@"../../../TronGame.Repository/XMLs/settings.xml");
        }

        public void GetPlayersNames()
        {
            var xml = XDocument.Load(@"../../../TronGame.Repository/XMLs/settings.xml");
            string player1Name = xml.Root.Element("player1name").Value;
            string player2Name = xml.Root.Element("player2name").Value;

            this.GameModel.Player1.Name = player1Name;
            this.GameModel.Player2.Name = player2Name;
        }

        /// <summary>
        /// Reset players
        /// </summary>
        public void NewGame()
        {
            this.GameModel.Player1 = new Player();
            this.GameModel.Player2 = new Player();
        }

        /// <summary>
        /// New round.
        /// </summary>
        public void NewRound()
        {
            this.SetPlayerStartPositon(this.GameModel.Player1);
            this.SetPlayerStartPositon(this.GameModel.Player2);
            this.GameModel.Obstacles.Clear();
            this.GameModel.Turbos.Clear();
            this.SetObstacles();
            this.SetTurbos();

            this.sw.Restart();
        }

        public void ResetField()
        {
            this.GameModel.GameField = new GameObject[30, 50];

            this.GameModel.Obstacles.Clear();
            this.GameModel.Turbos.Clear();
            this.SetObstacles();
            this.SetTurbos();

            this.SetPlayerStartPositon(this.GameModel.Player1);
            this.SetPlayerStartPositon(this.GameModel.Player2);

            this.sw.Start();
        }

        public void ChangeDifficulty(Difficulty difficulty)
        {
            var xml = XDocument.Load(@"../../../TronGame.Repository/XMLs/settings.xml");
            xml.Root.SetElementValue("difficulty", (int)difficulty);
            xml.Save(@"../../../TronGame.Repository/XMLs/settings.xml");
            this.GameModel.Difficulty = difficulty;

            this.ResetField();
        }

        /// <summary>
        /// Move the selected player to the selected direction
        /// </summary>
        /// <param name="player">Player instance</param>
        /// <param name="direction">Direction of the movement</param>
        public void MovePlayer(Player player, MovingDirection direction)
        {
            player.MovingDirection = direction;
        }

        private void MovePlayers()
        {
            Task.Run(() => this.MovePlayersProcess(this.GameModel.Player1));
            Task.Run(() => this.MovePlayersProcess(this.GameModel.Player2));
        }

        public bool IsGamePaused { get; set; }

        public void PauseGame()
        {
            this.IsGamePaused = true;
        }

        public void ContinueGame()
        {
            this.IsGamePaused = false;
        }

        private void MovePlayersProcess(Player player)
        {
            while (true)
            {
                if (!IsGamePaused)
                {
                    switch (player.MovingDirection)
                    {
                        case MovingDirection.Up:
                            if (player.Point.Y > 0)
                            {
                                player.Point = new Point(player.Point.X, player.Point.Y - 1);
                            }

                            break;
                        case MovingDirection.Down:
                            if (player.Point.Y < 29)
                            {
                                player.Point = new Point(player.Point.X, player.Point.Y + 1);
                            }

                            break;
                        case MovingDirection.Left:
                            if (player.Point.X > 0)
                            {
                                player.Point = new Point(player.Point.X - 1, player.Point.Y);
                            }

                            break;
                        case MovingDirection.Rigth:
                            if (player.Point.X < 49)
                            {
                                player.Point = new Point(player.Point.X + 1, player.Point.Y);
                            }

                            break;
                    }

                    if (this.CheckObstacles(player))
                    {
                        this.PickUp(player, ObjectType.Obstacle);
                    }
                    else if (this.CheckTurbos(player))
                    {
                        this.PickUp(player, ObjectType.Turbo);
                    }
                    else if (this.CheckPlayerRoute(player))
                    {
                        this.PickUp(player, ObjectType.Player);
                    }

                    this.GameModel.GameField[(int)player.Point.Y, (int)player.Point.X] = player;

                    if (player.Turbo)
                    {
                        Thread.Sleep(150);
                    }
                    else
                    {
                        Thread.Sleep(300);
                    }
                }

            }
        }

        private bool CheckPlayerRoute(Player player)
        {
            if ((this.GameModel.GameField[(int)player.Point.Y, (int)player.Point.X] as Player) != null && this.GameModel.GameField[(int)player.Point.Y, (int)player.Point.X].GetType() == typeof(Player))
            {
                return true;
            }

            return false;
        }

        private bool CheckObstacles(Player player)
        {
            foreach (var item in this.GameModel.Obstacles)
            {
                if (item.Point.X == player.Point.X && item.Point.Y == player.Point.Y)
                {
                    this.GameModel.Obstacles.Remove(item);
                    return true;
                }
            }

            return false;
        }

        private bool CheckTurbos(Player player)
        {
            foreach (var item in this.GameModel.Turbos)
            {
                if (item.Point.X == player.Point.X && item.Point.Y == player.Point.Y)
                {
                    this.GameModel.Turbos.Remove(item);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Pick up an object
        /// </summary>
        /// <param name="player">Who picked up the object</param>
        /// <param name="objectType">Type of object</param>
        public void PickUp(Player player, ObjectType objectType)
        {
            switch (objectType)
            {
                case ObjectType.Player:
                    this.DiePlayer(player);
                    this.ResetField();
                    break;
                case ObjectType.Turbo:
                    player.NumberOfTurbos++;
                    break;
                case ObjectType.Obstacle:
                    this.DiePlayer(player);
                    this.ResetField();
                    break;
            }
        }

        /// <summary>
        /// Speed up the selected player if it is posible
        /// </summary>
        /// <param name="player">Player instance</param>
        public void UseTurbo(Player player)
        {
            if (player.NumberOfTurbos > 0)
            {
                player.SpeedUp();
            }
        }

        /// <summary>
        /// Save the actual gamestate
        /// </summary>
        public void SaveGameState()
        {
            XmlSerializer x = new XmlSerializer(this.GameModel.GetType());
            string filename = string.Format($"save{DateTime.Now:yyyyMMddHHmmss}.xml");
            using (StreamWriter sw = new StreamWriter(filename, false, Encoding.UTF8))
            {
                x.Serialize(sw, this.GameModel);
            }
        }

        /// <summary>
        /// Restore the selected gamestate
        /// </summary>
        /// <param name="filename">Selected gamestate file</param>
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

        private void TestGame()
        {
            this.NewGame();
            this.GetPlayersNames();
            //this.AddNameToPlayers("Karcsi", "Kata");
            this.NewRound();
            this.StartBackgroundSong();

            this.GameModel.Player1.NumberOfTurbos = 2;

            this.MovePlayers();
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
            player.NumberOfWins++;

            this.IsGamePaused = true;
            Thread.Sleep(900);
            this.IsGamePaused = false;

            if (player.NumberOfWins == 5)
            {
                this.EndGame();
            }
        }

        private void EndGame()
        {
            this.HighScoreCheck();
            this.ResetPlayer(this.GameModel.Player1);
            this.ResetPlayer(this.GameModel.Player2);
            this.ResetField();
            this.sw.Stop();
        }

        private void HighScoreCheck()
        {
            if (this.GameModel.HighScore.Player1Score < this.GameModel.Player1.NumberOfWins || this.GameModel.HighScore.Player2Score < this.GameModel.Player1.NumberOfWins)
            {
                this.GameModel.HighScore = new HighScore() { DateTime = DateTime.Now, Player1Name = this.GameModel.Player1.Name, Player2Name = this.GameModel.Player2.Name, Player1Score = this.GameModel.Player1.NumberOfWins, Player2Score = this.GameModel.Player2.NumberOfWins };
            }
        }

        private void ResetPlayer(Player player)
        {
            player.NumberOfTurbos = 0;
            player.NumberOfWins = 0;
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
                int posX = rnd.Next(0, 50);
                int posY = rnd.Next(0, 30);

                ObstacleObject o = new ObstacleObject() { Point = new Point(posX, posY) };
                this.GameModel.Obstacles.Add(o);
                i++;
            }
        }

        private void GenerateTurbos(int num)
        {
            int i = 0;
            while (i != num)
            {
                int posX = rnd.Next(0, 50);
                int posY = rnd.Next(0, 30);

                TurboObject o = new TurboObject() { Point = new Point(posX, posY) };
                this.GameModel.Turbos.Add(o);
                i++;
            }
        }

        private void SetPlayerStartPositon(Player player)
        {
            int posX = rnd.Next(10, 40);
            int posY = rnd.Next(7, 23);
            while (this.GameModel.GameField[posY, posX] != null)
            {
                posX = rnd.Next(10, 40);
                posY = rnd.Next(7, 23);
            }

            player.Point = new Point(posX, posY);
            this.GameModel.GameField[posY, posX] = player;
        }

        public void StartBackgroundSong()
        {
            var xml = XDocument.Load(@"../../../TronGame.Repository/XMLs/settings.xml");
            var music = xml.Root.Element("music").Value;
            if (music == "1")
            {
                this.backgroundMediaPlayer.Play();
            }
        }

        public void EnableBackgroundMusic()
        {
            this.backgroundMediaPlayer.Play();
            var xml = XDocument.Load(@"../../../TronGame.Repository/XMLs/settings.xml");
            xml.Root.SetElementValue("music", 1);
            xml.Save(@"../../../TronGame.Repository/XMLs/settings.xml");
        }

        public void DisableBackgroundMusic()
        {
            this.backgroundMediaPlayer.Stop();
            var xml = XDocument.Load(@"../../../TronGame.Repository/XMLs/settings.xml");
            xml.Root.SetElementValue("music", 0);
            xml.Save(@"../../../TronGame.Repository/XMLs/settings.xml");
        }
    }
}
