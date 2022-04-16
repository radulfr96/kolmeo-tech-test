using DataLayer;
using DataLayer.Contracts;
using Domain;
using UnitOfWork.Contracts;

namespace UnitOfWork
{
    public class ProductUnitOfWork : IProductUnitofWork, IDisposable
    {
        private readonly KolmeoContext _dbContext;
        private IProductDataLayer _productDataLayer;
        private bool disposed = false;

        public ProductUnitOfWork(KolmeoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IProductDataLayer ProductDataLayer
        {
            get
            {
                if (_productDataLayer == null)
                {
                    _productDataLayer = new ProductDataLayer(_dbContext);
                }
                return _productDataLayer;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                _dbContext.Dispose();
            }

            disposed = true;
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        ~ProductUnitOfWork()
        {
            Dispose(false);
        }
    }
}