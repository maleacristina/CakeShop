using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MiroBello.Models;

namespace MiroBello.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext>options ):base(options) { }

       
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductsOnBills> ProductsOnBill { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<ClientAccount> ClientAccounts { get; set; }
        public DbSet<ClientCart> ClientCart { get; set; }
        public DbSet<ProductsOnCart> ProductsOnCart { get; set; }

   

        

    }
}
