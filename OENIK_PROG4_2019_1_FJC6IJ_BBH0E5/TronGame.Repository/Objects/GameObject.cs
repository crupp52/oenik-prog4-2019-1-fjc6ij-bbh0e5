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
        public GameObject()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameObject"/> class.
        /// </summary>
        /// <param name="x">Position in X axis</param>
        /// <param name="y">Position in Y axis</param>
        public GameObject(double x, double y)
        {
            this.Area = new Rect(x, y, 40, 40);
        }

        /// <summary>
        /// Position in X axis
        /// </summary>
        public int PosX { get; set; }

        /// <summary>
        /// Position in Y axis
        /// </summary>
        public int PosY { get; set; }

        /// <summary>
        /// Area of the object
        /// </summary>
        public Rect Area { get; set; }
    }
}
