using Lazulisoft.WebApi2WithStartupClass.Api.Models;
using System.Collections.Generic;

namespace Lazulisoft.WebApi2WithStartupClass.Api.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);
        void Commit();
    }
}