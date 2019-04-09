namespace TronGame.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRepository
    {
        GameObject[,] GetGameField();

        Player GetPlayer(int numOfPlayer);

        Difficulty GetDifficulty();

        int GetHighScore(int score);

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
        /// Sets the name of the players.
        /// </summary>
        /// <param name="playerOneName"></param>
        /// <param name="playerTwoName"></param>
        /// <returns></returns>
        void SetPlayersName(string playerOneName, string playerTwoName);

        /// <summary>
        /// Set a new highscore.
        /// </summary>
        /// <param name="playerOneName"></param>
        /// <param name="playerTwoName"></param>
        /// <param name="firstPlayerScore"></param>
        /// <param name="secondPlayersScore"></param>
        /// <param name="time"></param>
        void SetHighScore(string playerOneName, string playerTwoName, int firstPlayerScore, int secondPlayersScore, double time);
    }
}
