using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Day25
{
    /// <summary>
    /// Every field on the playing field is a tile. All tiles combined form the playing field. 
    /// This class keeps track of all tiles that make up the playing field. There are some accessors to find tiles, rows and columns quickly. 
    /// The actual logic can be found in the GameController class
    /// </summary>
    public class PlayingField : List<Tile>
    {
        private int uniqueTileId = 0;

        public PlayingField() { }

        /// <summary>
        /// Verify if there are no holes in the field. The amount of tiles should match the expected amount by checking the product of rows and columns.
        /// </summary>
        /// <returns>True if the amount of tiles is OK, false if there are not enough or too many tiles</returns>
        internal bool VerifyTileCount()
        {
            return (RowCount * ColumnCount) == this.Count;
        }

        /// <summary>
        /// Print the current state of the playing field. The layout of the field is as follows:
        ///   ---> (incrementing X-coordinate)
        /// |
        /// |
        /// V (incrementing Y-coordinate)
        /// 
        /// Tiles can have either an East facing direction, a South facing direction or be empty.
        /// </summary>
        /// <returns>A string that shows the state of the playing field</returns>
        public override string ToString()
        {
            string result = "";

            for (int i = 0; i < RowCount; i++)
            {
                var row = GetRow(i);
                foreach (var tile in row)
                {
                    result += TileDirection.GetDirectionSymbol(tile.Direction);
                }
                result += "\n";
            }

            return result;
        }

        /// <summary>
        /// Add a tile to the playing field, based on coordinate and the direction
        /// </summary>
        /// <param name="x">The X-coordinate where the tile will be</param>
        /// <param name="y">The Y-coordinate where the tile will be</param>
        /// <param name="direction">Shows if the tile is East-facing, South-facing or empty</param>
        public void AddTile(int x, int y, TileDirection.Direction direction)
        {
            Add(new Tile(uniqueTileId++, x, y, direction));
        }

        #region Accessors
        /// <summary>
        /// Get the tile at a given coordinate
        /// </summary>
        /// <param name="point">A coordinate in the playing field</param>
        /// <returns>The tile on that coordinate. Returns null if tile is not found or the coordinate is outside the playing field bounds</returns>
        public Tile GetTileAt(Point point)
        {
            return Find(t => t.Coordinate == point);
        }

        private int rowCount = 0;
        /// <summary>
        /// Get the amount of rows available in the playing field
        /// </summary>
        public int RowCount
        {
            get
            {
                if (rowCount == 0)
                    rowCount = this.Select(t => t.Coordinate.Y).GroupBy(y => y).Count();
                return rowCount;
            }
        }

        private int columnCount = 0;
        /// <summary>
        /// Get the amount of columns available in the playing field
        /// </summary>
        public int ColumnCount
        {
            get
            {
                if (columnCount == 0)
                    columnCount = this.Select(t => t.Coordinate.X).GroupBy(x => x).Count();
                return columnCount;
            }
        }

        /// <summary>
        /// Get a subset of the playing field; all tiles that are present on the given row.
        /// </summary>
        /// <param name="index">The index of the row</param>
        /// <returns>A list of all tiles that are present on the given row. They are sorted from left to right (incrementing X-coordinates)</returns>
        List<Tile> GetRow(int index)
        {
            return this.Where(t => t.Coordinate.Y == index).OrderBy(r => r.Coordinate.X).ToList();
        }

        /// <summary>
        /// Get a subset of the playing field; all tiles that are present on the given column.
        /// </summary>
        /// <param name="index">The index of the column</param>
        /// <returns>A list of all tiles that are present on the given column. They are sorted from top to bottom (incrementing Y-coordinates)</returns>
        List<Tile> GetColumn(int index)
        {
            return this.Where(t => t.Coordinate.X == index).OrderBy(r => r.Coordinate.Y).ToList();
        }
        #endregion
    }
}
