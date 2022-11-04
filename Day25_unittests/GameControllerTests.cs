using Day25;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Day25_unittests
{
    [TestClass]
    public class GameControllerTests
    {
        [TestMethod]
        public void GameControllerSingleTurnNoMapExiting()
        {
            var field = PlayingFieldHelper.CreateBasicField2by2();
            GameController gameController = new GameController(field);

            gameController.PlayTurn();

            var result = field.ToString();
            Assert.AreEqual(result, ".>\n.>\n");
        }

        [TestMethod]
        public void GameControllerTwoTurnsWithMapExiting()
        {
            var field = PlayingFieldHelper.CreateBasicField2by2();
            GameController gameController = new GameController(field);

            gameController.PlayTurn();
            gameController.PlayTurn();

            var result = field.ToString();

            Assert.AreEqual(result, ">.\n>.\n");
        }

        [TestMethod]
        public void GameControllerTwoTurnsWithMapExitingAndHavingToWait()
        {
            var field = PlayingFieldHelper.CreateFieldForMapExitingAndHavingToWait();
            GameController gameController = new GameController(field);

            gameController.PlayTurn();
            gameController.PlayTurn();

            var result = field.ToString();

            Assert.AreEqual(result, ".>..\nvvv.\n");

            //Initial state:
            // >...
            // vvv.

            // After 1 move:
            // v>v.
            // .v..

            // After 2 moves:
            // .>..
            // vvv.
        }
    }
}
