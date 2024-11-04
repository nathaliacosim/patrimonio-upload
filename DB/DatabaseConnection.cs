using System;
using Npgsql;
using System.Data;

public class DatabaseConnection
{
    private readonly string _connectionString;

    public DatabaseConnection(string host, int port, string database, string username, string password)
    {
        _connectionString = $"Host={host};Port={port};Database={database};Username={username};Password={password};";
    }


    public IDbConnection GetConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }


    public void Connect()
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            try
            {
                connection.Open();
                Console.WriteLine("Conexão estabelecida com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao conectar ao banco de dados: {ex.Message}");
            }
        }
    }

    public int ExecuteInsert(string query, object parameters)
    {
        using (var connection = new NpgsqlConnection(_connectionString))
        {
            try
            {
                connection.Open();
                using (var command = new NpgsqlCommand(query, connection))
                {
                    // Adiciona parâmetros ao comando
                    foreach (var prop in parameters.GetType().GetProperties())
                    {
                        command.Parameters.AddWithValue($"@{prop.Name}", prop.GetValue(parameters) ?? DBNull.Value);
                    }

                    // Retorna o ID do registro inserido
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao executar inserção no banco de dados: {ex.Message}");
                return -1; // Retorna um valor indicativo de erro
            }
        }
    }
}
