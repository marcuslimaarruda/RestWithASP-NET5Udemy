
using RestWithASPNETUdemy.Model.Base;
using RestWithASPNETUdemy.Model.Context;
using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RestWithASPNETUdemy.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {

        private MySQLContext _context;

        private DbSet<T> dataset;

        public GenericRepository(MySQLContext context)
        {
            _context = context;
            dataset = _context.Set<T>();
        }


        public T Create(T item)
        {
            try
            {
                dataset.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public List<T> FindAll()
        {
            return dataset.ToList();
        }

        public T FindById(long id)
        {
            return dataset.SingleOrDefault(p => p.id.Equals(id));
        }

        public T Update(T item)
        {
            var result = dataset.SingleOrDefault(p => p.id.Equals(item.id));

            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return result;

        }
        public void Delete(long id)
        {
            var result = dataset.SingleOrDefault(p => p.id.Equals(id));

            if (result != null)
            {
                try
                {
                    dataset.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public bool Exists(long id)
        {
            return dataset.Any(p => p.id.Equals(id));
        }

    }


}
