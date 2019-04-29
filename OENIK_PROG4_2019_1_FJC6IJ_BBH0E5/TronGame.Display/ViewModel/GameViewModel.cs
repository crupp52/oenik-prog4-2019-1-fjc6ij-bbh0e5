﻿namespace TronGame.Display
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.CommandWpf;
    using TronGame.Model;

    /// <summary>
    /// GameViewModel viewmodel
    /// </summary>
    public class GameViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameViewModel"/> class.
        /// </summary>
        public GameViewModel()
        {
            this.GameControl = new GameControl();
            this.GameModel = new GameModel();

            this.ExitGameCommand = new RelayCommand(() => { Application.Current.Shutdown(); });
        }

        public GameControl GameControl { get; set; }

        public GameModel GameModel { get; set; }

        public ICommand ExitGameCommand { get; private set; }
    }
}