using NUnit.Framework;
using System.Linq;

namespace PacManKata
{
    [TestFixture]
    public class MazeTest
    {
        private Maze maze;

        [SetUp]
        public void SetUp()
        {
            maze = new Maze(5, 5);
        }

        [TearDown]
        public void TearDown()
        {
            AssertThereIsExactlyOnePacMan();
        }

        [Test]
        public void PacManLooksUpInCenterOfMaze()
        {
            Assert.IsTrue(maze.IsPacManAt(3, 3));
            Assert.IsTrue(maze.IsPacManLookingUp());
        }

        [Test]
        public void PacManMovesUp()
        {
            maze.Tick();

            Assert.IsTrue(maze.IsPacManAt(2, 3));
            Assert.IsTrue(maze.IsPacManLookingUp());
            Assert.IsTrue(maze.IsEmptyAt(3, 3));
        }

        [Test]
        public void PacManWrapsAroundWhenAtTop()
        {
            maze.Tick();
            maze.Tick();
            maze.Tick();

            Assert.IsTrue(maze.IsPacManAt(5, 3));
            Assert.IsTrue(maze.IsPacManLookingUp());
            Assert.IsTrue(maze.IsEmptyAt(3, 3));
            Assert.IsTrue(maze.IsEmptyAt(2, 3));
            Assert.IsTrue(maze.IsEmptyAt(1, 3));
        }

        [Test]
        public void TickLeftTickCheck()
        {
            maze.Tick();
            maze.PacManLeft();
            maze.Tick();

            Assert.IsTrue(maze.IsPacManAt(2, 2));
            Assert.IsTrue(maze.IsPacManLookingLeft());
        }

        [Test]
        public void PacManMovesUpAndBackAgain()
        {
            maze.PacManDown();
            maze.Tick();

            Assert.IsTrue(maze.IsPacManAt(4, 3));
            Assert.IsTrue(maze.IsPacManLookingDown());
        }

        [Test]
        public void PacManMovesLeftAndBackAgain()
        {
            maze.PacManLeft();
            maze.Tick();
            maze.PacManRight();
            maze.Tick();

            Assert.IsTrue(maze.IsPacManAt(3, 3));
            Assert.IsTrue(maze.IsPacManLookingRight());
        }

        [Test]
        public void PacManChangesDirectionLeft()
        {
            maze.PacManLeft();

            Assert.IsTrue(maze.IsPacManAt(3, 3));
            Assert.IsTrue(maze.IsPacManLookingLeft());
        }

        [Test]
        public void PacManMovesLeft()
        {
            maze.PacManLeft();
            maze.Tick();

            Assert.IsTrue(maze.IsPacManAt(3, 2));
            Assert.IsTrue(maze.IsPacManLookingLeft());
            Assert.IsTrue(maze.IsEmptyAt(3, 3));
        }

        [Test]
        public void PacManWrapsAroundLeft()
        {
            maze.PacManLeft();
            maze.Tick();
            maze.Tick();
            maze.Tick();

            Assert.IsTrue(maze.IsPacManAt(3, 5));
            Assert.IsTrue(maze.IsPacManLookingLeft());
            Assert.IsTrue(maze.IsEmptyAt(3, 3));
            Assert.IsTrue(maze.IsEmptyAt(3, 2));
            Assert.IsTrue(maze.IsEmptyAt(3, 1));
        }

        [Test]
        public void PacManChangesDirectionDown()
        {
            maze.PacManDown();

            Assert.IsTrue(maze.IsPacManAt(3, 3));
            Assert.IsTrue(maze.IsPacManLookingDown());
        }

        [Test]
        public void PacManChangesDirectionRight()
        {
            maze.PacManRight();

            Assert.IsTrue(maze.IsPacManAt(3, 3));
            Assert.IsTrue(maze.IsPacManLookingRight());
        }

        [Test]
        public void PacManChangesDirectionUp()
        {
            maze.PacManDown();
            maze.PacManUp();

            Assert.IsTrue(maze.IsPacManAt(3, 3));
            Assert.IsTrue(maze.IsPacManLookingUp());
        }

        private void AssertThereIsExactlyOnePacMan()
        {
            int pacmans = Enumerable.Range(1, maze.Rows).Sum(row => Enumerable.Range(1, maze.Columns).Count(column => maze.IsPacManAt(row, column)));

            Assert.AreEqual(1, pacmans);
        }
    }
}
