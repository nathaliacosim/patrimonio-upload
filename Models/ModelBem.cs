using PatrimonioDourados.Models;
using System.Collections.Generic;

namespace PatrimonioDourados.Models;
public class ModelBem
{
    public int offset { get; set; }
    public int limit { get; set; }
    public bool hasNext { get; set; }
    public List<ContentItem> content { get; set; } = new List<ContentItem>();
    public int total { get; set; }
    public object valor { get; set; }
    public object soma { get; set; }
    public object dados { get; set; }
}