using Application.Commands.AddProductCommand;
using Application.Commands.DeleteProductCommand;
using Application.Commands.UpdateProductCommand;
using Application.Queries.GetProductQuery;
using Application.Queries.GetProdutsQuery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KolmeoTestAPI.Controllers
{
    [ApiExceptionFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Used to add a new product
        /// </summary>
        /// <param name="request">The request with the product information</param>
        /// <returns>Response that indicates the result</returns>
        [HttpPost("")]
        public async Task<AddProductCommandResponse> AddProduct([FromBody] AddProductCommand command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Used to get products
        /// </summary>
        /// <returns>Response that indicates the result</returns>
        [HttpGet("")]
        public async Task<GetProductsQueryResponse> GetProducts()
        {
            return await _mediator.Send(new GetProductsQuery());
        }

        /// <summary>
        /// Used to get a specific product
        /// </summary>
        /// <param name="id">the id of the product to be retreived</param>
        /// <returns>Response that indicates the result</returns>
        [HttpGet("{id}")]
        public async Task<GetProductQueryResponse> GetProduct([FromRoute] int id)
        {
            return await _mediator.Send(new GetProductQuery() { Id = id });
        }

        /// <summary>
        /// Used to update a product
        /// </summary>
        /// <param name="request">The information used to update the product</param>
        /// <returns>Response that indicates the result</returns>
        [HttpPatch("")]
        public async Task<UpdateProductCommandResponse> UpdateProduct([FromBody] UpdateProductCommand command)
        {
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Used to remove a product from the system
        /// </summary>
        /// <param name="id">The id of the product to be deleted</param>
        /// <returns>Response that indicates the result</returns>
        [HttpDelete("{id}")]
        public async Task<DeleteProductCommandResponse> DeleteAuthor([FromRoute] int id)
        {
            return await _mediator.Send(new DeleteProductCommand() { Id = id });
        }
    }
}
