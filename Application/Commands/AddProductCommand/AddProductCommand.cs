using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWork.Contracts;

namespace Application.Commands.AddProductCommand
{
    public class AddProductCommand : IRequest<AddProductCommandResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }

    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, AddProductCommandResponse>
    {
        private readonly IProductUnitofWork _productUnitofWork;

        public AddProductCommandHandler(IProductUnitofWork productUnitofWork)
        {
            _productUnitofWork = productUnitofWork;
        }

        public async Task<AddProductCommandResponse> Handle(AddProductCommand command, CancellationToken cancellationToken)
        {
            var response = new AddProductCommandResponse();

            var product = new Product()
            {
                Description = command.Description,
                Name = command.Name,
                Price = command.Price,
            };

            await _productUnitofWork.ProductDataLayer.AddProductAsync(product);
            await _productUnitofWork.SaveAsync();

            response.Id = product.Id;

            return response;
        }
    }
}
