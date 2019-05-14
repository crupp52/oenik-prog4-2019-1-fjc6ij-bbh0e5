namespace TronGame.BusinessLogic
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
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

            this.NewRound();

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

            //var xml = XDocument.Load(@"../../../TronGame.Repository/XMLs/settings.xml");
            //xml.Root.SetElementValue("player1name", player1Name);
            //xml.Root.SetElementValue("player2name", player2Name);
            //xml.Save(@"../../../TronGame.Repository/XMLs/settings.xml");
        }

        /// <summary>
        /// Get players names (set gamemodel properties)
        /// </summary>
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
            this.ResetPlayer(this.GameModel.Player1);
            this.ResetPlayer(this.GameModel.Player2);
            this.IsGameEnded = false;
            this.IsGamePaused = false;
            this.NewRound();
        }

        private void NewRound()
        {
            this.GameModel.GameField = new int[30, 50];
            this.SetPlayerStartPositon(this.GameModel.Player1);
            this.SetPlayerStartPositon(this.GameModel.Player2);
            this.GameModel.Obstacles.Clear();
            this.GameModel.Turbos.Clear();
            this.SetObstacles();
            this.SetTurbos();

            this.sw.Restart();
        }

        /// <summary>
        /// Reset gamefield
        /// </summary>
        public void ResetField()
        {
            this.GameModel.GameField = new int[30, 50];
            this.SetPlayerStartPositon(this.GameModel.Player1);
            this.SetPlayerStartPositon(this.GameModel.Player2);
            this.GameModel.Obstacles.Clear();
            this.GameModel.Turbos.Clear();
            this.SetObstacles();
            this.SetTurbos();
            this.sw.Start();
        }

        /// <summary>
        /// Change the game difficulty
        /// </summary>
        /// <param name="difficulty"></param>
        public void ChangeDifficulty(Difficulty difficulty)
        {
            //var xml = XDocument.Load(@"../../../TronGame.Repository/XMLs/settings.xml");
            //xml.Root.SetElementValue("difficulty", (int)difficulty);
            //xml.Save(@"../../../TronGame.Repository/XMLs/settings.xml");
            this.GameModel.Difficulty = difficulty;

            this.NewRound();
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

        /// <summary>
        /// Move the players to a specific direction
        /// </summary>
        private void MovePlayers()
        {
            Task.Run(() => this.MovePlayersProcess(this.GameModel.Player1));
            Task.Run(() => this.MovePlayersProcess(this.GameModel.Player2));
        }

        /// <summary>
        /// Is the game paused
        /// </summary>
        public bool IsGamePaused { get; private set; }

        /// <summary>
        /// Is the game ended
        /// </summary>
        public bool IsGameEnded { get; private set; }

        public bool IsMusicEnabled { get; private set; }

        /// <summary>
        /// Pause the game
        /// </summary>
        public void PauseGame()
        {
            this.IsGamePaused = true;
        }

        /// <summary>
        /// Continue the game
        /// </summary>
        public void ContinueGame()
        {
            this.IsGamePaused = false;
        }

        /// <summary>
        /// Move player sidefunction
        /// </summary>
        /// <param name="player">Player object</param>
        private void MovePlayersProcess(Player player)
        {
            while (true)
            {
                if (!this.IsGamePaused && !this.IsGameEnded)
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

                    if (player == this.GameModel.Player1)
                    {
                        this.GameModel.GameField[(int)player.Point.Y, (int)player.Point.X] = 1;
                    }
                    else
                    {
                        this.GameModel.GameField[(int)player.Point.Y, (int)player.Point.X] = 2;
                    }

                    if (player.Turbo)
                    {
                        Thread.Sleep(150);
                    }
                    else
                    {
                        Thread.Sleep(300);
                    }
                }
                else
                {
                    Thread.Sleep(500);
                }
            }
        }

        /// <summary>
        /// Check player route
        /// </summary>
        /// <param name="player">Player object</param>
        /// <returns></returns>
        private bool CheckPlayerRoute(Player player)
        {
            if (this.GameModel.GameField[(int)player.Point.Y, (int)player.Point.X] != 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check obstackles
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Check if pickup object is turbo
        /// </summary>
        /// <param name="player">Player object</param>
        /// <returns>Is turbo object</returns>
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

        private void PickUp(Player player, ObjectType objectType)
        {
            switch (objectType)
            {
                case ObjectType.Player:
                    this.DiePlayer(player);
                    this.NewRound();
                    break;
                case ObjectType.Turbo:
                    player.NumberOfTurbos++;
                    break;
                case ObjectType.Obstacle:
                    this.DiePlayer(player);
                    this.NewRound();
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
                this.GameModel.GameField = new int[30, 50];

                XDocument xdoc = XDocument.Load(filename);
                this.GameModel.Player1.Name = xdoc.Element("GameModel").Element("Player1").Element("Name").Value;
                this.GameModel.Player1.Point = new Point(double.Parse(xdoc.Element("GameModel").Element("Player1").Element("Point").Element("X").Value), double.Parse(xdoc.Element("GameModel").Element("Player1").Element("Point").Element("Y").Value));
                this.GameModel.Player1.NumberOfWins = int.Parse(xdoc.Element("GameModel").Element("Player1").Element("NumberOfWins").Value);
                this.GameModel.Player1.NumberOfTurbos = int.Parse(xdoc.Element("GameModel").Element("Player1").Element("NumberOfTurbos").Value);

                this.GameModel.Player2.Name = xdoc.Element("GameModel").Element("Player2").Element("Name").Value;
                this.GameModel.Player2.Point = new Point(double.Parse(xdoc.Element("GameModel").Element("Player2").Element("Point").Element("X").Value), double.Parse(xdoc.Element("GameModel").Element("Player2").Element("Point").Element("Y").Value));
                this.GameModel.Player2.NumberOfWins = int.Parse(xdoc.Element("GameModel").Element("Player2").Element("NumberOfWins").Value);
                this.GameModel.Player2.NumberOfTurbos = int.Parse(xdoc.Element("GameModel").Element("Player2").Element("NumberOfTurbos").Value);

                var obstacles = from e in xdoc.Descendants("Obstacles").Elements("ObstacleObject").Elements("Point")
                        select new { X = e.Element("X").Value, Y = e.Element("Y").Value };

                var turbos = from e in xdoc.Descendants("Turbos").Elements("TurboObject").Elements("Point")
                                select new { X = e.Element("X").Value, Y = e.Element("Y").Value };

                this.GameModel.Obstacles.Clear();
                this.GameModel.Turbos.Clear();

                foreach (var item in obstacles)
                {
                    this.GameModel.Obstacles.Add(new ObstacleObject() { Point = new Point(double.Parse(item.X), double.Parse(item.Y)) });
                }

                foreach (var item in turbos)
                {
                    this.GameModel.Turbos.Add(new TurboObject() { Point = new Point(double.Parse(item.X), double.Parse(item.Y)) });
                }
            }
        }

        private void TestGame()
        {
            this.NewGame();
            this.GetPlayersNames();
            this.NewRound();
            this.StartBackgroundSong();

            this.GameModel.Player1.NumberOfTurbos = 2;

            this.MovePlayers();
        }

        /// <summary>
        /// Kill the player
        /// </summary>
        /// <param name="player">Playerobject</param>
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

        /// <summary>
        /// Player wins the round
        /// </summary>
        /// <param name="player">Player object</param>
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

        /// <summary>
        /// End the game
        /// </summary>
        private void EndGame()
        {
            this.IsGamePaused = true;
            this.IsGameEnded = true;
            this.HighScoreCheck();
            this.sw.Stop();
        }

        /// <summary>
        /// Check the highscore
        /// </summary>
        private void HighScoreCheck()
        {
            if (this.GameModel.HighScore.Player1Score < this.GameModel.Player1.NumberOfWins || this.GameModel.HighScore.Player2Score < this.GameModel.Player1.NumberOfWins)
            {
                this.GameModel.HighScore = new HighScore() { DateTime = DateTime.Now, Player1Name = this.GameModel.Player1.Name, Player2Name = this.GameModel.Player2.Name, Player1Score = this.GameModel.Player1.NumberOfWins, Player2Score = this.GameModel.Player2.NumberOfWins };
            }
        }

        /// <summary>
        /// Reset players win and turbos number
        /// </summary>
        /// <param name="player">Player object</param>
        private void ResetPlayer(Player player)
        {
            player.NumberOfTurbos = 0;
            player.NumberOfWins = 0;
        }

        /// <summary>
        /// Generate obstacles
        /// </summary>
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

        /// <summary>
        /// Genereate turbos
        /// </summary>
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

        /// <summary>
        /// Generate obstacles by number
        /// </summary>
        /// <param name="num">Number of genarated obstacles</param>
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

        /// <summary>
        /// Generate turbos
        /// </summary>
        /// <param name="num">Number of turbos generated</param>
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

        /// <summary>
        /// Set players start position
        /// </summary>
        /// <param name="player">Player object</param>
        private void SetPlayerStartPositon(Player player)
        {
            int posX = rnd.Next(10, 40);
            int posY = rnd.Next(7, 23);
            while (this.GameModel.GameField[posY, posX] != 0)
            {
                posX = rnd.Next(10, 40);
                posY = rnd.Next(7, 23);
            }

            player.Point = new Point(posX, posY);

            if (player == this.GameModel.Player1)
            {
                this.GameModel.GameField[posY, posX] = 1;
            }
            else
            {
                this.GameModel.GameField[posY, posX] = 2;
            }
        }

        /// <summary>
        /// Start background music
        /// </summary>
        public void StartBackgroundSong()
        {
            this.IsMusicEnabled = true;
            //var xml = XDocument.Load(@"../../../TronGame.Repository/XMLs/settings.xml");
            //var music = xml.Root.Element("music").Value;
            //if (music == "1")
            //{
            //    this.backgroundMediaPlayer.Play();
            //}
        }

        /// <summary>
        /// Enale background music
        /// </summary>
        public void EnableBackgroundMusic()
        {
            this.IsMusicEnabled = true;
            this.backgroundMediaPlayer.Play();
            //var xml = XDocument.Load(@"../../../TronGame.Repository/XMLs/settings.xml");
            //xml.Root.SetElementValue("music", 1);
            //xml.Save(@"../../../TronGame.Repository/XMLs/settings.xml");
        }

        /// <summary>
        /// Disable background music
        /// </summary>
        public void DisableBackgroundMusic()
        {
            this.IsMusicEnabled = false;
            this.backgroundMediaPlayer.Stop();
            //var xml = XDocument.Load(@"../../../TronGame.Repository/XMLs/settings.xml");
            //xml.Root.SetElementValue("music", 0);
            //xml.Save(@"../../../TronGame.Repository/XMLs/settings.xml");
        }
    }
}
