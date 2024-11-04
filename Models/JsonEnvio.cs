using System.Collections.Generic;

namespace PatrimonioDourados.Models;

public class JsonEnvio
{
    public TipoBem tipoBem { get; set; }
    public GrupoBem grupoBem { get; set; }
    public EspecieBem especieBem { get; set; }
    public TipoUtilizacaoBem tipoUtilizacaoBem { get; set; }
    public TipoAquisicao tipoAquisicao { get; set; }
    public EstadoConservacao estadoConservacao { get; set; }
    public string descricao { get; set; }
    public string dataInclusao { get; set; }
    public string dataAquisicao { get; set; }
    public bool consomeCombustivel { get; set; }
    public SituacaoBem situacaoBem { get; set; }
    public BemValor bemValor { get; set; }
}