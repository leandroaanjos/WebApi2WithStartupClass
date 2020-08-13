using Lazulisoft.WebApi2WithStartupClass.Api.Data;
using Lazulisoft.WebApi2WithStartupClass.Api.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Lazulisoft.WebApi2WithStartupClass.Api.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public IEnumerable<Product> GetAll()
        {
            return _db.Products;
        }

        public Product GetById(int id)
        {
            return _db.Products.FirstOrDefault(p => p.Id == id);
        }

        public void Add(Product product)
        {
            _db.Products.Add(product);
        }

        public void Update(Product product)
        {
            _db.Entry(product).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var entity = _db.Products.Find(id);
            Delete(entity);
        }

        public void Delete(Product product)
        {
            if (_db.Entry(product).State == EntityState.Detached)
                _db.Products.Attach(product);

            _db.Entry(product).State = EntityState.Deleted;
        }

        public void Commit()
        {
            _db.SaveChanges();
        }
    }
}