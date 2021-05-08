
using RestWithASPNETUdemy.Hypermedia;
using RestWithASPNETUdemy.Hypermedia.Abstract;
using System.Collections.Generic;

namespace RestWithASPNETUdemy.Data.VO
{

    public class PersonVO : ISupportsHyperMedia
    {
        //[JsonPropertyName("Codigo")]    
        public long id { get; set; }
        //[JsonPropertyName("Primeiro Nome")]
        public string primeiroNome { get; set; }
        //[JsonPropertyName("Ultimo Nome")]
        public string UltimoNome { get; set; }
        public string Endereco { get; set; }
        //[JsonIgnore]
        public string Sexo { get; set; }

        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
