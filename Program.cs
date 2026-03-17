using GameStatsOracle.Models;
using GameStatsOracle.Services;
using System;   

namespace GameStatsOracle
{
    class Program
    {
        static void Main(string[] args)
        {
            GameService gameService = new GameService();
            StatisticsService statsService= new StatisticsService();

            Console.WriteLine("registro de juego");

            GameResult game = new GameResult();
            game.GameDate = DateTime.Now;

            Console.Write("jugador: ");
            game.PlayerName = Console.ReadLine();

            Console.Write("juego: ");
            game.GameName = Console.ReadLine();

            Console.Write("puntaje: ");
            game.Score = int.Parse(Console.ReadLine());

            Console.Write("resultado (win / lose): ");
            game.Result = Console.ReadLine();

            gameService.SaveGame(game);
            Console.WriteLine("juego guardaro en oracle. ");

            Console.WriteLine("\nestadisticas ultimos 6 meses:\n");

            statsService.GetStatisticsLastSixMonths();
        }
    }
}   