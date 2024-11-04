using System;
using CsvHelper;
using System.IO;
using System.Text;
using System.Globalization;
using CsvHelper.Configuration;
using System.Collections.Generic;
using PatrimonioDourados.Models;

namespace PatrimonioDourados.Controller;
public class CsvController
{
    public List<BensCsv> LerCsv(string caminhoArquivo)
    {
        var bens = new List<BensCsv>();

        try
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                BadDataFound = null,
                MissingFieldFound = null,
                HasHeaderRecord = false,

            };

            using (var reader = new StreamReader(caminhoArquivo, Encoding.GetEncoding("ISO-8859-1")))
            using (var csv = new CsvReader(reader, config))
            {
                while (csv.Read())
                {
                    var bem = new BensCsv
                    {
                        sequencial = csv.GetField<int>(0),
                        descricao = csv.GetField<string>(1),
                        exercicio = csv.GetField<int>(2),
                        classificacao_contabil = csv.GetField<int>(3),
                        conta_contabil = csv.GetField<string>(4),
                        valor_aquisicao = csv.GetField<double?>(5),
                        valor_residual = csv.GetField<double?>(6),
                        vida_util = csv.GetField<int?>(7),
                        taxa_depreciacao = csv.GetField<double?>(8),
                        data_atual = DateTime.FromOADate(csv.GetField<double>(9)),
                        ano_atual = csv.GetField<int?>(10),
                        valor_atual = csv.GetField<double?>(11)
                    };
                    bens.Add(bem);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao ler o arquivo: {ex.Message}");
        }

        return bens;
    }
}
