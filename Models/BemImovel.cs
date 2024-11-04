using System;

namespace PatrimonioDourados.Models;
public class BemImovel
{
    public int? id { get; set; }
    public CartorioRegistro cartorioRegistro { get; set; } = null;
    public UnidadeMedida unidadeMedida { get; set; } = null;
    public string denominacao { get; set; } = string.Empty;
    public string finalidade { get; set; } = string.Empty;
    public DateTime? dtMatricula { get; set; }
    public string matricula { get; set; } = string.Empty;
    public string livro { get; set; } = string.Empty;
    public string folha { get; set; } = string.Empty;
    public string nroRegistroImovel { get; set; } = string.Empty;
    public decimal? areaImovel { get; set; }
    public decimal? grauSul { get; set; }
    public decimal? grauOeste { get; set; }
    public decimal? minutoSul { get; set; }
    public decimal? minutoOeste { get; set; }
    public decimal? segundoSul { get; set; }
    public decimal? segundoOeste { get; set; }
    public decimal? latitude { get; set; }
    public decimal? longitude { get; set; }
}