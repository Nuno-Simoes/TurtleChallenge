namespace TurtleChallenge.Library.Models
{
    using System.Text.Json.Serialization;

    public interface IMovableGameElement : IGameElement
    {
        public enum ElementDirection
        {
            North = 0,
            East = 1,
            South = 2,
            West = 3
        }
        
        public int XPosition { get; set; }
        
        public int YPosition { get; set; }
        
        public ElementDirection Direction { get; set; }

        public void Rotate();
    }
}
