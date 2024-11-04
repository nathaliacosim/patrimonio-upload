using System;
using Dapper;
using Newtonsoft.Json;
using PatrimonioDourados.Models;
using System.Collections.Generic;
using PatrimonioDourados.Http;
using Newtonsoft.Json.Linq;

namespace PatrimonioDourados.UseCase;

public class PostBensCloud
{
    private readonly DatabaseConnection _dbConnection;

    public PostBensCloud(DatabaseConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public List<BensCsv> SelectBens()
    {
        try
        {
            using (var connection = _dbConnection.GetConnection())
            {
                connection.Open();

                string query = "SELECT * FROM bens WHERE classificacao_contabil = 6";

                var bens = connection.Query<BensCsv>(query).AsList();
                return bens;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao buscar bens: {ex.Message}");
            return new List<BensCsv>();
        }
    }

    public void EnviarBens(string token)
    {
        var bensCsvList = SelectBens();

        List<JsonEnvio> jsonEnvioList = new List<JsonEnvio>();

        foreach (var bensCsv in bensCsvList)
        {
            int id_tipo_bem = 4472; // Móveis
            int id_especie_bem = 539060;
            double? taxa_depreciacao = 10.00;
            int id_grupo_bem = 51839; // 1: Móveis e Utensílios (10 anos)
            int? utilizacao = null; // Inicia como null
            bool combustivel = false;
            int? id_depreciacao = 2086;

            switch (bensCsv.classificacao_contabil)
            {
                case 2:
                    id_grupo_bem = 51840;
                    id_especie_bem = 539063;
                    break;
                case 3:
                    id_grupo_bem = 51841;
                    id_especie_bem = 539062;
                    break;
                case 4:
                    id_grupo_bem = 51843;
                    id_especie_bem = 539067;
                    combustivel = true;
                    break;
                case 5:
                    id_grupo_bem = 51842;
                    id_especie_bem = 539061;
                    taxa_depreciacao = 5.00;
                    break;
                case 6:
                    id_grupo_bem = 51844;
                    id_tipo_bem = 4471;
                    id_especie_bem = 539066;
                    utilizacao = 4358;
                    taxa_depreciacao = null;
                    id_depreciacao = null;
                    break;
                case 7:
                    id_grupo_bem = 51845;
                    id_especie_bem = 539059;
                    taxa_depreciacao = 5.00;
                    break;
                case 8:
                    id_grupo_bem = 51846;
                    id_especie_bem = 539065;
                    break;
            }

            JsonEnvio jsonEnvio = new JsonEnvio
            {
                tipoBem = new TipoBem { id = id_tipo_bem },
                grupoBem = new GrupoBem { id = id_grupo_bem },
                especieBem = new EspecieBem { id = id_especie_bem },
                tipoUtilizacaoBem = utilizacao.HasValue ? new TipoUtilizacaoBem { id = utilizacao.Value } : null,
                tipoAquisicao = new TipoAquisicao { id = 5891 },
                estadoConservacao = new EstadoConservacao { id = 4192 },
                descricao = bensCsv.descricao,
                dataInclusao = "2024-10-23",
                dataAquisicao = $"{bensCsv.exercicio}-01-01",
                consomeCombustivel = combustivel,
                situacaoBem = new SituacaoBem
                {
                    valor = "EM_EDICAO",
                    descricao = "Em edição"
                },
                bemValor = new BemValor
                {
                    metodoDepreciacao = id_depreciacao.HasValue ? new MetodoDepreciacao { id = id_depreciacao.Value } : null,
                    moeda = new Moeda
                    {
                        id = 8,
                        nome = "R$ - Real (1994)",
                        sigla = "R$",
                        dtCotacao = "1994-07-01",
                        fatorConversao = 2750,
                        formaCalculo = "DIVIDIR"
                    },
                    vlAquisicao = bensCsv.valor_aquisicao,
                    vlAquisicaoConvertido = bensCsv.valor_aquisicao,
                    vlResidual = (bensCsv.valor_aquisicao/100),
                    vlDepreciavel = (bensCsv.valor_aquisicao - (bensCsv.valor_aquisicao / 100)),
                    vlDepreciado = 0.00,
                    saldoDepreciar = (bensCsv.valor_aquisicao - (bensCsv.valor_aquisicao / 100)),
                    anosVidaUtil = bensCsv.vida_util,
                    taxaDepreciacaoAnual = taxa_depreciacao,
                    dtInicioDepreciacao = $"{bensCsv.exercicio}-01-01",
                    vlLiquidoContabil = bensCsv.valor_aquisicao,
                },
                
            };

            var dadosJson = JsonConvert.SerializeObject(jsonEnvio);
            Console.WriteLine(dadosJson);

            Send(token, dadosJson);
        }
    }

    public void Send(string token, string dados)
    {
        var requisicao = RequisicaoHttp.PostRequisicao(token, dados, "https://patrimonio.betha.cloud/patrimonio-services/api/bens");
        Console.WriteLine(requisicao.Result);
    }
}
