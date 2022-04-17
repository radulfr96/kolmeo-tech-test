using Application.Commands.AddProductCommand;
using Application.Commands.DeleteProductCommand;
using Bogus;
using Domain;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTest
{
    [Collection("IntegrationTestCollection")]
    public class DeleteProductCommandTest
    {
        private readonly KolmeoContext _context;
        private readonly IMediator _mediatr;
        private readonly Faker _faker;

        public DeleteProductCommandTest(TestFixture fixture)
        {
            var services = fixture.ServiceCollection;

            var provider = services.BuildServiceProvider();
            _mediatr = provider.GetRequiredService<IMediator>();
            _context = provider.GetRequiredService<KolmeoContext>();
            _faker = new Faker();
        }

        [Fact]
        public async Task DeleteProductCommandSuccess()
        {
            var adddCommand = new AddProductCommand()
            {
                Name = _faker.Commerce.ProductName(),
                Description = _faker.Commerce.ProductDescription(),
                Price = _faker.Random.Decimal(1.00m, 100.00m),
            };

            var addResult = await _mediatr.Send(adddCommand);

            var deleteCommand = new DeleteProductCommand()
            {
                Id = addResult.Id,
            };

            await _mediatr.Send(deleteCommand);

            var product = _context.Products.FirstOrDefault(p => p.Id == addResult.Id);

            product.Should().BeNull();
        }
    }
}