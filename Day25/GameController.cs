using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Day25
{
    /// <summary>
    /// This class handles the search for tiles that must be moved during a turn and the actual move. 
    /// East-facing tiles move first if there's place for them to the right.
    /// Then all South-facing tiles move if there is place beneath them.
    /// The game stops when no tiles can move.
    /// </summary>
    public class GameController
    {
        PlayingField playingField;
        List<Tile> tilesSelectedForMoving = new List<Tile>();
        public GameController(PlayingField field)
        {
            playingField = field;
        }
        
        /// <summary>
        /// The only way to win is to play the game. A turn consists of two actions; first find and move all tiles that can in the East-facing direction,
        /// then find and move any tiles that can in the South-facing direction.
        /// </summary>
        /// <returns>True if at least one tile was moved, in either East or South-facing direction. False if nothing moved. The game is over!</returns>
        public bool PlayTurn()
        {
            var movedEast = FindAndMoveTiles(TileDirection.Direction.East);
            var movedSouth = FindAndMoveTiles(TileDirection.Direction.South);

            return movedEast || movedSouth;
        }

        /// <summary>
        /// Start the action to find all tiles that need to be moved based on their direction, see if they're available to move, and actually move them when possible.
        /// </summary>
        /// <param name="tileDirection">The direction we're currently playing. Only tiles with the same direction are of interest at the moment</param>
        /// <returns>True if any tiles moved after all the preparation steps, false if nothing moved in the given direction</returns>
        private bool FindAndMoveTiles(TileDirection.Direction tileDirection)
        {
            ClearPreviousMoveSelection();

            // TODO: multithreading?
            foreach (var item in playingField)
            {
                PrepareTileMovement(item, tileDirection);
            }

            return MoveTiles();
        }

        /// <summary>
        /// Go through the list of all tiles and find the ones in the selected direction. Start the preparation for a mass relocation
        /// </summary>
        /// <param name="tile">The current tile</param>
        /// <param name="tileDirection">The direction for which we're searching tiles</param>
        private void PrepareTileMovement(Tile tile, TileDirection.Direction tileDirection)
        {
            if (tile.Direction == tileDirection)
            {
                CheckIfTileCanMove(tile);
            }
        }

        /// <summary>
        /// Check the neighbour of a tile. 
        /// This depends on the direction; an East-facing tile checks the neighbour to their right, a South-facing tile checks the neighbour to their bottom.
        /// If a tile is on the edge of the playing field, the first place on their row or column is checked, since tiles cannot fall off the field.
        /// </summary>
        /// <param name="tile">Enter the tile that must verify the state of their neighbour</param>
        /// <returns>True if the neighbour is empty, false if their neighbour prevents a move</returns>
        private bool CheckIfTileCanMove(Tile tile)
        {
            Point requestedCoordinate = DetermineFutureCoordinate(tile);

            if (requestedCoordinate != tile.Coordinate)
            {
                var neighbour = playingField.GetTileAt(requestedCoordinate);
                if (neighbour != null && neighbour.Direction == TileDirection.Direction.None)
                {
                    tile.UpdateFutureCoordinate(requestedCoordinate);
                    tilesSelectedForMoving.Add(tile);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determine where the tile will be moved to. An East-facing tile will move to their right, a South-facing tile will move to their bottom.
        /// </summary>
        /// <param name="tile">Enter the tile for which a new coordinate must be found</param>
        /// <returns>A future coordinate. Can be the same as the current coordinate if no move is possible</returns>
        private Point DetermineFutureCoordinate(Tile tile)
        {
            Point requestedCoordinate = tile.Coordinate;
            switch (tile.Direction)
            {
                case TileDirection.Direction.East:
                    int newXCoordinate = (tile.Coordinate.X + 1) % playingField.ColumnCount;
                    requestedCoordinate = new Point(newXCoordinate, tile.Coordinate.Y);
                    break;
                case TileDirection.Direction.South:
                    int newYCoordinate = (tile.Coordinate.Y + 1) % playingField.RowCount;
                    requestedCoordinate = new Point(tile.Coordinate.X, newYCoordinate);
                    break;
                case TileDirection.Direction.None:
                default:
                    break;
            }

            return requestedCoordinate;
        }

        /// <summary>
        /// Go through the list of all tiles marked for movement and update them to their new coordinate
        /// </summary>
        /// <returns>True if at least one tile is marked for movement, false if there are none.</returns>
        private bool MoveTiles()
        {
            // TODO: multithreading?
            foreach (var tile in tilesSelectedForMoving)
            {
                UpdateTileLocation(tile);
            }

            return tilesSelectedForMoving.Any();
        }

        /// <summary>
        /// Swap the current and neighbour tile
        /// </summary>
        /// <param name="tile">Enter the tile that must have its coordinate updated.</param>
        private void UpdateTileLocation(Tile tile)
        {
            var currentCoordinate = tile.Coordinate;
            var neighbour = playingField.GetTileAt(tile.FutureCoordinate);

            if (neighbour != null && tile != neighbour)
            {
                tile.UpdateCoordinate(tile.FutureCoordinate);
                neighbour.UpdateCoordinate(currentCoordinate);
            }
        }

        /// <summary>
        /// Clear the selection of tiles that had to be moved in the last turn, we're starting afresh.
        /// </summary>
        private void ClearPreviousMoveSelection()
        {
            tilesSelectedForMoving.Clear();
        }
    }
}
