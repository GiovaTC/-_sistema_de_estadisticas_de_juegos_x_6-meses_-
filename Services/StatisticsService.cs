using GameStatsOracle.Database;
using Oracle.ManagedDataAccess.Client;
using System;

namespace GameStatsOracle.Services
{
    public class StatisticsService
    {
        private OracleConnectionManager connectionManager = new OracleConnectionManager();

        public void GetStatisticsLastSixMonths()
        {
            using (var connection = connectionManager.GetConnection())
            {
                connection.Open();

                string sql = @"
                SELECT 
                    COUNT(*)   TOTAL_JUEGOS, 
                    AVG(SCORE) PROMEDIO_PUNTAJE,
                    MAX(SCORE) MEJOR_PUNTAJE,
                    MIN(SCORE) PEOR_PUNTAJE
                FROM GAME_RESULTS
                WHERE GAME_DATE >= ADD_MONTHS(SYSDATE, -6)";
                               
                OracleCommand cmd = new OracleCommand(sql, connection);
                
                var reader = cmd.ExecuteReader();
                
                while (reader.Read())
                {
                   
                    Console.WriteLine("Total Juegos: " + reader["TOTAL_JUEGOS"]);
                    Console.WriteLine("Promedio Puntuacion: " + reader["PROMEDIO_PUNTAJE"]);
                    Console.WriteLine("Mejor Puntacion: " + reader["MEJOR_PUNTAJE"]);
                    Console.WriteLine("Peor Puntuacion: " + reader["PEOR_PUNTAJE"]);
                }
            }
        }
    } 
}