using System.Drawing;

namespace Day25
{
    public class Tile
    {
        private int uniqueId = 0;
        public Point Coordinate { get; private set; }
        public TileDirection.Direction Direction { get; private set; }
        public Point FutureCoordinate { get; private set; }

        /// <summary>
        /// Constructor. Create a new tile, with a unique id, a coordinate and a direction
        /// </summary>
        /// <param name="id">A unique id</param>
        /// <param name="x">The X-coordinate</param>
        /// <param name="y">The Y-coordinate</param>
        /// <param name="dir">The direction of the tile, either East-facing, South-facing or None</param>
        public Tile(int id, int x, int y, TileDirection.Direction dir)
        {
            uniqueId = id;
            Coordinate = new Point(x, y);
            Direction = dir;
            FutureCoordinate = Coordinate;
        }

        /// <summary>
        /// Update the coordinate during the move of the tile based on the future coordinate that was set during the preparation
        /// </summary>
        /// <param name="newCoordinate">The new coordinate of the tile. Clear the future coordinate since that value has served its purpose</param>
        public void UpdateCoordinate(Point newCoordinate)
        {
            if (Coordinate != newCoordinate)
            {
                Coordinate = newCoordinate;
                FutureCoordinate = Coordinate;
            }
        }

        /// <summary>
        /// Set the future coordinate for the coming move
        /// </summary>
        /// <param name="newFutureCoordinate">The next coordinate where the tile will be placed</param>
        public void UpdateFutureCoordinate(Point newFutureCoordinate)
        {
            if (FutureCoordinate != newFutureCoordinate)
                FutureCoordinate = newFutureCoordinate;
        }

        /// <summary>
        /// Show a representation of the tile, showing it's most important properties
        /// </summary>
        /// <returns>A string with some information on the tile</returns>
        public override string ToString()
        {
            return $"{uniqueId} at {Coordinate.X}:{Coordinate.Y} - {TileDirection.GetDirectionSymbol(Direction)}";
        }
    }
}
