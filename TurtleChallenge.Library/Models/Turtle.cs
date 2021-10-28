namespace TurtleChallenge.Library.Models
{
    using Extensions;

    public class Turtle : IMovableGameElement
    {
        public Turtle(IMovableGameElement.ElementDirection startingDirection, int startingXPosition, int startingYPosition)
        {
            Direction = startingDirection;
            XPosition = startingXPosition;
            YPosition = startingYPosition;
        }

        public int XPosition { get; set; }
        
        public int YPosition { get; set; }
        
        public IMovableGameElement.ElementDirection Direction { get; set; }

        public void Rotate() => Direction = Direction.CycleNext();
    }
}
