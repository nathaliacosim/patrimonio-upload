using System.Collections.Generic;

namespace PatrimonioDourados.Models;
public class JsonRetornoCloud
{
    public string? id { get; set; }
    public string? status { get; set; }
    public List<JsonRetorno>? retorno { get; set; }
}