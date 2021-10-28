namespace TurtleChallenge.Library
{
    using Settings;

    public interface IManager
    {
        GameResults RunGame(GameSettings gameSettings, GameMoves gameMoves);
    }
}
