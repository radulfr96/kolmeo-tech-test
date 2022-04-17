using Application.Commands.AddProductCommand;
using Application.Queries.GetProductQuery;
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
    public class GetProductQueryTest
    {
        private readonly KolmeoContext _context;
        private readonly IMediator _mediatr;
        private readonly Faker _faker;

        public GetProductQueryTest(TestFixture fixture)
        {
            var services = fixture.ServiceCollection;

            var provider = services.BuildServiceProvider();
            _mediatr = provider.GetRequiredService<IMediator>();
            _context = provider.GetRequiredService<KolmeoContext>();
            _faker = new Faker();
        }

        [Fact]
        public async Task GetProductQuerySuccess()
        {
            var adddCommand = new AddProductCommand()
            {
                Name = _faker.Commerce.ProductName(),
                Description = _faker.Commerce.ProductDescription(),
                Price = _faker.Random.Decimal(1.00m, 100.00m),
            };

            var addResult = await _mediatr.Send(adddCommand);

            var getQuery = new GetProductQuery()
            {
                Id = addResult.Id,
            };

            var result = await _mediatr.Send(getQuery);

            result.Should().NotBeNull();
            result.Product.Should().BeEquivalentTo(new Product()
            {
                Description = adddCommand.Description,
                Name = adddCommand.Name,
                Price = adddCommand.Price,
                Id = addResult.Id,
            });
        }
    }
}