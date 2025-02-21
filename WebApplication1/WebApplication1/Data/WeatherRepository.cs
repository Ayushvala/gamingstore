using Microsoft.Data.SqlClient;
using WebApplication1.Model;

namespace WebApplication1.Data
{
    public class WeatherRepository
    {
        private readonly string _connectionString;

        public WeatherRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Get all weather data
        public IEnumerable<Weather> GetAll()
        {
            var weatherList = new List<Weather>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT Id, City, Temperature, Condition FROM Weather", connection);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var weather = new Weather
                    {
                        Id = reader.GetInt32(0),
                        City = reader.GetString(1),
                        Temperature = reader.GetInt32(2),
                        Condition = reader.GetString(3)
                    };
                    weatherList.Add(weather);
                }
            }
            return weatherList;
        }

        // Get single weather by ID
        public Weather GetById(int id)
        {
            Weather weather = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT Id, City, Temperature, Condition FROM Weather WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    weather = new Weather
                    {
                        Id = reader.GetInt32(0),
                        City = reader.GetString(1),
                        Temperature = reader.GetInt32(2),
                        Condition = reader.GetString(3)
                    };
                }
            }
            return weather;
        }

        // Insert weather data
        public void Insert(Weather weather)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO Weather (Id, City, Temperature, Condition) VALUES (@Id, @City, @Temperature, @Condition)", connection);
                command.Parameters.AddWithValue("@Id", weather.Id);
                command.Parameters.AddWithValue("@City", weather.City);
                command.Parameters.AddWithValue("@Temperature", weather.Temperature);
                command.Parameters.AddWithValue("@Condition", weather.Condition);

                command.ExecuteNonQuery();
            }
        }

        // Update weather data
        public void Update(Weather weather)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("UPDATE Weather SET City = @City, Temperature = @Temperature, Condition = @Condition WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", weather.Id);
                command.Parameters.AddWithValue("@City", weather.City);
                command.Parameters.AddWithValue("@Temperature", weather.Temperature);
                command.Parameters.AddWithValue("@Condition", weather.Condition);

                command.ExecuteNonQuery();
            }
        }

        // Delete weather data
        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM Weather WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);

                command.ExecuteNonQuery();
            }
        }
    }
}
