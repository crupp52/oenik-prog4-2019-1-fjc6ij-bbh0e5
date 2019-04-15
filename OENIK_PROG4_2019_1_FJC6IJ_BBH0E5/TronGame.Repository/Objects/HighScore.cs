namespace TronGame.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class HighScore
    {
        public HighScore()
        {
            this.Player1Score = 0;
            this.Player2Score = 0;
            this.Player1Name = "Béla";
            this.Player2Name = "Józsi";
            this.DateTime = DateTime.Now;
        }

        public int Player1Score { get; set; }

        public int Player2Score { get; set; }

        public string Player1Name { get; set; }

        public string Player2Name { get; set; }

        public DateTime DateTime { get; set; }
    }
}
