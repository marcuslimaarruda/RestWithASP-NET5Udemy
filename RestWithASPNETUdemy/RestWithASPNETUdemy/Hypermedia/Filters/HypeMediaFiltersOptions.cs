using RestWithASPNETUdemy.Hypermedia.Abstract;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Hypermedia.Filters
{
    public class HypeMediaFiltersOptions
    {
        public List<IResponseEnricher> ContentResponseEnricherList { get; set; } = new List<IResponseEnricher>();
    }
}
