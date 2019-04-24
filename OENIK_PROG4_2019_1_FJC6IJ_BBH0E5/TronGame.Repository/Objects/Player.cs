namespace TronGame.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Moving directions
    /// </summary>
    public enum MovingDirection
    {
        /// <summary>
        /// Move to up
        /// </summary>
        Up,

        /// <summary>
        /// Move to down
        /// </summary>
        Down,

        /// <summary>
        /// Move to left
        /// </summary>
        Left,

        /// <summary>
        /// Move to right
        /// </summary>
        Rigth
    }

    /// <summary>
    /// Player Object
    /// </summary>
    public class Player : GameObject
    {
        private readonly Stopwatch stopwatch;
        private int speed = 5;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        public Player()
        {
            this.stopwatch = new Stopwatch();

            this.NumberOfWins = 0;
            this.NumberOfTurbos = 0;
        }

        /// <summary>
        /// Gets or sets name of player
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets number of wins
        /// </summary>
        public int NumberOfWins { get; set; }

        /// <summary>
        /// Gets or sets number of speed ups
        /// </summary>
        public int NumberOfTurbos { get; set; }

        /// <summary>
        /// Move the player in the field
        /// </summary>
        /// <param name="direction">Direction of the move</param>
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

        /// <summary>
        /// Activate the speed up
        /// </summary>
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
