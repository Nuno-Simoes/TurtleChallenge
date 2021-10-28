using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TurtleChallenge.UnitTests")]
namespace TurtleChallenge.Library.Models
{
    using System;
    using Library;

    public class GameBoard
    {
        public IGameElement[,] Elements {get; internal set; }

        public int Width { get; }

        public int Height { get; }

        public GameBoard(int width, int height)
        {
            if(width <= 0)
            {
                throw new ArgumentException("Width must be greater then 0");
            }
            
            if(height <= 0)
            {
                throw new ArgumentException("Height must be greater then 0");
            }
            
            Width = width;
            Height = height;
            Elements = new IGameElement[width, height];
        }

        public void PlaceElement<T>(T element, int x, int y) where T : IGameElement
        {
            if (IsOutOfBounds(x, y))
            {
                throw new ArgumentOutOfRangeException($"{element.GetType().FullName} placed outside of the board [{Width},{Height}] ->[{x},{y}]");
            }
            
            Elements[x, y] = element;
        }

        private bool IsOutOfBounds(int xPos, int yPos) => xPos < 0 || xPos > Width-1 || yPos < 0 || yPos > Height-1;

        private IGameElement GetElement(int x, int y) => Elements[x, y];

        public GameResults MoveElement<T>(T movableElement, int x, int y) where T : IMovableGameElement
        {
            Elements[movableElement.XPosition, movableElement.YPosition] = null;
            
            movableElement.XPosition = x;
            movableElement.YPosition = y;
            
            var moveResult = ValidateMove(x, y);
            if (moveResult != GameResults.OutOfBounds)
            {
                Elements[x, y] = movableElement;
            }

            return moveResult;
        }
        
        private GameResults ValidateMove(int nextXPos, int nextYPos)
        {
            if (IsOutOfBounds(nextXPos, nextYPos))
            {
                return GameResults.OutOfBounds;
            }

            var nextTileElement = GetElement(nextXPos, nextYPos);
            return nextTileElement switch
            {
                null => GameResults.InDanger,
                Mine _ => GameResults.HitMine,
                Exit _ => GameResults.Success,
                _ => throw new ArgumentOutOfRangeException(nameof(nextTileElement))
            };
        }
    }
}
