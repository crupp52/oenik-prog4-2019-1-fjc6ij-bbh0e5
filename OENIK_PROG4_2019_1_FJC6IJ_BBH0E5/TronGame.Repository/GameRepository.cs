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

    public enum ObjectType
    {
        Player, Turbo, Obstacle
    }

    public enum Difficulty
    {
        Easy, Medium, Hard
    }

    public class GameRepository : IRepository
    {
    }
}
