using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GetProdutsQuery
{
    public class GetProductsQuery: IRequest<GetProductsQueryResponse>
    {
    }

    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, GetProductsQueryResponse>
    {
        public Task<GetProductsQueryResponse> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
