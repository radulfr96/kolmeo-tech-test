using Application.Commands.AddProductCommand;
using Application.UnitTests;
using Domain.Enums;
using FluentAssertions;
using FluentValidation.TestHelper;
using System.Linq;
using Xunit;

namespace Application.UnitTest
{
    public class AddProductCommandTest
    {
        private readonly AddProductCommandValidator _validator;

        public AddProductCommandTest(TestFixture fixture)
        {
            _validator = new AddProductCommandValidator();
        }

        [Fact]
        public void NameNotProvided()
        {
            var command = new AddProductCommand()
            {
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
            var command = new AddProductCommand()
            {
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
            var command = new AddProductCommand()
            {
                Name = "Test",
                Description = "Test Description",
                Price = -1.00m,
            };

            var result = _validator.TestValidate(command);

            result.IsValid.Should().BeFalse();
            result.Errors.Select(e => e.ErrorCode).Where(e => e == ErrorCodeEnum.PriceInvalidValue.ToString()).Any().Should().BeTrue();
        }
    }
}