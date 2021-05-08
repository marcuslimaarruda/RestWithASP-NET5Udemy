using System;

namespace RestWithASPNETUdemy.Model
{
    public class BookVO
    {
        public long id { get; set; }
        public string Tiulo { get; set; }
        public string Author { get; set; }
        public DateTime DataLancamento { get; set; }
        public double Preco { get; set; }
    }
}
