
using System.Text.Json.Serialization;

namespace RestWithASPNETUdemy.Data.VO
{

    public class PersonVO
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
    }
}
