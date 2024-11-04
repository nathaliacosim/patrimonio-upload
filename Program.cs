using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PatrimonioDourados.Controller;
using PatrimonioDourados.Models;
using PatrimonioDourados.UseCase;

namespace PatrimonioDourados
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            string cliente = "IP de Dourados";
            string sistema = "Patrimônio";
            string tipoJob = "envio de dados faltantes";

            Console.WriteLine("Olá, seja bem-vindo! :) ");
            Console.WriteLine("Vamos iniciar o processo...");
            Console.WriteLine($"Iniciando {tipoJob} no Sistema {sistema} na entidade {cliente}.");

            var csvController = new CsvController();
            List<BensCsv> bens;

            try
            {
                bens = csvController.LerCsv(@"C:\\Users\\DevStaf02\\Área de Trabalho\\bens.csv");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao ler o arquivo CSV: {ex.Message}");
                return;
            }

            Console.WriteLine("Configurando base de dados...");
            string host = "localhost";
            int port = 5432;
            string database = "IP_Dourados";
            string username = "postgres";
            string password = "root";

            var dbConnection = new DatabaseConnection(host, port, database, username, password);
            dbConnection.Connect();

            Console.WriteLine("Iniciando inserção de dados na base local...");

            try
            {
                string token = "Bearer token";

                var insertDBLocal = new PostBensPgsql(dbConnection);
                insertDBLocal.InserirBens(bens);
                Console.WriteLine("Dados inseridos com sucesso!");

                var postCloud = new PostBensCloud(dbConnection);
                postCloud.EnviarBens(token);

                var getDados = new GetBensCloud(dbConnection);
                getDados.GetBens(token);
                
                var atualizaDados = new PatchBens(dbConnection);
                await atualizaDados.AtualizaAsync(token);

                var tombamento = new TombarBens(dbConnection);
                tombamento.AguardarTombamento(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir dados na base: {ex.Message}");
            }

            Console.WriteLine("Processo concluído.");
        }
    }
}
