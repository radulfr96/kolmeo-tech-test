using Domain;

namespace DataLayer.Contracts
{
    public interface IProductDataLayer
    {
        public Task AddProductAsync(Product product);

        public Task DeleteProduct(Product author);

        public Task<Product> GetProduct(int id);

        public Task<List<Product>> GetProducts();
    }
}