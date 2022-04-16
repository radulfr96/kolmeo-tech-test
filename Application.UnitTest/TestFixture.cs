using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Domain;
using Application.Commands.AddProductCommand;

namespace Application.UnitTests
{
    public class TestFixture
    {
        public IServiceCollection ServiceCollection { get; private set; }

        public TestFixture()
        {
            ServiceCollection = new ServiceCollection();

            ServiceCollection.AddDbContext<KolmeoContext>(opt =>
            {
                opt.UseInMemoryDatabase("ApollosLibrayUnitTestDB");
            });

            ServiceCollection.AddDbContext<KolmeoContext>();

            ServiceCollection.AddMediatR(typeof(AddProductCommand).GetTypeInfo().Assembly);
        }
    }
}
