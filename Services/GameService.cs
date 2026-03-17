using GameStatsOracle.Database;
using GameStatsOracle.Models;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System;

namespace GameStatsOracle.Services
{
    public class GameService
    {
        private readonly OracleConnectionManager connectionManager = new OracleConnectionManager();

        public void SaveGame(GameResult game)
        {
            using (var connection = connectionManager.GetConnection())
            {
                connection.Open();

                string sql = @"INSERT INTO GAME_RESULTS 
                               (GAME_DATE, PLAYER_NAME, GAME_NAME, SCORE, RESULT) 
                               VALUES (:p_date, :p_player, :p_game, :p_score, :p_result)";

                using (OracleCommand cmd = new OracleCommand(sql, connection))
                {
                    // 🔥 Importante en Oracle
                    cmd.BindByName = true;

                    // ✅ Parámetros tipados correctamente
                    cmd.Parameters.Add("p_date", OracleDbType.Date).Value = game.GameDate;
                    cmd.Parameters.Add("p_player", OracleDbType.Varchar2).Value = game.PlayerName;
                    cmd.Parameters.Add("p_game", OracleDbType.Varchar2).Value = game.GameName;
                    cmd.Parameters.Add("p_score", OracleDbType.Int32).Value = game.Score;
                    cmd.Parameters.Add("p_result", OracleDbType.Varchar2).Value = game.Result;

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}