namespace TronGame.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IRepository
    {
        /// <summary>
        /// Returns with a GameObject array what contains the game field.
        /// </summary>
        /// <returns>GameField GameObject array</returns>
        GameObject[,] GetGameField();

        /// <summary>
        /// Retruns with a selected player.
        /// </summary>
        /// <param name="numOfPlayer">Number of the player (1 or 2)</param>
        /// <returns>Player object</returns>
        Player GetPlayer(int numOfPlayer);

        /// <summary>
        /// Return with the selected difficulty of current match.
        /// </summary>
        /// <returns>Difficulty of the game.</returns>
        Difficulty GetDifficulty();

        /// <summary>
        /// Returns with the Highest Score ever.
        /// </summary>
        /// <returns>Highest Score ever.</returns>
        int GetHighScore();

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
        /// Set a new highscore.
        /// </summary>
        /// <param name="playerOneName"></param>
        /// <param name="playerTwoName"></param>
        /// <param name="firstPlayerScore"></param>
        /// <param name="secondPlayersScore"></param>
        /// <param name="time"></param>
        void SetHighScore(string playerOneName, string playerTwoName, int firstPlayerScore, int secondPlayersScore, double time);

        /// <summary>
        /// Set name a selected Player object.
        /// </summary>
        /// <param name="numOfPlayer">Player ID</param>
        /// <param name="name">Name of Player</param>
        void SetPlayerName(int numOfPlayer, string name);

        void SetNewObjectOnField(ObjectType objectType, GameObject item);

        void ResetGameField();

        void ResetPlayers();


        /// <summary>
        /// Creates an game state to XML file.
        /// </summary>
        void SaveGamestate();

        /// <summary>
        /// Loads the selected game state from XML file.
        /// </summary>
        /// <param name="filename">Selected file name.</param>
        void LoadGamestate(string filename);
    }
}
