namespace TurtleChallenge.Library.Parsers
{
    using System;
    using System.IO;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Settings;

    public class JsonParser : IParser
    {
        public T ParseFile<T>(string fileLocation) where T : IGameFile
        {
            if (string.IsNullOrWhiteSpace(fileLocation))
            {
                throw new ArgumentNullException(nameof(fileLocation), 
                    "A path for the game file must be provided.");
            }

            var jsonString = File.ReadAllText(fileLocation);

            var serializerOptions = new JsonSerializerOptions{
                Converters ={
                    new JsonStringEnumConverter()
                }
            };
            
            return JsonSerializer.Deserialize<T>(jsonString, serializerOptions);
        }
    }
}
