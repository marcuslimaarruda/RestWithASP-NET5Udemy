
using System.ComponentModel.DataAnnotations.Schema;


namespace RestWithASPNETUdemy.Model
{
    [Table("person")]
    public class Person
    {
        [Column("id")]
        public long id { get; set; }
        [Column("primeiro_nome")]
        public string primeiroNome { get; set; }
        [Column("ultimp_nome")]
        public string UltimoNome { get; set; }
        [Column("endereco")]
        public string Endereco { get; set; }
        [Column("Sexo")]
        public string Sexo { get; set; }
        
    }

}
