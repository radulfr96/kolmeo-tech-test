using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Contracts;

namespace Application.Queries.GetProdutsQuery
{
    public class GetProductsQuery: IRequest<GetProductsQueryResponse>
    {
    }

    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, GetProductsQueryResponse>
    {
        private readonly IProductUnitofWork _productUnitofWork;

        public GetProductsQueryHandler(IProductUnitofWork productUnitofWork)
        {
            _productUnitofWork = productUnitofWork;
        }

        public async Task<GetProductsQueryResponse> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return new GetProductsQueryResponse()
            {
                Products = await _productUnitofWork.ProductDataLayer.GetProductsAsync()
            };
        }
    }
}
