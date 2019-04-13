namespace TronGame.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRepository
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
