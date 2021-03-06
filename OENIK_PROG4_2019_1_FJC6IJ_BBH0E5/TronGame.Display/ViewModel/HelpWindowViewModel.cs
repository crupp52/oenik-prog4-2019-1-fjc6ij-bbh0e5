﻿using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TronGame.Display
{
    /// <summary>
    /// Help window class
    /// </summary>
    public class HelpWindowViewModel : ViewModelBase
    {
        public string HelpDescription { get; set; }

        public string GameGoalDescription { get; set; }

        public string StartGameDescription { get; set; }

        public string DuringGameDescription { get; set; }

        public string EndGameDescription { get; set; }

        public HelpWindowViewModel()
        {
            this.HelpDescription = GetHelpDescription();
            this.GameGoalDescription = GetGameGoalDescription();
            this.StartGameDescription = GetStartGameDescription();
            this.DuringGameDescription = GetDuringGameDescription();
            this.EndGameDescription = GetEndGameDescription();
        }

        /// <summary>
        /// Get a part of helpwindow description
        /// </summary>
        /// <returns>Help description</returns>
        private string GetHelpDescription()
        {
            return "This is a 2D game based on the game seen from the TRON moovie.\nWhile players are moving, they draw a line behind them.\nIf somebody runs into the other players drawed line he/she dies...Horribly.. :(";
        }

        /// <summary>
        /// Get a part of helpwindow description
        /// </summary>
        /// <returns>Help description</returns>
        private string GetGameGoalDescription()
        {
            return "The goal is: Be the first who wins 5 games! (Be the last who survives!)\nHint: be tricky where you draw your lines!";
        }

        /// <summary>
        /// Get a part of helpwindow description
        /// </summary>
        /// <returns>Help description</returns>
        private string GetStartGameDescription()
        {
            return "Both player start from a random position of the map.\nBetween small games scores will refresh.";
        }

        /// <summary>
        /// Get a part of helpwindow description
        /// </summary>
        /// <returns>Help description</returns>
        private string GetDuringGameDescription()
        {
            return "You can move either by WASD or the arrow keys.\nBe careful! Speedups and obstacles can appear anywhere on the map!\nSpeedups will give you speed boost while obstacles will kill you!";
        }

        /// <summary>
        /// Get a part of helpwindow description
        /// </summary>
        /// <returns>Help description</returns>
        private string GetEndGameDescription()
        {
            return "The game ends when a player wins 5 small games.\nYou can die if you pick up an obstacle or if you run into your enemy line.\nAt the end of the game the highscore gets updated if neccessery.";
        }
    }
}
