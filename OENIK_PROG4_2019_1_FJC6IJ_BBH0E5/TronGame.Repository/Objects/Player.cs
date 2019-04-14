namespace TronGame.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public enum MovingDirection
    {
        Up, Down, Left, Rigth
    }

    public class Player : GameObject
    {
        private Stopwatch stopwatch;
        private int speed = 5;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="name">Name of the player</param>
        public Player()
        {
            this.stopwatch = new Stopwatch();

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
                    this.PosY -= this.speed;
                    break;
                case MovingDirection.Down:
                    this.PosY += this.speed;
                    break;
                case MovingDirection.Left:
                    this.PosX -= this.speed;
                    break;
                case MovingDirection.Rigth:
                    this.PosX += this.speed;
                    break;
            }
        }

        public void SpeedUp()
        {
            this.NumberOfTurbos--;
            new Thread(() =>
            {
                this.speed = 10;
                Thread.Sleep(7);
                this.speed = 5;
            });
        }
    }
}
