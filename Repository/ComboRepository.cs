using MySqlConnector;
using RestauranteAPI.Models;

namespace RestauranteAPI.Repositories
{
    public class ComboRepository : IComboRepository
    {
        private readonly string _connectionString;

        public ComboRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("RestauranteBD");
        }
        public async Task<List<Combo>> GetAllAsync()
        {
            var combos = new List<Combo>();

            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            string query = "SELECT Id, IdPlatoPrincipal, IdBebida, IdPostre, Descuento, Precio FROM Combo";
            using var command = new MySqlCommand(query, connection);
            using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                var combo = new Combo(
                    platoPrincipal: null!,
                    bebida: null!,
                    postre: null!,
                    descuento: reader.GetDouble("Descuento")
                )
                {
                    Id = reader.GetInt32("Id"),
                    Precio = reader.GetDouble("Precio")
                };

                combos.Add(combo);
            }

            return combos;
        }

        public async Task<Combo?> GetByIdAsync(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            string query = "SELECT Id, IdPlatoPrincipal, IdBebida, IdPostre, Descuento, Precio FROM Combo WHERE Id = @Id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new Combo(
                    platoPrincipal: null!,
                    bebida: null!,
                    postre: null!,
                    descuento: reader.GetDouble("Descuento")
                )
                {
                    Id = reader.GetInt32("Id"),
                    Precio = reader.GetDouble("Precio")
                };
            }

            return null;
        }

        public async Task AddAsync(ComboCreateDto dto)
        {
            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            string query = @"INSERT INTO Combo (IdPlatoPrincipal, IdBebida, IdPostre, Descuento)
                             VALUES (@IdPlatoPrincipal, @IdBebida, @IdPostre, @Descuento)";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@IdPlatoPrincipal", dto.PlatoPrincipalId);
            command.Parameters.AddWithValue("@IdBebida", dto.BebidaId);
            command.Parameters.AddWithValue("@IdPostre", dto.PostreId);
            command.Parameters.AddWithValue("@Descuento", dto.Descuento);

            await command.ExecuteNonQueryAsync();
        }

        public async Task UpdateAsync(int id, ComboCreateDto dto)
        {
            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            string query = @"UPDATE Combo 
                             SET IdPlatoPrincipal = @IdPlatoPrincipal, 
                                 IdBebida = @IdBebida, 
                                 IdPostre = @IdPostre, 
                                 Descuento = @Descuento
                             WHERE Id = @Id";

            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@IdPlatoPrincipal", dto.PlatoPrincipalId);
            command.Parameters.AddWithValue("@IdBebida", dto.BebidaId);
            command.Parameters.AddWithValue("@IdPostre", dto.PostreId);
            command.Parameters.AddWithValue("@Descuento", dto.Descuento);

            await command.ExecuteNonQueryAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();

            string query = "DELETE FROM Combo WHERE Id = @Id";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            await command.ExecuteNonQueryAsync();
        }
    }
}
