using System;

namespace PatrimonioDourados.Models;
public class BemValor
{
    public int? id { get; set; } = null;
    public MetodoDepreciacao metodoDepreciacao { get; set; } = new MetodoDepreciacao();
    public Moeda moeda { get; set; } = new Moeda();
    public double? vlAquisicao { get; set; } = null;
    public double? vlAquisicaoConvertido { get; set; } = null;
    public double? vlResidual { get; set; } = null;
    public double? vlDepreciavel { get; set; } = null;
    public double? vlDepreciado { get; set; } = null;
    public double? saldoDepreciar { get; set; } = null;
    public double? capacidadeProdutiva { get; set; } = null;
    public double? vlUltimaReavaliacao { get; set; } = null;
    public double? vlLiquidoContabil { get; set; } = null;
    public int? anosVidaUtil { get; set; } = null;
    public double? taxaDepreciacaoAnual { get; set; } = null;
    public string? dtInicioDepreciacao { get; set; } = null;
    public string? dtUltimaReavaliacao { get; set; } = null;
}