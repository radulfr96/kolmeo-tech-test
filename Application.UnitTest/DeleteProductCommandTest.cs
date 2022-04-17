using Application.Commands.DeleteProductCommand;
using Application.UnitTests;
using DataLayer.Contracts;
using Domain.Enums;
using Domain.Exceptions;
using FluentAssertions;
using FluentValidation.TestHelper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using UnitOfWork.Contracts;
using Xunit;

namespace Application.UnitTest
{
    [Collection("UnitTestCollection")]
    public class DeleteProductCommandTest
    {
        private readonly DeleteProductCommandValidator _validator;
        private readonly TestFixture _fixture;

        public DeleteProductCommandTest(TestFixture fixture)
        {
            _validator = new DeleteProductCommandValidator();
            _fixture = fixture;
        }

        [Fact]
        public void IdInvalidValue()
        {
            var command = new DeleteProductCommand()
            {
            };

            var result = _validator.TestValidate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Select(e => e.ErrorCode).Where(e => e == ErrorCodeEnum.ProductIdInvalidValue.ToString()).Any().Should().BeTrue();
        }

        [Fact]
        public async Task ProductNotFound()
        {
            var command = new DeleteProductCommand()
            {
                Id = 1,
            };

            var productDataLayer = new Mock<IProductDataLayer>();

            var mockProductUOW = new Mock<IProductUnitofWork>();
            mockProductUOW.Setup(p => p.ProductDataLayer).Returns(productDataLayer.Object);
            _fixture.ServiceCollection.AddTransient(services =>
            {
                return mockProductUOW.Object;
            });

            var provider = _fixture.ServiceCollection.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();

            Func<Task> act = async () => await mediator.Send(command);
            await act.Should().ThrowAsync<ProductNotFoundException>();
        }
    }
}