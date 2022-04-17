using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.IO;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Domain;
using UnitOfWork.Contracts;
using UnitOfWork;
using Application.Commands.AddProductCommand;

namespace Application.IntegrationTest
{
    public class TestFixture
    {
        public IServiceCollection ServiceCollection { get; private set; }

        public TestFixture()
        {
            var services = new ServiceCollection();

            services.AddDbContext<KolmeoContext>(opt =>
            {
                opt.UseInMemoryDatabase("KolmeoIntegrationTestCache");
            });

            services.AddTransient<IProductUnitofWork>(p =>
            {
                return new ProductUnitOfWork(p.GetRequiredService<KolmeoContext>());
            });

            services.AddMediatR(typeof(AddProductCommand).GetTypeInfo().Assembly);

            var provider = services.BuildServiceProvider();

            ServiceCollection = services;
        }
    }
}
