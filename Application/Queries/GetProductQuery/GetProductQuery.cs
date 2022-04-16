using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Contracts;

namespace Application.Queries.GetProductQuery
{
    public class GetProductQuery : IRequest<GetProductQueryResponse>
    {
        public int Id { get; set; }
    }

    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, GetProductQueryResponse>
    {
        private readonly IProductUnitofWork _productUnitofWork;

        public GetProductQueryHandler(IProductUnitofWork productUnitofWork)
        {
            _productUnitofWork = productUnitofWork;
        }

        public async Task<GetProductQueryResponse> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {
            var response = new GetProductQueryResponse();

            var product = await _productUnitofWork.ProductDataLayer.GetProductAsync(query.Id);

            if (product == null)
            {
                throw new ProductNotFoundException($"Unable to find product with id [{query.Id}]");
            }

            response.Product = product;

            return response;
        }
    }
}
