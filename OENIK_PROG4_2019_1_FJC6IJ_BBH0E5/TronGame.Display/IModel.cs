namespace TronGame.Display
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TronGame.Repository;

    public interface IModel
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
