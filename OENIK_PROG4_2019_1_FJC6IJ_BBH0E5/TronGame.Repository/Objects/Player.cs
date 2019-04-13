namespace TronGame.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Player : GameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="name">Name of the player</param>
        /// <param name="posX">Positions on X dimention.</param>
        /// <param name="posY">Positions on Y dimention.</param>
        public Player()
        {
            this.NumberOfWins = 0;
            this.NumberOfTurbos = 0;
        }

        public string Name { get; set; }

        public int NumberOfWins { get; set; }

        public int NumberOfTurbos { get; set; }
    }
}
