using RestWithASPNETUdemy.Data.Converter.Contract;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Model;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNETUdemy.Data.Converter.Implementations
{
    public class BookConverter : IParser<BookVO, Book>, IParser<Book, BookVO>
    {
        public Book Parse(BookVO origin)
        {
            if (origin == null) return null;
            return new Book
            {
                id = origin.id,
                Tiulo = origin.Tiulo,
                Author = origin.Author,
                DataLancamento = origin.DataLancamento,
                Preco = origin.Preco
            };
        }

        public BookVO Parse(Book origin)
        {
            if (origin == null) return null;
            return new BookVO
            {
                id = origin.id,
                Tiulo = origin.Tiulo,
                Author = origin.Author,
                DataLancamento = origin.DataLancamento,
                Preco = origin.Preco
            };
        }

        public List<Book> Parse(List<BookVO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }


        public List<BookVO> Parse(List<Book> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
