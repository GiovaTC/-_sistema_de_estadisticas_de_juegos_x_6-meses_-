using GameStatsOracle.Database;
using GameStatsOracle.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStatsOracle.Services
{
    public class GameService
    {
        private OracleConnectionManager connectionManager = new OracleConnectionManager();

        public void SaveGame(GameResult game)
        {
            using (var connection = connectionManager.GetConnection())
            {
                connection.Open();

                string sql = @"INSERT INTO GAME_RESULTS 
                      (GAME_DATE, PLAYER_NAME, GAME_NAME, SCORE, RESULT) 
                      VALUES (:date, :player, :game, :score, :result)";

                using (OracleCommand cmd = new OracleCommand(sql, connection))
                {
                    cmd.BindByName = true;

                    cmd.Parameters.Add("date", game.GameDate);
                    cmd.Parameters.Add("player", game.PlayerName);
                    cmd.Parameters.Add("game", game.GameName);
                    cmd.Parameters.Add("score", game.Score);
                    cmd.Parameters.Add("result", game.Result);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}   
