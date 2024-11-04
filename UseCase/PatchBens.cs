using System;
using Dapper;
using PatrimonioDourados.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using PatrimonioDourados.Http;
using System.Threading.Tasks;

namespace PatrimonioDourados.UseCase;

public class PatchBens
{
    private readonly DatabaseConnection _dbConnection;

    public PatchBens(DatabaseConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public List<BemPlaca> PatchItensBens()
    {
        try
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();
                string query = "SELECT id_cloud, numero_placa FROM bens_cloud";
                var bens = connection.Query<BemPlaca>(query).AsList();
                return bens;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao buscar bens: {ex.Message}");
            return new List<BemPlaca>();
        }
    }

    public async Task AtualizaAsync(string token)
    {
        var itensBens = PatchItensBens();

        using (var connection = _dbConnection.GetConnection())
        {
            connection.Open();

            foreach (var item in itensBens)
            {
                Console.WriteLine($"ID: {item.id_cloud}, Placa: {item.numero_placa}");

                AtualizaPlacas obj = new AtualizaPlacas
                {
                    numeroPlaca = item.numero_placa
                };

                var dadosJson = JsonConvert.SerializeObject(obj);
                Console.WriteLine($"Dados JSON: {dadosJson}");

                // Insere ou ignora se já existir
                string insertQuery = @"
                    INSERT INTO bens_placas (id_cloud, numero_placa) 
                    VALUES (@IdCloud, @NumeroPlaca)";

                var parameters = new { IdCloud = item.id_cloud, NumeroPlaca = item.numero_placa  };
                connection.Execute(insertQuery, parameters);

                var url_base = $"https://patrimonio.betha.cloud/patrimonio-services/api/bens/{item.id_cloud}";
                await SendAsync(token, dadosJson, url_base); 
            }
        }
    }

    public async Task SendAsync(string token, string dados, string url)
    {
        try
        {
            var requisicao = await RequisicaoHttp.PatchRequisicao(token, dados, url);
            Console.WriteLine($"Resposta da requisição: {requisicao}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao enviar requisição: {ex.Message}");
        }
    }
}
