using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day25
{
    public class TileDirection
    {
        public enum Direction
        {
            East,   // >
            South,  // v
            None    // .
        }

        /// <summary>
        /// Get the symbol that matches the direction
        /// </summary>
        /// <param name="dir">The direction for which the symbol must be printed</param>
        /// <returns>A string containing the symbol of the given direction</returns>
        public static string GetDirectionSymbol(Direction dir)
        {
            switch (dir)
            {
                case Direction.East:
                    return ">";
                case Direction.South:
                    return "v";
                case Direction.None:
                    return ".";
                default:
                    return "?";
            }
        }
    }
}
