namespace TurtleChallenge.Library
{
    using System;
    using Models;
    using Settings;

    public class Manager : IManager
    {
        public GameResults RunGame(GameSettings gameSettings, GameMoves gameMoves)
        {
            var (gameBoard, turtle) = LoadBoard(gameSettings);

            return RunMoves(gameMoves, gameBoard, turtle);
        }
        
        private static (GameBoard, Turtle) LoadBoard(GameSettings gameSettings)
        {
            var gameBoard = new GameBoard(gameSettings.BoardSize.Width, gameSettings.BoardSize.Height);

            var turtle = new Turtle(gameSettings.StartPosition.Direction,
                gameSettings.StartPosition.X,
                gameSettings.StartPosition.Y);

            gameBoard.PlaceElement(turtle, gameSettings.StartPosition.X, gameSettings.StartPosition.Y);

            gameBoard.PlaceElement(new Exit(), gameSettings.ExitPosition.X, gameSettings.ExitPosition.Y);

            foreach (var minePosition in gameSettings.MinePositions)
            {
                gameBoard.PlaceElement(new Mine(), minePosition.X, minePosition.Y);
            }

            return (gameBoard, turtle);
        }

        private GameResults RunMoves(GameMoves gameMoves, GameBoard gameBoard, Turtle turtle)
        {
            foreach (var move in gameMoves.MovesSet)
            {
                switch (move)
                {
                    case GameMoves.AllowedMoves.R:
                        turtle.Rotate();
                        break;
                    case GameMoves.AllowedMoves.M:
                        var (nextXPos, nextYPos) = GetTurtleNextPosition(turtle.XPosition, turtle.YPosition, turtle.Direction);

                        var moveResult = gameBoard.MoveElement(turtle, nextXPos, nextYPos);
                        if (moveResult != GameResults.InDanger)
                        {
                            return moveResult;
                        }

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return GameResults.InDanger;
        }

        private static (int x, int y) GetTurtleNextPosition(int currentXPos, int currentYPos, IMovableGameElement.ElementDirection direction) =>
            direction switch
            {
                IMovableGameElement.ElementDirection.North => (currentXPos, currentYPos-1),
                IMovableGameElement.ElementDirection.East => (currentXPos+1, currentYPos),
                IMovableGameElement.ElementDirection.South => (currentXPos, currentYPos+1),
                IMovableGameElement.ElementDirection.West => (currentXPos-1, currentYPos),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), "Invalid direction provided.")
            };
    }
}
