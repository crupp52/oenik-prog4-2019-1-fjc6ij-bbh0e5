namespace TronGame.Model
{
    using System.Collections.Generic;
    using System.Windows.Input;
    using TronGame.Repository;

    /// <summary>
    /// Interface of GameModel object
    /// </summary>
    public interface IGameModel
    {
        /// <summary>
        /// Gets or sets Player1
        /// </summary>
        Player Player1 { get; set; }

        /// <summary>
        /// Gets or sets Player2
        /// </summary>
        Player Player2 { get; set; }

        /// <summary>
        /// Gets or sets Obstacles
        /// </summary>
        List<ObstacleObject> Obstacles { get; set; }

        /// <summary>
        /// Gets or sets Turbos
        /// </summary>
        List<TurboObject> Turbos { get; set; }

        /// <summary>
        /// Gets or sets Difficulty
        /// </summary>
        Difficulty Difficulty { get; set; }

        /// <summary>
        /// Gets or sets GameField
        /// </summary>
        GameObject[,] GameField { get; set; }

        /// <summary>
        /// Gets or sets HighScore
        /// </summary>
        HighScore HighScore { get; set; }
    }
}
