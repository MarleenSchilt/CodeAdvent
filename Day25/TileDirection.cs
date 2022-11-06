namespace Day25
{
    /// <summary>
    /// Enum and ToString for the direction of a tile.
    /// There are three options; East-facing, South-facing and None.
    /// </summary>
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
