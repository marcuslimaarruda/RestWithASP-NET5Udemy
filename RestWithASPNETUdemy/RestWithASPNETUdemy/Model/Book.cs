/* 
  Classe para mapeamento ta tabela Books.
*/
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNETUdemy.Model
{
    [Table("books")]
    public class Book
    {
        [Column("id")]
        public int id { get; set; }

        [Column("author")]
        public string Author { get; set; }

        [Column("launch_date")]
        public  DateTime DataLancamento{ get; set; }

        [Column("price")]
        public double Preco { get; set; }
        
        [Column("title")]
        public string Tiulo { get; set; }        
    }
}
