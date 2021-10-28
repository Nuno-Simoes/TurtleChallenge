namespace TurtleChallenge.Library.Settings
{
    using System.Collections.Generic;
    using Models;

    public class GameSettings : IGameFile
    {
        public BoardSize BoardSize { get; set; }
        
        public DirectionalPosition StartPosition { get; set; }
        
        public TwoDPosition ExitPosition { get; set; }
        
        public ISet<TwoDPosition> MinePositions { get; set; }
    }
    
    public class BoardSize
    {
        public int Height { get; set; }
        
        public int Width { get; set; }
    }

    public class TwoDPosition
    {
        public int X { get; set; }
        
        public int Y { get; set; }
    }

    public class DirectionalPosition : TwoDPosition
    {
        public IMovableGameElement.ElementDirection Direction { get; set; }
    }
}
