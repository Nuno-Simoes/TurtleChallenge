using NUnit.Framework;

namespace TurtleChallenge.UnitTests
{
    using System;
    using Library;
    using Library.Models;

    public class GameBoardTests
    {
        [Test]
        [TestCase(0, 1)]
        [TestCase(1, 0)]
        public void GameBoard_InvalidArgumentsProvided_ArgumentExceptionThrown(int width, int height)
        {
            // ARRANGE // ACT // ASSERT
            Assert.Throws<ArgumentException>(() => _ = new GameBoard(width, height));
        }
        
        [Test]
        public void PlaceElement_CorrectElement_ElementPlacedOnCorrectLocation()
        {
            // ARRANGE
            var sut = new GameBoard(2, 2);
            var testElement = new Mine();
            
            // ACT
            sut.PlaceElement(testElement, 1, 1);
            
            // ASSERT
            Assert.AreEqual(testElement, sut.Elements[1, 1]);
        }
        
        [Test]
        public void PlaceElement_ElementOutOfBounds_ArgumentOutOfRangeExceptionThrown()
        {
            // ARRANGE
            var sut = new GameBoard(1, 1);
            var element = new Mine();
            
            // ACT // ASSERT
            Assert.Throws<ArgumentOutOfRangeException>(() => sut.PlaceElement(element, 1, 1));
        }
        
        [Test]
        [TestCase(1, 1, GameResults.Success)]
        [TestCase(1, 0, GameResults.InDanger)]
        [TestCase(0, 1, GameResults.HitMine)]
        public void MoveElement_MoveCases_ElementMovedAndExpectedResultReturned(int nextX, int nextY, GameResults expectedResult)
        {
            // ARRANGE
            var sut = new GameBoard(2, 2);
            sut.Elements[0, 1] = new Mine();
            sut.Elements[1, 1] = new Exit();

            var turtle = new Turtle(IMovableGameElement.ElementDirection.West, 0,0);
            
            // ACT
            var moveResult = sut.MoveElement(turtle, nextX, nextY);
                
            // ASSERT
            Assert.AreEqual(expectedResult, moveResult);
            Assert.AreEqual(turtle, sut.Elements[nextX, nextY]);
        }
        
        [Test]
        public void MoveElement_MoveOutOfBounds_ElementMovedAndOutOfBoundsResultReturned()
        {
            // ARRANGE
            var sut = new GameBoard(1, 1);
            var turtle = new Turtle(IMovableGameElement.ElementDirection.West, 0,0);
            sut.Elements[0, 0] = turtle;
            
            // ACT
            var moveResult = sut.MoveElement(turtle, 2, 2);
                
            // ASSERT
            Assert.AreEqual(GameResults.OutOfBounds, moveResult);
            Assert.AreEqual(null, sut.Elements[0, 0]);
        }
    }
}
