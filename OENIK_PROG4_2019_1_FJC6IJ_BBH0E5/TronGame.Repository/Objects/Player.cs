namespace TronGame.Repository
{
    using GalaSoft.MvvmLight;
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
        public event EventHandler PlayerStep;

        private readonly Stopwatch stopwatch;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        public Player()
        {
            this.stopwatch = new Stopwatch();
            this.Turbo = false;

            this.NumberOfWins = 0;
            this.NumberOfTurbos = 0;

            //this.Move();
        }

        public bool Turbo { get; set; }

        private int numberOfWins;
        private int numberOfTurbos;
        private string name;

        /// <summary>
        /// Gets or sets name of player
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.Set(ref this.name, value); }
        }

        /// <summary>
        /// Gets or sets number of wins
        /// </summary>
        public int NumberOfWins
        {
            get { return this.numberOfWins; }
            set { this.Set(ref this.numberOfWins, value); }
        }

        /// <summary>
        /// Gets or sets number of speed ups
        /// </summary>
        public int NumberOfTurbos
        {
            get { return this.numberOfTurbos; }
            set { this.Set(ref this.numberOfTurbos, value); }
        }

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
                            if (this.Point.Y > 0)
                            {
                                this.Point = new Point(this.Point.X, this.Point.Y - 1);
                            }
                            break;
                        case MovingDirection.Down:
                            if (this.Point.Y < 27)
                            {
                                this.Point = new Point(this.Point.X, this.Point.Y + 1);
                            }
                            break;
                        case MovingDirection.Left:
                            if (this.Point.X > 0)
                            {
                                this.Point = new Point(this.Point.X - 1, this.Point.Y);
                            }
                            break;
                        case MovingDirection.Rigth:
                            if (this.Point.X < 48)
                            {
                                this.Point = new Point(this.Point.X + 1, this.Point.Y);
                            }
                            break;
                    }
                    this.PlayerStep?.Invoke(this, EventArgs.Empty);
                    if (this.Turbo)
                    {
                        Thread.Sleep(250);
                    }
                    else
                    {
                        Thread.Sleep(300);
                    }
                }
            });
        }

        /// <summary>
        /// Activate the speed up
        /// </summary>
        public void SpeedUp()
        {
            this.NumberOfTurbos--;
            Task.Run(() =>
            {
                this.Turbo = true;
                Thread.Sleep(5000);
                this.Turbo = false;
            });
        }
    }
}
