using Day25;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace Day25_unittests
{
    [TestClass]
    public class PlayingFieldTests
    {


        [TestMethod]
        public void PlayingFieldAddTile()
        {
            PlayingField field = new PlayingField();
            field.AddTile(0, 0, TileDirection.Direction.East);
            field.AddTile(0, 1, TileDirection.Direction.None);
            field.AddTile(0, 2, TileDirection.Direction.South);

            Assert.AreEqual(field.RowCount, 3);
            Assert.AreEqual(field.ColumnCount, 1);

            Assert.AreEqual(field.Count, 3);
        }

        [TestMethod]
        public void PlayingFieldToString()
        {
            var field = PlayingFieldHelper.CreateBasicField2by2();
            var result = field.ToString();

            Assert.AreEqual(result, ">.\n>.\n");
        }


        [TestMethod]
        public void PlayingFieldGetTileAtInsideField()
        {
            var field = PlayingFieldHelper.CreateBasicField2by2();

            var coordinate = new Point(0, 1);
            var tile = field.GetTileAt(coordinate);


            Assert.IsNotNull(tile);
            Assert.AreEqual(tile.Coordinate, coordinate);
            Assert.AreEqual(tile.Direction, TileDirection.Direction.East);
        }

        [TestMethod]
        public void PlayingFieldGetTileAtOutsideField()
        {
            var field = PlayingFieldHelper.CreateBasicField2by2();
            var tile = field.GetTileAt(new Point(5, 5));

            Assert.IsNull(tile);
        }
    }
}
