namespace TronGame.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class GameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GameObject"/> class.
        /// Set start values of properties.
        /// </summary>
        /// <param name="posX">Positions on X dimention.</param>
        /// <param name="posY">Positions on Y dimention.</param>
        public GameObject()
        {
        }

        public int PosX { get; set; }

        public int PosY { get; set; }
    }
}
