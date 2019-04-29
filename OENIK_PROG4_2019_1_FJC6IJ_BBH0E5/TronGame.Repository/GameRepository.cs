namespace TronGame.Repository
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using System.Xml.Serialization;

    /// <summary>
    /// Type of the Objects
    /// </summary>
    public enum ObjectType
    {
        /// <summary>
        /// Player object
        /// </summary>
        Player,

        /// <summary>
        /// Speed up object
        /// </summary>
        Turbo,

        /// <summary>
        /// Obstacle object
        /// </summary>
        Obstacle
    }

    /// <summary>
    /// Difficulty of the Game
    /// </summary>
    public enum Difficulty
    {
        /// <summary>
        /// Easy mode.
        /// </summary>
        Easy,

        /// <summary>
        /// Medium mode.
        /// </summary>
        Medium,

        /// <summary>
        /// Hard mode.
        /// </summary>
        Hard
    }

    /// <summary>
    /// Contains the save and load gamestate methods.
    /// </summary>
    public class GameRepository : IRepository
    {
    }
}
