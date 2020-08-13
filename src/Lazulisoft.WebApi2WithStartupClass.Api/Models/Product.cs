namespace Lazulisoft.WebApi2WithStartupClass.Api.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public ProductCategory Category { get; set; }
    }
}