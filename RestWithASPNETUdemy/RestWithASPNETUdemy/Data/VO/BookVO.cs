using RestWithASPNETUdemy.Hypermedia;
using RestWithASPNETUdemy.Hypermedia.Abstract;
using System;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Model
{
    public class BookVO : ISupportsHyperMedia
    {
        public long id { get; set; }
        public string Tiulo { get; set; }
        public string Author { get; set; }
        public DateTime DataLancamento { get; set; }
        public double Preco { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
