using System;
using Dapper;
using PatrimonioDourados.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using PatrimonioDourados.Http;

namespace PatrimonioDourados.UseCase;

public class TombarBens
{
    private readonly DatabaseConnection _dbConnection;

    public TombarBens(DatabaseConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }


    public List<string> GetBensCloud()
    {
        try
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();

                string query = "SELECT id_cloud FROM bens_cloud;";

                var bens = connection.Query<string>(query).AsList();
                return bens;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao buscar bens: {ex.Message}");
            return new List<string>();
        }
    }

    public void AguardarTombamento(string token)
    {
        var itensBens = GetBensCloud();

        foreach (var item in itensBens)
        {
            Console.WriteLine(item);
            
            var url_base = "https://patrimonio.betha.cloud/patrimonio-services/api/bens/" + item + "/aguardarTombamento";
            Send(token, url_base);
        }
    }

    public void Send(string token, string url)
    {
        var requisicao = RequisicaoHttp.PostRequisicao(token, null, url);
        Console.WriteLine(requisicao.Result);
    }
}
