using RestWithASPNETUdemy.Model;
using RestWithASPNETUdemy.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithASPNETUdemy.Repository.Implementations
{
    public class BookRepositoryImplementation : IBookRepository
    {
        private MySQLContext _context;

        public BookRepositoryImplementation(MySQLContext context)
        {
            _context = context;
        }

        public Book Create(Book book)
        {
            try
            {
                _context.Add(book);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
            return book;
        }

        public void Delete(long id)
        {
            var result = _context.Books.SingleOrDefault(p => p.id.Equals(id));

            if (result != null)
            {
                try
                {
                    _context.Books.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
        public List<Book> FindAll()
        {

            return _context.Books.ToList();
            //return null;
        }

        public Book FindById(long id)
        {
            return _context.Books.SingleOrDefault(p => p.id.Equals(id));
     
        }

        public Book Update(Book book)
        {
            if (!Exists(book.id)) return null;

            var result = _context.Books.SingleOrDefault(p => p.id.Equals(book.id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(book);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    throw;
                }
            }

    
            return book;
        }

        private bool Exists(long id)
        {
            return _context.Books.Any(p => p.id.Equals(id));
        }
    }
}
