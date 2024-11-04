using PatrimonioDourados.Models;
using System.Collections.Generic;
using System;

namespace PatrimonioDourados.Models;
public class ContentItem
{
    public int id { get; set; }
    public TipoBem tipoBem { get; set; } = new TipoBem();
    public GrupoBem grupoBem { get; set; } = new GrupoBem();
    public EspecieBem especieBem { get; set; } = new EspecieBem();
    public TipoUtilizacaoBem tipoUtilizacaoBem { get; set; } = new TipoUtilizacaoBem();
    public TipoAquisicao tipoAquisicao { get; set; } = new TipoAquisicao();
    public Fornecedor fornecedor { get; set; } = null;
    public Responsavel responsavel { get; set; } = null;
    public EstadoConservacao estadoConservacao { get; set; } = new EstadoConservacao();
    public TipoComprovante tipoComprovante { get; set; } = null;
    public Organograma organograma { get; set; } = null;
    public LocalizacaoFisica localizacaoFisica { get; set; } = null;
    public int? numeroRegistro { get; set; } = null;
    public string numeroComprovante { get; set; } = null;
    public string descricao { get; set; } = string.Empty;
    public DateTime dataInclusao { get; set; }
    public DateTime dataAquisicao { get; set; }
    public bool consomeCombustivel { get; set; }
    public int? qtdDiasGarantia { get; set; } = null;
    public DateTime? dataInicioGarantia { get; set; } = null;
    public DateTime? dataFinalGarantia { get; set; } = null;
    public DateTime? dataProximaManutencao { get; set; } = null;
    public DateTime? dtUltimaDepreciacao { get; set; } = null;
    public SituacaoBem situacaoBem { get; set; } = new SituacaoBem();
    public string numeroPlaca { get; set; } = null;
    public string observacao { get; set; } = null;
    public NumeroAnoProcesso numeroAnoProcesso { get; set; } = null;
    public NumeroAnoSolicitacao numeroAnoSolicitacao { get; set; } = null;
    public List<NumeroAnoEmpenho> numeroAnoEmpenho { get; set; } = new List<NumeroAnoEmpenho>();
    public BemImovel bemImovel { get; set; } = null;
    public BemValor bemValor { get; set; } = null;
    public object numeroConvenio { get; set; } = null;
}
