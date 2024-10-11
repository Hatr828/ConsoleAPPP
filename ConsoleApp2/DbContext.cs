using BankTransactionExample;
using Microsoft.EntityFrameworkCore;
using Shop.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;

namespace Shop
{
    public class DbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("###");
        }
    }
}
