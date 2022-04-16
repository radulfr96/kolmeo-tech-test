using Domain;

namespace DataLayer.Contracts
{
    public interface IProductDataLayer
    {
        public Task AddProductAsync(Product product);

        public Task DeleteProductAsync(Product author);

        public Task<Product> GetProductAsync(int id);

        public Task<List<Product>> GetProductsAsync();
    }
}