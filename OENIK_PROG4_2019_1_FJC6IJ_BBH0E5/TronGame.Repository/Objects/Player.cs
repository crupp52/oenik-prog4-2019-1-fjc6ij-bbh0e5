namespace TronGame.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows;

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
        private int speed = 20;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        public Player()
        {
            this.stopwatch = new Stopwatch();

            this.NumberOfWins = 0;
            this.NumberOfTurbos = 0;
            this.Move();
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

        public MovingDirection MovingDirection { get; set; }

        /// <summary>
        /// Move the player in the field
        /// </summary>
        public void Move()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    switch (this.MovingDirection)
                    {
                        //case MovingDirection.Up:
                        //    this.Area = new System.Windows.Rect(this.Area.X, this.Area.Y - this.speed, 20, 20);
                        //    this.PosY -= this.speed;
                        //    break;
                        //case MovingDirection.Down:
                        //    this.Area = new System.Windows.Rect(this.Area.X, this.Area.Y + this.speed, 20, 20);
                        //    this.PosY += this.speed;
                        //    break;
                        //case MovingDirection.Left:
                        //    this.Area = new System.Windows.Rect(this.Area.X - this.speed, this.Area.Y, 20, 20);
                        //    this.PosX -= this.speed;
                        //    break;
                        //case MovingDirection.Rigth:
                        //    this.Area = new System.Windows.Rect(this.Area.X + this.speed, this.Area.Y, 20, 20);
                        //    this.PosX += this.speed;
                        //    break;
                        case MovingDirection.Up:
                            this.Point = new Point(this.Point.X, this.Point.Y - 1);
                            this.PosY -= this.speed;
                            break;
                        case MovingDirection.Down:
                            this.Point = new Point(this.Point.X, this.Point.Y + 1);
                            this.PosY += this.speed;
                            break;
                        case MovingDirection.Left:
                            this.Point = new Point(this.Point.X - 1, this.Point.Y);
                            this.PosX -= this.speed;
                            break;
                        case MovingDirection.Rigth:
                            this.Point = new Point(this.Point.X + 1, this.Point.Y);
                            this.PosX += this.speed;
                            break;
                    }
                    Thread.Sleep(300);
                }
            });
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
