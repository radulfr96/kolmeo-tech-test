using DataLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork.Contracts
{
    public interface IProductUnitofWork
    {
        /// <summary>
        /// Data layer for products
        /// </summary>
        IProductDataLayer ProductDataLayer { get; }

        /// <summary>
        /// Used to save data changes
        /// </summary>
        Task SaveAsync();
    }
}
