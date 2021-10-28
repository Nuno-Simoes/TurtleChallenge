namespace TurtleChallenge
{
    using System;
    using Library;
    using Library.Parsers;
    using Library.Settings;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public static class Program
    {
        public static void Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();

            using var serviceScope = host.Services.CreateScope();
            var provider = serviceScope.ServiceProvider;
            
            var parser = provider.GetService<IParser>();
            var settings = parser.ParseFile<GameSettings>(args[0]);
            var moves = parser.ParseFile<GameMoves>(args[1]);
            
            var manager = provider.GetService<IManager>();

            try
            {
                Console.WriteLine(manager.RunGame(settings, moves).ParseResult());
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine($"Settings file contains elements outside of the board {args[0]}.");
            }
        }

        private static string ParseResult(this GameResults gameResult) => gameResult switch
        {
            GameResults.Success => "Turtle escaped.",
            GameResults.HitMine => "Turtle hit a mine.",
            GameResults.InDanger => "Turtle still in danger.",
            GameResults.OutOfBounds => "Turtle fell off the board."
        };

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddSingleton<IParser, JsonParser>()
                        .AddSingleton<IManager, Manager>());
    }
}
