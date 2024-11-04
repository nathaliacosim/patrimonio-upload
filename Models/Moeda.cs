using System;

namespace PatrimonioDourados.Models;
public class Moeda
{
    public int id { get; set; }
    public string nome { get; set; } = string.Empty;
    public string sigla { get; set; } = string.Empty;
    public string dtCotacao { get; set; }
    public decimal fatorConversao { get; set; }
    public string formaCalculo { get; set; } = string.Empty;
}