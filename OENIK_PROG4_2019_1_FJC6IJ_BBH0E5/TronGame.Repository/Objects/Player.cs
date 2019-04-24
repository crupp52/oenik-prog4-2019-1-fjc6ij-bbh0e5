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
                    this.Area = new System.Windows.Rect(this.Area.X, this.Area.Y - this.speed, 40, 40);
                    this.PosY -= this.speed;
                    break;
                case MovingDirection.Down:
                    this.Area = new System.Windows.Rect(this.Area.X, this.Area.Y + this.speed, 40, 40);
                    this.PosY += this.speed;
                    break;
                case MovingDirection.Left:
                    this.Area = new System.Windows.Rect(this.Area.X - this.speed, this.Area.Y, 40, 40);
                    this.PosX -= this.speed;
                    break;
                case MovingDirection.Rigth:
                    this.Area = new System.Windows.Rect(this.Area.X + this.speed, this.Area.Y, 40, 40);
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
