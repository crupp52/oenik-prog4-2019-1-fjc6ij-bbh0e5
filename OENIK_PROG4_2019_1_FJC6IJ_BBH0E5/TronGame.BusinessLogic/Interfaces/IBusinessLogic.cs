﻿namespace TronGame.BusinessLogic
{
    using System;
    using TronGame.Model;
    using TronGame.Repository;

    /// <summary>
    /// Interface of the GameLogic
    /// </summary>
    public interface IBusinessLogic
    {
        /// <summary>
        /// ScreenRefresh eventhandler
        /// </summary>
        event EventHandler ScreenRefresh;

        bool IsGamePaused { get; }

        bool IsGameEnded { get; }

        bool IsMusicEnabled { get; }

        /// <summary>
        /// Gets GameModel
        /// </summary>
        IGameModel GameModel { get; }

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
        /// Moves the selected player to selected direction.
        /// </summary>
        /// <param name="player">Selected Player object.</param>
        /// <param name="direction">Selected MovingDirection enum.</param>
        void MovePlayer(Player player, MovingDirection direction);

        void ChangeDifficulty(Difficulty difficulty);

        /// <summary>
        /// Player speed is double until 10 sec.
        /// </summary>
        /// <param name="player">Selected player.</param>
        void UseTurbo(Player player);

        /// <summary>
        /// Creates an game state to XML file.
        /// </summary>
        void SaveGameState();

        /// <summary>
        /// Loads the selected game state from XML file.
        /// </summary>
        /// <param name="filename">Selected file name.</param>
        void LoadGameState(string filename);

        /// <summary>
        /// Start teh background song at the start of the game.
        /// </summary>
        void StartBackgroundSong();

        /// <summary>
        /// Enables the background music.
        /// </summary>
        void EnableBackgroundMusic();

        /// <summary>
        /// Disables the background music.
        /// </summary>
        void DisableBackgroundMusic();

        /// <summary>
        /// Pauses the game.
        /// </summary>
        void PauseGame();

        /// <summary>
        /// Continues the game (stops the pause effect).
        /// </summary>
        void ContinueGame();
    }
}
