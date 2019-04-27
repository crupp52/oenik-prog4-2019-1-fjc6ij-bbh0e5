namespace TronGame.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// Base object for Player, Turbos and Obstacles
    /// </summary>
    public abstract class GameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameObject"/> class.
        /// Set start values of properties.
        /// </summary>
        protected GameObject()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObject"/> class.
        /// </summary>
        /// <param name="x">Position in X axis</param>
        /// <param name="y">Position in Y axis</param>
        protected GameObject(double x, double y)
        {
            this.Point = new Point(x, y);
        }

        /// <summary>
        /// Gets or sets position of the object
        /// </summary>
        public Point Point { get; set; }
    }
}
