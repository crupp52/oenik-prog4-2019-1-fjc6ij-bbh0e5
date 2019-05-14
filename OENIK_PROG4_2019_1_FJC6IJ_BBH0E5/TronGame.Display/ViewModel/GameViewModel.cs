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
    using Microsoft.Win32;
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

            this.IsMusicEnabled = true;
            this.ExitGameCommand = new RelayCommand(() => { Application.Current.Shutdown(); });
            this.ShowHighScoreCommand = new RelayCommand(() => { MessageBox.Show(this.GameControl.GameModel.HighScore.GetFullDescription()); });
            this.ShowHelpWindowCommand = new RelayCommand(() => { new HelpWindow().Show(); });
            this.SetDifficultyToEasyCommand = new RelayCommand(() => { this.SetDifficulty(0); });
            this.SetDifficultyToMediumCommand = new RelayCommand(() => { this.SetDifficulty(1); });
            this.SetDifficultyToHardCommand = new RelayCommand(() => { this.SetDifficulty(2); });
            this.EnableDisableMusicCommand = new RelayCommand(() => { this.ChangeMusicState(); });
            this.ChangePlayersNameCommand = new RelayCommand(() => { new ChangePlayersNameWindow(this.GameControl).Show(); });
            this.PauseGameCommand = new RelayCommand(() =>
            {
                this.PauseGame();
                var pauseWindow = new PauseWindow();
                pauseWindow.Closing += this.ContinueGame;
                pauseWindow.Show();
            });

            this.NewGameCommand = new RelayCommand(() => this.GameControl.NewGame());
            this.LoadGameCommand = new RelayCommand(() =>
            {
                this.GameControl.PauseGame();
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "XML files (*.xml)|*.xml";
                if (openFileDialog.ShowDialog() == true)
                {
                    this.GameControl.LoadGame(openFileDialog.FileName);
                }
                this.GameControl.ContinueGame();
            });
        }

        /// <summary>
        /// Contibue game
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event</param>
        private void ContinueGame(object sender, EventArgs e)
        {
            this.GameControl.ContinueGame();
        }

        /// <summary>
        /// Pause game
        /// </summary>
        private void PauseGame()
        {
            this.GameControl.PauseGame();
        }

        /// <summary>
        /// Gamecontrol
        /// </summary>
        public GameControl GameControl { get; set; }

        public ICommand NewGameCommand { get; private set; }

        public ICommand LoadGameCommand { get; private set; }

        public ICommand ExitGameCommand { get; private set; }

        public ICommand ShowHighScoreCommand { get; private set; }

        public ICommand ShowHelpWindowCommand { get; private set; }

        public ICommand SetDifficultyToEasyCommand { get; private set; }

        public ICommand SetDifficultyToMediumCommand { get; private set; }

        public ICommand SetDifficultyToHardCommand { get; private set; }

        public ICommand EnableDisableMusicCommand { get; private set; }

        public ICommand ChangePlayersNameCommand { get; private set; }

        public ICommand PauseGameCommand { get; private set; }

        /// <summary>
        /// Set difficulty
        /// </summary>
        /// <param name="diff">difficulty level</param>
        private void SetDifficulty(int diff)
        {
            this.GameControl.ChangeDifficulty((Repository.Difficulty)diff);
        }

        /// <summary>
        /// Is the music enabled property
        /// </summary>
        public bool IsMusicEnabled { get; set; }

        /// <summary>
        /// Change the music state (enable/disable)
        /// </summary>
        private void ChangeMusicState()
        {
            if (!this.IsMusicEnabled) { this.GameControl.DisableMusic(); }
            else { this.GameControl.EnableMusic(); }
        }


        public string GameTime { get { return DateTime.Now.ToString("yyyy-MM-dd"); } }
    }
}
