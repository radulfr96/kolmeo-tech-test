using DataLayer.Contracts;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ProductDataLayer : IProductDataLayer
    {
        private readonly KolmeoContext _context;

        public ProductDataLayer(KolmeoContext context)
        {
            _context = context;
        }

        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
        }

        public async Task DeleteProduct(Product author)
        {
            await Task.FromResult(_context.Products.Remove(author));
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
