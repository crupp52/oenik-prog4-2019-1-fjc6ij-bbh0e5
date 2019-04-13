namespace TronGame.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public enum MovingDirection
    {
        Up, Down, Left, Rigth
    }

    public class Player : GameObject
    {
        private int step = 5;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="name">Name of the player</param>
        public Player()
        {
            this.NumberOfWins = 0;
            this.NumberOfTurbos = 0;
        }

        public string Name { get; set; }

        public int NumberOfWins { get; set; }

        public int NumberOfTurbos { get; set; }

        public void Move(MovingDirection direction)
        {
            switch (direction)
            {
                case MovingDirection.Up:
                    this.PosY -= this.step;
                    break;
                case MovingDirection.Down:
                    this.PosY += this.step;
                    break;
                case MovingDirection.Left:
                    this.PosX -= this.step;
                    break;
                case MovingDirection.Rigth:
                    this.PosX += this.step;
                    break;
            }
        }
    }
}
