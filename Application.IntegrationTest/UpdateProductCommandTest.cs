using Application.Commands.AddProductCommand;
using Application.Commands.UpdateProductCommand;
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
    public class UpdateProductCommandTest
    {
        private readonly KolmeoContext _context;
        private readonly IMediator _mediatr;
        private readonly Faker _faker;

        public UpdateProductCommandTest(TestFixture fixture)
        {
            var services = fixture.ServiceCollection;

            var provider = services.BuildServiceProvider();
            _mediatr = provider.GetRequiredService<IMediator>();
            _context = provider.GetRequiredService<KolmeoContext>();
            _faker = new Faker();
        }

        [Fact]
        public async Task UpdateProductCommandSuccess()
        {

            var adddCommand = new AddProductCommand()
            {
                Name = _faker.Commerce.ProductName(),
                Description = _faker.Commerce.ProductDescription(),
                Price = _faker.Random.Decimal(1.00m, 100.00m),
            };

            var addResult = await _mediatr.Send(adddCommand);

            var editCommand = new UpdateProductCommand()
            {
                Id = addResult.Id,
                Description = _faker.Commerce.ProductDescription(),
                Name = _faker.Commerce.ProductName(),
                Price = _faker.Random.Decimal(1.00m, 100.00m),
            };

            var result = await _mediatr.Send(editCommand);

            var product = _context.Products.FirstOrDefault(p => p.Id == addResult.Id);

            product.Should().NotBeNull();
            product.Should().BeEquivalentTo(new Product()
            {
                Description = editCommand.Description,
                Id = addResult.Id,
                Name = editCommand.Name,
                Price = editCommand.Price,
            });
        }
    }
}