namespace TurtleChallenge.Library.Parsers
{
    using Settings;

    public interface IParser
    {
        T ParseFile<T>(string fileLocation) where T : IGameFile;
    }
}
