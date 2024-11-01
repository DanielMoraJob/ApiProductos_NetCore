using ApiProductos.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ApiProductos.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
    }
}
