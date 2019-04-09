namespace TronGame.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TurboObject : GameObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TurboObject"/> class.
        /// </summary>
        /// <param name="posX">Positions on X dimention.</param>
        /// <param name="posY">Positions on Y dimention.</param>
        public TurboObject(int posX, int posY)
            : base(posX, posY)
        {
        }
    }
}
