using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Contracts;

namespace Application.Commands.DeleteProductCommand
{
    public class DeleteProductCommand : IRequest<DeleteProductCommandResponse>
    {
        public int Id { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, DeleteProductCommandResponse>
    {
        private readonly IProductUnitofWork _productUnitofWork;

        public DeleteProductCommandHandler(IProductUnitofWork productUnitofWork)
        {
            _productUnitofWork = productUnitofWork;
        }

        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            var response = new DeleteProductCommandResponse();

            var product = await _productUnitofWork.ProductDataLayer.GetProductAsync(command.Id);

            if (product == null)
            {
                throw new ProductNotFoundException($"Unable to find product with id [{command.Id}]");
            }

            await _productUnitofWork.ProductDataLayer.DeleteProductAsync(product);
            await _productUnitofWork.SaveAsync();

            return response;
        }
    }
}
