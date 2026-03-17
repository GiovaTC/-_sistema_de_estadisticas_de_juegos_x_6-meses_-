# -_sistema_de_estadisticas_de_juegos_x_6-meses_- :.
📊 Sistema de Estadísticas de Juegos (6 meses):

<img width="1024" height="1024" alt="image" src="https://github.com/user-attachments/assets/355ed210-0523-4528-9196-0d0bc540595d" />    

<img width="2542" height="1073" alt="image" src="https://github.com/user-attachments/assets/8cc45d76-68ab-4d7d-aa2d-76ea1f22027d" />    

```
C# (.NET) + Oracle Database 19c.
Este proyecto muestra un ejemplo completo de aplicación en .NET C# para registrar y calcular estadísticas de juegos durante 6 meses, almacenando los datos en Oracle Database 19c.

La aplicación incluye:
- Conexión a Oracle
- Registro de resultados de juegos
- Consulta por rango de fechas
- Cálculo de estadísticas
- Interfaz Console App (fácil de probar)

🧰 Tecnologías usadas:

- C# (.NET)
- Oracle Database 19c
- Oracle Data Provider for .NET (ODP.NET)
- Oracle SQL.

📁 Estructura del proyecto
GameStatsOracle
│
├── Database
│     OracleConnectionManager.cs
│
├── Models
│     GameResult.cs
│
├── Services
│     GameService.cs
│     StatisticsService.cs
│
└── Program.cs

🗄️ 1. Script SQL para Oracle:
Ejecutar en Oracle SQL Developer o Oracle Database 19c.
CREATE TABLE GAME_RESULTS (
    ID NUMBER GENERATED ALWAYS AS IDENTITY,
    GAME_DATE DATE,
    PLAYER_NAME VARCHAR2(100),
    GAME_NAME VARCHAR2(100),
    SCORE NUMBER,
    RESULT VARCHAR2(20),
    CONSTRAINT PK_GAME_RESULTS PRIMARY KEY(ID)
);

📦 2. Instalar librería Oracle en .NET:
En NuGet Package Manager instalar:
Oracle.ManagedDataAccess
Este paquete es el proveedor oficial para conectar .NET con Oracle.

🧩 3. Modelo de datos:
📄 Models/GameResult.cs
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

🔗 4. Conexión a Oracle:
📄 Database/OracleConnectionManager.cs
using Oracle.ManagedDataAccess.Client;

namespace GameStatsOracle.Database
{
    public class OracleConnectionManager
    {
        private string connectionString =
            "User Id=USUARIO;Password=PASSWORD;Data Source=localhost:1521/XEPDB1";

        public OracleConnection GetConnection()
        {
            return new OracleConnection(connectionString);
        }
    }
}

🎮 5. Servicio para registrar juegos:
📄 Services/GameService.cs
using GameStatsOracle.Database;
using GameStatsOracle.Models;
using Oracle.ManagedDataAccess.Client;

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

                OracleCommand cmd = new OracleCommand(sql, connection);

                cmd.Parameters.Add(":date", game.GameDate);
                cmd.Parameters.Add(":player", game.PlayerName);
                cmd.Parameters.Add(":game", game.GameName);
                cmd.Parameters.Add(":score", game.Score);
                cmd.Parameters.Add(":result", game.Result);

                cmd.ExecuteNonQuery();
            }
        }
    }
}

📈 6. Servicio de estadísticas:
Calcula estadísticas de los últimos 6 meses.
📄 Services/StatisticsService.cs

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
                    COUNT(*) TOTAL_JUEGOS,
                    AVG(SCORE) PROMEDIO_PUNTAJE,
                    MAX(SCORE) MEJOR_PUNTAJE,
                    MIN(SCORE) PEOR_PUNTAJE
                FROM GAME_RESULTS
                WHERE GAME_DATE >= ADD_MONTHS(SYSDATE, -6)";

                OracleCommand cmd = new OracleCommand(sql, connection);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine("Total juegos: " + reader["TOTAL_JUEGOS"]);
                    Console.WriteLine("Promedio puntuación: " + reader["PROMEDIO_PUNTAJE"]);
                    Console.WriteLine("Mejor puntuación: " + reader["MEJOR_PUNTAJE"]);
                    Console.WriteLine("Peor puntuación: " + reader["PEOR_PUNTAJE"]);
                }
            }
        }
    }
}

🖥️ 7. Programa principal:
📄 Program.cs

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
            StatisticsService statsService = new StatisticsService();

            Console.WriteLine("Registro de Juego");

            GameResult game = new GameResult();

            game.GameDate = DateTime.Now;

            Console.Write("Jugador: ");
            game.PlayerName = Console.ReadLine();

            Console.Write("Juego: ");
            game.GameName = Console.ReadLine();

            Console.Write("Puntaje: ");
            game.Score = int.Parse(Console.ReadLine());

            Console.Write("Resultado (WIN/LOSE): ");
            game.Result = Console.ReadLine();

            gameService.SaveGame(game);

            Console.WriteLine("Juego guardado en Oracle.");

            Console.WriteLine("\nEstadísticas últimos 6 meses:\n");

            statsService.GetStatisticsLastSixMonths();
        }
    }
}

▶️ 8. Ejemplo de ejecucion:
Registro de Juego:
Jugador: Carlos
Juego: FIFA
Puntaje: 5
Resultado: WIN

Juego guardado en Oracle.

Estadísticas últimos 6 meses:
Total juegos: 20
Promedio puntuación: 3.5
Mejor puntuación: 7
Peor puntuación: 1

📊 Posibles mejoras del sistema
Se pueden agregar mejoras como:
Interfaz gráfica con WPF o WinForms
Dashboard con gráficas de estadísticas
Exportación de resultados a Excel  
API REST con ASP.NET Core
Autenticación de usuarios
Ranking de jugadores  / :. / .        
