namespace TronGame.BusinessLogicTests
{
    using System;
    using System.Collections.Generic;
    using Moq;
    using NUnit.Framework;
    using TronGame.BusinessLogic;
    using TronGame.Model;
    using TronGame.Repository;

    /// <summary>
    /// GameLogic test class
    /// </summary>
    [TestFixture]
    public class GameLogicTest
    {
        private Mock<IGameModel> mock;
        private IBusinessLogic logic;

        /// <summary>
        /// Gets yield return Players
        /// </summary>
        public static IEnumerable<TestCaseData> PlayerList
        {
            get
            {
                yield return new TestCaseData(new[]
                {
                    new Player() { Name = "Teszt Béla", NumberOfTurbos = 2, NumberOfWins = 4 }
                });
                yield return new TestCaseData(new[]
                {
                    new Player() { Name = "Teszt Elek", NumberOfTurbos = 3, NumberOfWins = 1 }
                });
            }
        }

        /// <summary>
        /// Setup the mocked GameModel object
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.mock = new Mock<IGameModel>();

            Player player1 = new Player() { Name = "Teszt Béla", NumberOfTurbos = 2, NumberOfWins = 4 };
            Player player2 = new Player() { Name = "Teszt Elek", NumberOfTurbos = 3, NumberOfWins = 1 };

            //List<ObstacleObject> obstacles = new List<ObstacleObject>()
            //{
            //    new ObstacleObject() { PosX = 61, PosY = 48 },
            //    new ObstacleObject() { PosX = 60, PosY = 63 },
            //    new ObstacleObject() { PosX = 55, PosY = 48 },
            //    new ObstacleObject() { PosX = 41, PosY = 3 },
            //    new ObstacleObject() { PosX = 49, PosY = 95 },
            //};

            //List<TurboObject> turbos = new List<TurboObject>()
            //{
            //    new TurboObject() { PosX = 14, PosY = 53 },
            //    new TurboObject() { PosX = 1, PosY = 7 },
            //    new TurboObject() { PosX = 19, PosY = 65 },
            //    new TurboObject() { PosX = 71, PosY = 21 },
            //};

            HighScore highScore = new HighScore() { Player1Score = 3, Player2Score = 2, Player1Name = "Teszt Elek", Player2Name = "Bem József", DateTime = DateTime.Now };

            int[,] gameField = new int[500, 1000];

            this.mock.Setup(x => x.Player1).Returns(player1);
            this.mock.Setup(x => x.Player2).Returns(player2);
            //this.mock.Setup(x => x.Obstacles).Returns(obstacles);
            //this.mock.Setup(x => x.Turbos).Returns(turbos);
            this.mock.Setup(x => x.HighScore).Returns(highScore);
            this.mock.Setup(x => x.GameField).Returns(gameField);

            this.logic = new GameLogic(this.mock.Object);
        }

        /// <summary>
        /// After setup, the GameLogic instance is not null
        /// </summary>
        [Test]
        public void NotEmptyLogic()
        {
            Assert.That(this.logic, Is.Not.Null);
        }

        /// <summary>
        /// Test of generate new obstacle and turbos objects
        /// </summary>
        /// <param name="difficulty">Difficulty of game</param>
        [Test]
        [TestCase(Difficulty.Easy)]
        [TestCase(Difficulty.Medium)]
        [TestCase(Difficulty.Hard)]
        public void StartNewGameTest(Difficulty difficulty)
        {
            this.logic.GameModel.Difficulty = difficulty;

            Assert.That(this.logic.GameModel.Obstacles.Count, Is.Not.Zero);
            Assert.That(this.logic.GameModel.Turbos.Count, Is.Not.Zero);
        }

        /// <summary>
        /// Change game difficulty
        /// </summary>
        /// <param name="difficulty">Difficulty of game</param>
        [Test]
        [TestCase(Difficulty.Easy)]
        [TestCase(Difficulty.Medium)]
        [TestCase(Difficulty.Hard)]
        public void StartNewGameWithDifficultyChange(Difficulty difficulty)
        {
            this.logic.GameModel.Difficulty = difficulty;

            if (difficulty == Difficulty.Easy)
            {
                Assert.That(this.logic.GameModel.Obstacles.Count, Is.EqualTo(3));
                Assert.That(this.logic.GameModel.Turbos.Count, Is.EqualTo(7));
            }
            else if (difficulty == Difficulty.Medium)
            {
                Assert.That(this.logic.GameModel.Obstacles.Count, Is.EqualTo(5));
                Assert.That(this.logic.GameModel.Turbos.Count, Is.EqualTo(5));
            }
            else
            {
                Assert.That(this.logic.GameModel.Obstacles.Count, Is.EqualTo(7));
                Assert.That(this.logic.GameModel.Turbos.Count, Is.EqualTo(3));
            }
        }

        /// <summary>
        /// Test of UseTurbo method
        /// </summary>
        /// <param name="player">Player instance</param>
        [Test]
        [TestCaseSource(nameof(PlayerList))]
        [Sequential]
        public void UseTurboTest(Player player)
        {
            int numOfTurbos = player.NumberOfTurbos;

            this.logic.UseTurbo(player);

            Assert.That(player.NumberOfTurbos, Is.LessThan(numOfTurbos));
        }

        /// <summary>
        /// Test of AddNameToPlayers
        /// </summary>
        /// <param name="name1">Name of the Player1</param>
        /// <param name="name2">Name of the Player2</param>
        [TestCase("Karcsi", "Béla")]
        [TestCase("Kata", "Ágnes")]
        public void SetPlayersNameTest(string name1, string name2)
        {
            this.logic.AddNameToPlayers(name1, name2);

            Assert.That(name1, Is.EqualTo(this.logic.GameModel.Player1.Name));
            Assert.That(name2, Is.EqualTo(this.logic.GameModel.Player2.Name));
        }

        /// <summary>
        /// Test of the MovePlayer method
        /// </summary>
        /// <param name="direction">Direction of the move</param>
        [TestCase(MovingDirection.Up)]
        [TestCase(MovingDirection.Down)]
        [TestCase(MovingDirection.Left)]
        [TestCase(MovingDirection.Rigth)]
        public void MovePlayerDirectionTest(MovingDirection direction)
        {
            if (direction == MovingDirection.Up || direction == MovingDirection.Down)
            {
                double pos = this.logic.GameModel.Player1.Point.Y;

                this.logic.MovePlayer(this.logic.GameModel.Player1, direction);

                Assert.That(pos, Is.Not.EqualTo(this.logic.GameModel.Player1.Point.Y));
            }
            else
            {
                double pos = this.logic.GameModel.Player1.Point.X;

                this.logic.MovePlayer(this.logic.GameModel.Player1, direction);

                Assert.That(pos, Is.Not.EqualTo(this.logic.GameModel.Player1.Point.X));
            }
        }
    }
}
