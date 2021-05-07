
using Microsoft.EntityFrameworkCore;

namespace RestWithASPNETUdemy.Model.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext() { }

        public MySQLContext(DbContextOptions<MySQLContext> options) : base (options) { }

        public Microsoft.EntityFrameworkCore.DbSet<Person> Persons { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Book> Books { get; set; }

    }
}
