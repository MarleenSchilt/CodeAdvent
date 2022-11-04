using Day25;

namespace Day25_unittests
{
    class PlayingFieldHelper
    {
        public static PlayingField CreateBasicField2by2()
        {
            PlayingField field = new PlayingField();

            field.AddTile(0, 0, TileDirection.Direction.East);
            field.AddTile(0, 1, TileDirection.Direction.East);
            field.AddTile(1, 0, TileDirection.Direction.None);
            field.AddTile(1, 1, TileDirection.Direction.None);

            // >.
            // >.

            return field;
        }

        public static PlayingField CreateFieldForMapExitingAndHavingToWait()
        {
            PlayingField field = new PlayingField();

            field.AddTile(0, 0, TileDirection.Direction.East);
            field.AddTile(1, 0, TileDirection.Direction.None);
            field.AddTile(2, 0, TileDirection.Direction.None);
            field.AddTile(3, 0, TileDirection.Direction.None);
            field.AddTile(0, 1, TileDirection.Direction.South);
            field.AddTile(1, 1, TileDirection.Direction.South);
            field.AddTile(2, 1, TileDirection.Direction.South);
            field.AddTile(3, 1, TileDirection.Direction.None);

            return field;
        }
    }
}
