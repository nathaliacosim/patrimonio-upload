using System;

namespace PatrimonioDourados.Models;

public class BensCsv
{
    public int sequencial { get; set; }
    public string descricao { get; set; }
    public int exercicio { get; set; }
    public int classificacao_contabil { get; set; }
    public string conta_contabil { get; set; }
    public double? valor_aquisicao { get; set; }
    public double? valor_residual { get; set; }
    public int? vida_util { get; set; }
    public double? taxa_depreciacao { get; set; }
    public DateTime? data_atual {  get; set; }
    public int? ano_atual { get; set; }
    public double? valor_atual { get; set; }
}