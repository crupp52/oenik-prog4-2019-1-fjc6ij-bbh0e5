namespace TronGame.Display
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;
    using System.Xml.Linq;
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

            IsMusicEnabled = true;
            // commands
            this.ExitGameCommand = new RelayCommand(() => { Application.Current.Shutdown(); });
            this.ShowHighScoreCommand = new RelayCommand(() => { MessageBox.Show(this.GameControl.GameModel.HighScore.GetFullDescription()); });
            this.ShowHelpWindowCommand = new RelayCommand(() => { new HelpWindow().Show(); });
            this.SetDifficultyToEasyCommand = new RelayCommand(() => { this.SetDifficulty(1); });
            this.SetDifficultyToMediumCommand = new RelayCommand(() => { this.SetDifficulty(2); });
            this.SetDifficultyToHardCommand = new RelayCommand(() => { this.SetDifficulty(3); });
            this.EnableDisableMusicCommand = new RelayCommand(() => { this.ChangeMusicState(); });
            this.ChangePlayersNameCommand = new RelayCommand(() => { new ChangePlayersNameWindow(GameControl).Show(); });
        }

        public GameControl GameControl { get; set; }

        public ICommand ExitGameCommand { get; private set; }

        public ICommand ShowHighScoreCommand { get; private set; }

        public ICommand ShowHelpWindowCommand { get; private set; }

        public ICommand SetDifficultyToEasyCommand { get; private set; }

        public ICommand SetDifficultyToMediumCommand { get; private set; }

        public ICommand SetDifficultyToHardCommand { get; private set; }

        public ICommand EnableDisableMusicCommand { get; private set; }

        public ICommand ChangePlayersNameCommand { get; private set; }

        private void SetDifficulty(int diff)
        {
            var xml = XDocument.Load(@"../../../TronGame.Repository/XMLs/settings.xml");
            xml.Root.SetElementValue("difficulty", diff);
            xml.Save(@"../../../TronGame.Repository/XMLs/settings.xml");
            MessageBox.Show("Difficulty was modified!");
        }

        public bool IsMusicEnabled { get; set; }

        private void ChangeMusicState()
        {
            if (!this.IsMusicEnabled) { this.GameControl.DisableMusic(); }
            else { this.GameControl.EnableMusic(); }
        }
    }
}
