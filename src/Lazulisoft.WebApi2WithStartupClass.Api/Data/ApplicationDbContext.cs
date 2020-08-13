using Lazulisoft.WebApi2WithStartupClass.Api.Models;
using System.Data.Entity;

namespace Lazulisoft.WebApi2WithStartupClass.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=DefaultConnection")
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}