using Application.Commands.AddProductCommand;
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
    public class AddProductCommandTest
    {
        private readonly KolmeoContext _context;
        private readonly IMediator _mediatr;
        private readonly Faker _faker;

        public AddProductCommandTest(TestFixture fixture)
        {
            var services = fixture.ServiceCollection;

            var provider = services.BuildServiceProvider();
            _mediatr = provider.GetRequiredService<IMediator>();
            _context = provider.GetRequiredService<KolmeoContext>();
            _faker = new Faker();
        }

        [Fact]
        public async Task AddProductCommandSuccess()
        {

            var command = new AddProductCommand()
            {
                Name = _faker.Commerce.ProductName(),
                Description = _faker.Commerce.ProductDescription(),
                Price = _faker.Random.Decimal(1.00m, 100.00m),
            };

            var result = await _mediatr.Send(command);

            var product = _context.Products.FirstOrDefault(a => a.Id == result.Id);

            product.Should().BeEquivalentTo(new Product()
            {
                Name = command.Name,
                Description = command.Description,
                Price = command.Price,
                Id = result.Id,
            });
        }
    }
}