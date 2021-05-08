/* 
  Classe para mapeamento ta tabela person.
*/
using RestWithASPNETUdemy.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNETUdemy.Model
{
    [Table("person")]
    public class Person : BaseEntity
    {

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
