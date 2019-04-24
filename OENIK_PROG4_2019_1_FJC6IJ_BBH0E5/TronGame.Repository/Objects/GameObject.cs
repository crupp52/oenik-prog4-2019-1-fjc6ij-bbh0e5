namespace TronGame.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

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

        public GameObject(double x, double y)
        {
            this.Area = new Rect(x, y, 40, 40);
        }

        public int PosX { get; set; }

        public int PosY { get; set; }

        private Rect _area;

        public Rect Area { get => _area; set => _area = value; }
    }
}
