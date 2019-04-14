namespace TronGame.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TronGame.Model;
    using TronGame.Repository;

    public interface IBusinessLogic
    {
        event EventHandler ScreenRefresh;

        /// <summary>
        /// Sets the names of the Player1 and Player2.
        /// </summary>
        /// <param name="player1Name">Name of the Player1.</param>
        /// <param name="player2Name">Name of the Player2.</param>
        void AddNameToPlayers(string player1Name, string player2Name);

        /// <summary>
        /// Start timer, resets GameField, Players, gererates new Obstacles and Turbos.
        /// </summary>
        void NewGame();

        /// <summary>
        /// Resets GameField, Players positons and genetrates new Obstacles and Turbos.
        /// </summary>
        void NewRound();

        /// <summary>
        /// Stops timer, resets GameField, Players, gererates new Obstacles and Turbos.
        /// </summary>
        void EndGame();

        /// <summary>
        /// Moves the selected player to selected direction.
        /// </summary>
        /// <param name="player">Selected Player object.</param>
        /// <param name="direction">Selected MovingDirection enum.</param>
        void MovePlayer(Player player, MovingDirection direction);

        /// <summary>
        /// Pick up the selected object with selected Player.
        /// </summary>
        /// <param name="player">Selected Player object.</param>
        /// <param name="objectType">Selected object type.</param>
        void PickUp(Player player, ObjectType objectType);

        /// <summary>
        /// Creates an game state to XML file.
        /// </summary>
        void SaveGameState();

        /// <summary>
        /// Loads the selected game state from XML file.
        /// </summary>
        /// <param name="filename">Selected file name.</param>
        void LoadGameState(string filename);
    }
}
