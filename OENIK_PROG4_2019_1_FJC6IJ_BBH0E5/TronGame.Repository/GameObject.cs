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
        public GameObject(int posX, int posY)
        {
            this.PosX = posX;
            this.PosY = posY;
        }

        public int PosX { get; private set; }

        public int PosY { get; private set; }
    }
}
