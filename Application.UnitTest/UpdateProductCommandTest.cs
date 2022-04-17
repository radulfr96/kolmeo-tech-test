using Application.Commands.AddProductCommand;
using Application.Commands.UpdateProductCommand;
using Application.UnitTests;
using DataLayer;
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
    public class UpdateProductCommandTest
    {
        private readonly UpdateProductCommandValidator _validator;
        private readonly TestFixture _fixture;

        public UpdateProductCommandTest(TestFixture fixture)
        {
            _validator = new UpdateProductCommandValidator();
            _fixture = fixture;
        }

        [Fact]
        public void IdInvalidValue()
        {
            var command = new UpdateProductCommand()
            {
            };

            var result = _validator.TestValidate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Select(e => e.ErrorCode).Where(e => e == ErrorCodeEnum.ProductIdInvalidValue.ToString()).Any().Should().BeTrue();
        }

        [Fact]
        public void NameNotProvided()
        {
            var command = new UpdateProductCommand()
            {
                Id = 1,
            };

            var result = _validator.TestValidate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Select(e => e.ErrorCode).Where(e => e == ErrorCodeEnum.NameNotProvided.ToString()).Any().Should().BeTrue();

            command.Name = "";

            result = _validator.TestValidate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Select(e => e.ErrorCode).Where(e => e == ErrorCodeEnum.NameNotProvided.ToString()).Any().Should().BeTrue();
        }

        [Fact]
        public void DesciptionNotProvided()
        {
            var command = new UpdateProductCommand()
            {
                Id = 1,
                Name = "Test",
            };

            var result = _validator.TestValidate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Select(e => e.ErrorCode).Where(e => e == ErrorCodeEnum.DescriptionNotProvided.ToString()).Any().Should().BeTrue();

            command.Description = "";

            result = _validator.TestValidate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Select(e => e.ErrorCode).Where(e => e == ErrorCodeEnum.DescriptionNotProvided.ToString()).Any().Should().BeTrue();
        }

        [Fact]
        public void PriceInvalidValue()
        {
            var command = new UpdateProductCommand()
            {
                Id = 1,
                Name = "Test",
                Description = "Test Description",
                Price = -1.00m,
            };

            var result = _validator.TestValidate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Select(e => e.ErrorCode).Where(e => e == ErrorCodeEnum.PriceInvalidValue.ToString()).Any().Should().BeTrue();
        }

        [Fact]
        public async Task ProductNotFound()
        {
            var command = new UpdateProductCommand()
            {
                Id= 1,
                Name = "Test",
                Description = "Test Description",
                Price = 10.00m,
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