using Foo_Form.Models;
using Microsoft.EntityFrameworkCore;

namespace Foo_Form.Data
{
    public class FooFormDbContext : DbContext
    {
        public FooFormDbContext(DbContextOptions<FooFormDbContext> options) : base(options)
        {
            
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Record> Records { get; set; }
    }
}
