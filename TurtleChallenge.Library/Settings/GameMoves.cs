namespace TurtleChallenge.Library.Settings
{
    public class GameMoves : IGameFile
    {
        public enum AllowedMoves
        {
            R,
            M
        }
        
        public AllowedMoves[] MovesSet { get; set; }
    }
}
