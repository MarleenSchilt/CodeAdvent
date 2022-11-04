using System;
using System.Drawing;
using Day25;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day25_unittests
{
    [TestClass]
    public class TileTests
    {
        [TestMethod]
        public void TestMethodTileToString()
        {
            Tile tile = new Tile(0, 1, 1, TileDirection.Direction.South);
            string result = tile.ToString();
            Assert.AreEqual(result, "0 at 1:1 - v");
        }

        [TestMethod]
        public void TestMethodTileUpdateCoordinate()
        {
            Tile tile = new Tile(0, 1, 1, TileDirection.Direction.South);
            tile.UpdateCoordinate(new Point(2, 3));

            Assert.AreEqual(tile.Coordinate.X , 2);
            Assert.AreEqual(tile.Coordinate.Y , 3);
        }

        [TestMethod]
        public void TestMethodTileUpdateCoordinateWithSameCoordinate()
        {
            Tile tile = new Tile(0, 1, 1, TileDirection.Direction.South);
            tile.UpdateCoordinate(new Point(1, 1));

            Assert.AreEqual(tile.FutureCoordinate.X, 1);
            Assert.AreEqual(tile.FutureCoordinate.Y, 1);
        }

        [TestMethod]
        public void TestMethodTileUpdateFutureCoordinate()
        {
            Tile tile = new Tile(0, 1, 1, TileDirection.Direction.South);
            tile.UpdateFutureCoordinate(new Point(1, 2));

            Assert.AreEqual(tile.FutureCoordinate.X, 1);
            Assert.AreEqual(tile.FutureCoordinate.Y, 2);
        }
    }
}
