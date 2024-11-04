using System.Collections.Generic;

namespace PatrimonioDourados.Models;

public class JsonEnvio
{
    public int id { get; set; }
    public Tipo tipoBem { get; set; }
    public Tipo grupoBem { get; set; }
    public Tipo especieBem { get; set; }
    public Tipo tipoUtilizacaoBem { get; set; }
    public Tipo tipoAquisicao { get; set; }
    public Entidade fornecedor { get; set; }
    public Entidade responsavel { get; set; }
    public Tipo estadoConservacao { get; set; }
    public Tipo tipoComprovante { get; set; }
    public Entidade organograma { get; set; }
    public Entidade localizacaoFisica { get; set; }
    public int numeroRegistro { get; set; }
    public string numeroComprovante { get; set; }
    public string descricao { get; set; }
    public string dataInclusao { get; set; }
    public string dataAquisicao { get; set; }
    public bool consomeCombustivel { get; set; }
    public int qtdDiasGarantia { get; set; }
    public string dataInicioGarantia { get; set; }
    public string dataFinalGarantia { get; set; }
    public string dataProximaManutencao { get; set; }
    public string dtUltimaDepreciacao { get; set; }
    public SituacaoBem situacaoBem { get; set; }
    public string numeroPlaca { get; set; }
    public string observacao { get; set; }
    public Descricao numeroAnoProcesso { get; set; }
    public Descricao numeroAnoSolicitacao { get; set; }
    public List<Descricao> numeroAnoEmpenho { get; set; }
    public BemImovel bemImovel { get; set; }
    public BemValor bemValor { get; set; }
    public string numeroConvenio { get; set; }
}

public class Tipo
{
    public int id { get; set; }
}

public class Entidade
{
    public int id { get; set; }
}

public class SituacaoBem
{
    public string valor { get; set; }
    public string descricao { get; set; }
}

public class Descricao
{
    public string descricao { get; set; }
}

public class BemImovel
{
    public int id { get; set; }
    public CartorioRegistro cartorioRegistro { get; set; }
    public UnidadeMedida unidadeMedida { get; set; }
    public string denominacao { get; set; }
    public string finalidade { get; set; }
    public string dtMatricula { get; set; }
    public string matricula { get; set; }
    public string livro { get; set; }
    public string folha { get; set; }
    public string nroRegistorImovel { get; set; }
    public decimal areaImovel { get; set; }
    public decimal grauSul { get; set; }
    public decimal grauOeste { get; set; }
    public decimal minutoSul { get; set; }
    public decimal minutoOeste { get; set; }
    public decimal segundoSul { get; set; }
    public decimal segundoOeste { get; set; }
    public decimal latitude { get; set; }
    public decimal longitude { get; set; }
}

public class CartorioRegistro
{
    public int id { get; set; }
}

public class UnidadeMedida
{
    public int id { get; set; }
}

public class BemValor
{
    public int id { get; set; }
    public MetodoDepreciacao metodoDepreciacao { get; set; }
    public Moeda moeda { get; set; }
    public decimal vlAquisicao { get; set; }
    public decimal vlAquisicaoConvertido { get; set; }
    public decimal vlResidual { get; set; }
    public decimal vlDepreciavel { get; set; }
    public decimal vlDepreciado { get; set; }
    public decimal saldoDepreciar { get; set; }
    public decimal capacidadeProdutiva { get; set; }
    public decimal vlUltimaReavaliacao { get; set; }
    public decimal vlLiquidoContabil { get; set; }
    public int anosVidaUtil { get; set; }
    public decimal taxaDepreciacaoAnual { get; set; }
    public string dtInicioDepreciacao { get; set; }
    public string dtUltimaReavaliacao { get; set; }
}

public class MetodoDepreciacao
{
    public int id { get; set; }
}

public class Moeda
{
    public int id { get; set; }
    public string key { get; set; }
    public string nome { get; set; }
    public string sigla { get; set; }
    public string dtCotacao { get; set; }
    public decimal fatorConversao { get; set; }
    public string formaCalculo { get; set; }
    public int version { get; set; }
    public string templateId { get; set; }
}
