using Oracle.ManagedDataAccess.Client;

namespace GameStatsOracle.Database
{
    public class OracleConnectionManager
    {
        private string connectionString =
            "User Id=system;Password=Tapiero123;Data Source=localhost:1521/orcl";

        public OracleConnection GetConnection()
        {
            return new OracleConnection(connectionString);
        }
    }
}   
