using System;

namespace GameStatsOracle.Models
{
    public class GameResult
    {
        public DateTime GameDate { get; set; }
        public string PlayerName { get; set; }
        public string GameName { get; set; }
        public int Score { get; set; }
        public string Result { get; set; }
    }
}   
