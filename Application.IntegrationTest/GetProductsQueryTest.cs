using Application.Commands.AddProductCommand;
using Application.Queries.GetProdutsQuery;
using Bogus;
using Domain;
using FluentAssertions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTest
{
    [Collection("IntegrationTestCollection")]
    public class GetProductsQueryTest
    {
        private readonly IMediator _mediatr;
        private readonly Faker _faker;

        public GetProductsQueryTest(TestFixture fixture)
        {
            var services = fixture.ServiceCollection;

            var provider = services.BuildServiceProvider();
            _mediatr = provider.GetRequiredService<IMediator>();
            _faker = new Faker();
        }

        [Fact]
        public async Task GetProductsQuerySuccess()
        {
            var adddCommand1 = new AddProductCommand()
            {
                Name = _faker.Commerce.ProductName(),
                Description = _faker.Commerce.ProductDescription(),
                Price = _faker.Random.Decimal(1.00m, 100.00m),
            };

            var addResult1 = await _mediatr.Send(adddCommand1);

            var adddCommand2 = new AddProductCommand()
            {
                Name = _faker.Commerce.ProductName(),
                Description = _faker.Commerce.ProductDescription(),
                Price = _faker.Random.Decimal(1.00m, 100.00m),
            };

            var addResult2 = await _mediatr.Send(adddCommand2);

            var query = new GetProductsQuery()
            {
            };

            var result = await _mediatr.Send(query);

            result.Should().NotBeNull();
            result.Products.Should().ContainEquivalentOf(new Product()
            {
                Description = adddCommand1.Description,
                Id = addResult1.Id,
                Name = adddCommand1.Name,
                Price = adddCommand1.Price,
            });
            result.Products.Should().ContainEquivalentOf(new Product()
            {
                Description = adddCommand2.Description,
                Id = addResult2.Id,
                Name = adddCommand2.Name,
                Price = adddCommand2.Price,
            });
        }
    }
}