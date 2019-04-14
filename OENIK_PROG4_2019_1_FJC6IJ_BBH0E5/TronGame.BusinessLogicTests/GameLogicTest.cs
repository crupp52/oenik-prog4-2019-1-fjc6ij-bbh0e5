namespace TronGame.BusinessLogicTests
{
    using System;
    using System.Collections.Generic;
    using Moq;
    using NUnit.Framework;
    using TronGame.BusinessLogic;
    using TronGame.Repository;

    [TestFixture]
    public class GameLogicTest
    {
        //private Mock<IRepository> mock;
        //private IBusinessLogic logic;

        //public static IEnumerable<TestCaseData> PlayerList
        //{
        //    get
        //    {
        //        yield return new TestCaseData(new[]
        //        {
        //            new Player() { Name = "Teszt Béla", PosX = 30, PosY = 50, NumberOfTurbos = 2, NumberOfWins = 4 }
        //        });
        //        yield return new TestCaseData(new[]
        //        {
        //            new Player() { Name = "Teszt Elek", PosX = 73, PosY = 10, NumberOfTurbos = 3, NumberOfWins = 1 }
        //    });
        //    }
        //}

        //[SetUp]
        //public void Setup()
        //{
        //    this.mock = new Mock<IRepository>();

        //    Player player1 = new Player() { Name = "Teszt Béla", PosX = 30, PosY = 50, NumberOfTurbos = 2, NumberOfWins = 4 };
        //    Player player2 = new Player() { Name = "Teszt Elek", PosX = 73, PosY = 10, NumberOfTurbos = 3, NumberOfWins = 1 };

        //    List<ObstacleObject> obstacles = new List<ObstacleObject>()
        //    {
        //        new ObstacleObject() { PosX = 61, PosY = 48 },
        //        new ObstacleObject() { PosX = 60, PosY = 63 },
        //        new ObstacleObject() { PosX = 55, PosY = 48 },
        //        new ObstacleObject() { PosX = 41, PosY = 3 },
        //        new ObstacleObject() { PosX = 49, PosY = 95 },
        //    };

        //    List<TurboObject> turbos = new List<TurboObject>()
        //    {
        //        new TurboObject() { PosX = 14, PosY = 53 },
        //        new TurboObject() { PosX = 1, PosY = 7 },
        //        new TurboObject() { PosX = 19, PosY = 65 },
        //        new TurboObject() { PosX = 71, PosY = 21 },
        //    };

        //    HighScore highScore = new HighScore() { Score = 3214, PlayerName = "Teszt József", DateTime = DateTime.Now };

        //    GameObject[,] gameField = new GameObject[100, 100];

        //    this.mock.Setup(x => x.Player1).Returns(player1);
        //    this.mock.Setup(x => x.Player2).Returns(player2);
        //    this.mock.Setup(x => x.Obstacles).Returns(obstacles);
        //    this.mock.Setup(x => x.Turbos).Returns(turbos);
        //    this.mock.Setup(x => x.HighScore).Returns(highScore);
        //    this.mock.Setup(x => x.GameField).Returns(gameField);

        //    this.logic = new GameLogic(this.mock.Object);
        //}

        //[Test]
        //public void NotEmptyLogic()
        //{
        //    Assert.That(this.logic, Is.Not.Null);
        //}

        //[Test]
        //[TestCase(Difficulty.Easy)]
        //[TestCase(Difficulty.Medium)]
        //[TestCase(Difficulty.Hard)]
        //public void StartNewGameTest(Difficulty difficulty)
        //{
        //    this.logic.GameRepository.Difficulty = difficulty;

        //    Assert.That(this.logic.GameRepository.Obstacles.Count, Is.Not.EqualTo(0));
        //    Assert.That(this.logic.GameRepository.Turbos.Count, Is.Not.EqualTo(0));
        //}

        //[Test]
        //[TestCase(Difficulty.Easy)]
        //// [TestCase(Difficulty.Medium)]
        //// [TestCase(Difficulty.Hard)]
        //public void StartNewGameWithDifficultyChange(Difficulty difficulty)
        //{
        //    this.logic.GameRepository.Difficulty = difficulty;

        //    if (difficulty == Difficulty.Easy)
        //    {
        //        Assert.That(this.logic.GameRepository.Obstacles.Count, Is.EqualTo(3));
        //        Assert.That(this.logic.GameRepository.Turbos.Count, Is.EqualTo(7));
        //    }
        //    else if (difficulty == Difficulty.Medium)
        //    {
        //        Assert.That(this.logic.GameRepository.Obstacles.Count, Is.EqualTo(5));
        //        Assert.That(this.logic.GameRepository.Turbos.Count, Is.EqualTo(5));
        //    }
        //    else
        //    {
        //        Assert.That(this.logic.GameRepository.Obstacles.Count, Is.EqualTo(7));
        //        Assert.That(this.logic.GameRepository.Turbos.Count, Is.EqualTo(3));
        //    }
        //}

        //[Test]
        //[TestCaseSource(nameof(PlayerList))]
        //[Sequential]
        //public void UseTurboTest(Player player)
        //{
        //    int numOfTurbos = player.NumberOfTurbos;

        //    this.logic.UseTurbo(player);

        //    Assert.That(player.NumberOfTurbos, Is.LessThan(numOfTurbos));
        //}
    }
}
