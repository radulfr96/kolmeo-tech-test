using Domain.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Contracts;

namespace Application.Commands.UpdateProductCommand
{
    public class UpdateProductCommand : IRequest<UpdateProductCommandResponse>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductCommandResponse>
    {

        private readonly IProductUnitofWork _productUnitofWork;

        public UpdateProductCommandHandler(IProductUnitofWork productUnitofWork)
        {
            _productUnitofWork = productUnitofWork;
        }

        public async Task<UpdateProductCommandResponse> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            var response = new UpdateProductCommandResponse();

            var product = await  _productUnitofWork.ProductDataLayer.GetProductAsync(command.Id);

            if (product == null)
            {
                throw new ProductNotFoundException($"Unable to find product with id [{command.Id}]");
            }

            product.Name = command.Name;
            product.Description = command.Description;
            product.Price = command.Price;

            return response;
        }
    }
}
