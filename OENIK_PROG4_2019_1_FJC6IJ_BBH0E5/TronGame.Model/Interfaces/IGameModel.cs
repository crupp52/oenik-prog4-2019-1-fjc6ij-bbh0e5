namespace TronGame.Model
{
    using System.Collections.Generic;
    using TronGame.Repository;

    public interface IGameModel
    {
        Player Player1 { get; set; }

        Player Player2 { get; set; }

        List<ObstacleObject> Obstacles { get; set; }

        List<TurboObject> Turbos { get; set; }

        Difficulty Difficulty { get; set; }

        GameObject[,] GameField { get; set; }

        HighScore HighScore { get; set; }
    }
}
