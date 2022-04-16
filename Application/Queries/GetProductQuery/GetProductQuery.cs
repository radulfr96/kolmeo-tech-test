using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GetProductQuery
{
    public class GetProductQuery : IRequest<GetProductQueryResponse>
    {
    }

    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, GetProductQueryResponse>
    {
        public Task<GetProductQueryResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
