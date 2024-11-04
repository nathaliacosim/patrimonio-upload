using System;
using System.Linq;
using Newtonsoft.Json;
using PatrimonioDourados.Http;
using PatrimonioDourados.Models;

namespace PatrimonioDourados.UseCase;

public class GetBensCloud
{
    private readonly DatabaseConnection _dbConnection;

    public GetBensCloud(DatabaseConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public void GetBens(string token)
    {
        const string url_base = "https://patrimonio.betha.cloud/patrimonio-services/api/bens";
        int offset = 0;
        int limit = 25;

        while (true)
        {
            var parametros = $"limit={limit}&offset={offset}";

            var json_data = RequisicaoHttp.GetRequisicao(token, $"{url_base}?{parametros}");
            var json_dataRet = JsonConvert.DeserializeObject<ModelBem>(json_data.Result);

            if (json_dataRet == null || json_dataRet.content == null || json_dataRet.content.Count == 0)
                break;

            json_dataRet.content.ForEach(betha =>
            {
                InserirBem(betha);
            });

            if (!json_dataRet.hasNext)
                break;

            offset += limit;
        }

    }

    private void InserirBem(ContentItem betha)
    {
        var numeroEmp = betha.numeroAnoEmpenho != null && betha.numeroAnoEmpenho.Any()
            ? string.Join(", ", betha.numeroAnoEmpenho.Select(e => e.descricao))
            : string.Empty;

        const string insertQuery = @"INSERT INTO bens_cloud (
                                        id_cloud,
                                        tipo_bem_id, grupo_bem_id, especie_bem_id, tipo_utilizacao_bem_id,
                                        tipo_aquisicao_id, fornecedor_id, responsavel_id, estado_conservacao_id,
                                        tipo_comprovante_id, organograma_id, localizacao_fisica_id, 
                                        numero_registro, numero_comprovante, descricao,
                                        data_inclusao, data_aquisicao, consome_combustivel,
                                        qtd_dias_garantia, data_inicio_garantia, data_final_garantia,
                                        data_proxima_manutencao, dt_ultima_depreciacao,
                                        situacao_bem_valor, numero_placa, observacao, 
                                        numero_processo, numero_solicitacao, numero_empenho,
                                        metodo_depreciacao, valor_aquisicao, valor_aquisicao_convertido,
                                        valor_residual, valor_depreciavel, valor_depreciado, 
                                        saldo_depreciar, valor_liquido, taxa_depreciacao_anula, 
                                        numero_convenio)
                                    VALUES (
                                        @id_cloud,
                                        @tipo_bem_id, @grupo_bem_id, @especie_bem_id, @tipo_utilizacao_bem_id,
                                        @tipo_aquisicao_id, @fornecedor_id, @responsavel_id, @estado_conservacao_id,
                                        @tipo_comprovante_id, @organograma_id, @localizacao_fisica_id, 
                                        @numero_registro, @numero_comprovante, @descricao,
                                        @data_inclusao, @data_aquisicao, @consome_combustivel,
                                        @qtd_dias_garantia, @data_inicio_garantia, @data_final_garantia,
                                        @data_proxima_manutencao, @dt_ultima_depreciacao,
                                        @situacao_bem_valor, @numero_placa, @observacao, 
                                        @numero_processo, @numero_solicitacao, @numero_empenho,
                                        @metodo_depreciacao, @valor_aquisicao, @valor_aquisicao_convertido,
                                        @valor_residual, @valor_depreciavel, @valor_depreciado, 
                                        @saldo_depreciar, @valor_liquido, @taxa_depreciacao_anula, 
                                        @numero_convenio)";

        var parameters = new
        {
            id_cloud = betha.id,
            tipo_bem_id = betha.tipoBem?.id ?? 0,
            grupo_bem_id = betha.grupoBem?.id ?? 0,
            especie_bem_id = betha.especieBem?.id ?? 0,
            tipo_utilizacao_bem_id = betha.tipoUtilizacaoBem?.id ?? 0,
            tipo_aquisicao_id = betha.tipoAquisicao?.id ?? 0,
            fornecedor_id = betha.fornecedor?.id ?? (int?)null,
            responsavel_id = betha.responsavel?.id ?? (int?)null,
            estado_conservacao_id = betha.estadoConservacao?.id ?? 0,
            tipo_comprovante_id = betha.tipoComprovante?.id ?? (int?)null,
            organograma_id = betha.organograma?.id ?? (int?)null,
            localizacao_fisica_id = betha.localizacaoFisica?.id ?? (int?)null,
            numero_registro = betha.numeroRegistro,
            numero_comprovante = betha.numeroComprovante,
            descricao = betha.descricao,
            data_inclusao = betha.dataInclusao,
            data_aquisicao = betha.dataAquisicao,
            consome_combustivel = betha.consomeCombustivel,
            qtd_dias_garantia = betha.qtdDiasGarantia,
            data_inicio_garantia = betha.dataInicioGarantia,
            data_final_garantia = betha.dataFinalGarantia,
            data_proxima_manutencao = betha.dataProximaManutencao,
            dt_ultima_depreciacao = betha.dtUltimaDepreciacao,
            situacao_bem_valor = betha.situacaoBem?.valor,
            numero_placa = betha.numeroPlaca ?? string.Empty,
            observacao = betha.observacao ?? string.Empty,
            numero_processo = betha.numeroAnoProcesso?.descricao ?? string.Empty,
            numero_solicitacao = betha.numeroAnoSolicitacao?.descricao ?? string.Empty,
            numero_empenho = numeroEmp,
            metodo_depreciacao = betha.bemValor?.metodoDepreciacao?.id ?? 0,
            valor_aquisicao = betha.bemValor?.vlAquisicao ?? 0,
            valor_aquisicao_convertido = betha.bemValor?.vlAquisicaoConvertido ?? 0,
            valor_residual = betha.bemValor?.vlResidual ?? 0,
            valor_depreciavel = betha.bemValor?.vlDepreciavel ?? 0,
            valor_depreciado = betha.bemValor?.vlDepreciado ?? 0,
            saldo_depreciar = betha.bemValor?.saldoDepreciar ?? 0,
            valor_liquido = betha.bemValor?.vlLiquidoContabil ?? 0,
            taxa_depreciacao_anula = betha.bemValor?.taxaDepreciacaoAnual ?? 0,
            numero_convenio = betha.numeroConvenio ?? string.Empty,
        };

        try
        {
            _dbConnection.ExecuteInsert(insertQuery, parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao inserir bem: {ex.Message}");
        }
    }
}
