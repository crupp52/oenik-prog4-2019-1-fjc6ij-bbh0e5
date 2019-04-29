namespace TronGame.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;

    /// <summary>
    /// High score object
    /// </summary>
    public class HighScore
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HighScore"/> class.
        /// </summary>
        public HighScore()
        {
            this.Player1Score = 0;
            this.Player2Score = 0;
            this.Player1Name = "Béla";
            this.Player2Name = "Józsi";
            this.DateTime = DateTime.Now;

            this.LoadHighScore();
        }

        /// <summary>
        /// Gets or sets score of Player1
        /// </summary>
        public int Player1Score { get; set; }

        /// <summary>
        /// Gets or sets score of Player2
        /// </summary>
        public int Player2Score { get; set; }

        /// <summary>
        /// Gets or sets name of Player1
        /// </summary>
        public string Player1Name { get; set; }

        /// <summary>
        /// Gets or sets name of Player2
        /// </summary>
        public string Player2Name { get; set; }

        /// <summary>
        /// Gets or sets DateTime
        /// </summary>
        public DateTime DateTime { get; set; }

        public void LoadHighScore()
        {
            var xml = XDocument.Load(@"../../../TronGame.Repository/XMLs/settings.xml");
            var highscore = xml.Root.Element("highscore");
            this.Player1Name = highscore.Element("player1name").Value;
            this.Player2Name = highscore.Element("player2name").Value;
            this.Player1Score = int.Parse(highscore.Element("player1score").Value);
            this.Player2Score = int.Parse(highscore.Element("player2score").Value);
            this.DateTime = DateTime.Parse(highscore.Element("time").Value);
        }

        public override string ToString()
        {
            return $"{Player1Name} - {Player2Name}  {Player1Score}:{Player2Score}";
        }
    }
}
