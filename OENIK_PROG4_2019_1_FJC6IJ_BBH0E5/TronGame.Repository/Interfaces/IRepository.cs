namespace TronGame.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRepository
    {
        Player Player1 { get; }

        Player Player2 { get; }

        List<ObstacleObject> Obstacles { get; }

        List<TurboObject> Turbos { get; }

        Difficulty Difficulty { get; }

        GameObject[,] GameField { get; }

        HighScore HighScore { get; }

        /// <summary>
        /// Set volume value for ingame music.
        /// </summary>
        /// <param name="value"></param>
        void SetVolume(double value);

        /// <summary>
        /// Set difficulty for the game.
        /// </summary>
        /// <param name="difficulty"></param>
        /// <returns></returns>
        void SetDifficulty(int difficulty);

        /// <summary>
        /// Set name a selected Player object.
        /// </summary>
        /// <param name="numOfPlayer">Player ID</param>
        /// <param name="name">Name of Player</param>
        void SetPlayerName(int numOfPlayer, string name);

        void SetNewObjectOnField(ObjectType objectType, GameObject item);

        void ResetGameField();

        void ResetPlayers();
    }
}
