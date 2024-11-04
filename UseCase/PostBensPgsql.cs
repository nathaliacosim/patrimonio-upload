using System;
using Dapper;
using System.Collections.Generic;
using PatrimonioDourados.Models;

namespace PatrimonioDourados.UseCase;

public class PostBensPgsql
{
    private readonly DatabaseConnection _dbConnection;

    public PostBensPgsql(DatabaseConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public void InserirBens(List<BensCsv> bens)
    {
        using (var connection = _dbConnection.GetConnection())
        {
            connection.Open();

            if (connection.State != System.Data.ConnectionState.Open)
            {
                throw new Exception("A conexão com o banco de dados não pôde ser aberta.");
            }

            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    foreach (var bem in bens)
                    {
                        DateTime dataAtual;
                        try
                        {
                            dataAtual = Convert.ToDateTime(bem.data_atual);
                        }
                        catch (FormatException)
                        {
                            throw new Exception($"Formato de data inválido: {bem.data_atual}");
                        }

                        var sql = @"
                                INSERT INTO bens (descricao, exercicio, classificacao_contabil, conta_contabil, 
                                                  valor_aquisicao, valor_residual, vida_util, taxa_depreciacao, 
                                                  data_atual, ano_atual, valor_atual)
                                VALUES (@descricao, @exercicio, @classificacao_contabil, @conta_contabil, 
                                        @valor_aquisicao, @valor_residual, @vida_util, @taxa_depreciacao, 
                                        @data_atual, @ano_atual, @valor_atual)";

                        connection.Execute(sql, new
                        {
                            descricao = bem.descricao,
                            exercicio = bem.exercicio,
                            classificacao_contabil = bem.classificacao_contabil,
                            conta_contabil = bem.conta_contabil,
                            valor_aquisicao = bem.valor_aquisicao,
                            valor_residual = bem.valor_residual,
                            vida_util = bem.vida_util,
                            taxa_depreciacao = bem.taxa_depreciacao,
                            data_atual = dataAtual.ToString("yyyy-MM-dd"),
                            ano_atual = bem.ano_atual,
                            valor_atual = bem.valor_atual
                        }, transaction);
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception("Erro ao inserir dados: " + ex.Message);
                }
            }
        }
    }
}
