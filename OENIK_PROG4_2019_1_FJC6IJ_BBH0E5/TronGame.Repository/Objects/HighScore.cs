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
            this.Score = 3000;
            this.PlayerName = "Béla";
            this.DateTime = DateTime.Now;
        }

        public int Score { get; set; }

        public string PlayerName { get; set; }

        public DateTime DateTime { get; set; }
    }
}
