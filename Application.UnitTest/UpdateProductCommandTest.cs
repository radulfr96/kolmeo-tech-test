using Application.Commands.AddProductCommand;
using Application.Commands.UpdateProductCommand;
using Application.UnitTests;
using Domain.Enums;
using FluentAssertions;
using FluentValidation.TestHelper;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using UnitOfWork.Contracts;
using Xunit;

namespace Application.UnitTest
{
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
            var command = new AddProductCommand()
            {
                Name = "Test",
                Description = "Test Description",
                Price = 10.00m,
            };

            var mockProductUOW = new Mock<IProductUnitofWork>();
            _fixture.ServiceCollection.AddTransient(services =>
            {
                return mockProductUOW.Object;
            });

            var referenceDataLayer = new Mock<IReferenceDataLayer>();
            referenceDataLayer.Setup(d => d.GetPublicationFormat(It.IsAny<int>())).Returns(Task.FromResult(new PublicationFormat()));

            var referenceUnitOfWork = new Mock<IReferenceUnitOfWork>();
            referenceUnitOfWork.Setup(r => r.ReferenceDataLayer).Returns(referenceDataLayer.Object);

            _fixture.ServiceCollection.AddTransient(services =>
            {
                return referenceUnitOfWork.Object;
            });

            var publisherDataLayer = new Mock<IPublisherDataLayer>();

            var publisherUnitOfWork = new Mock<IPublisherUnitOfWork>();
            publisherUnitOfWork.Setup(r => r.PublisherDataLayer).Returns(publisherDataLayer.Object);

            _fixture.ServiceCollection.AddTransient(services =>
            {
                return publisherUnitOfWork.Object;
            });

            var bookDataLayer = new Mock<IBookDataLayer>();
            bookDataLayer.Setup(d => d.GetSeries(It.IsAny<int>())).Returns(Task.FromResult((Domain.Series)null));

            var bookUnitOfWork = new Mock<IBookUnitOfWork>();
            bookUnitOfWork.Setup(b => b.BookDataLayer).Returns(bookDataLayer.Object);

            _fixture.ServiceCollection.AddTransient(services =>
            {
                return bookUnitOfWork.Object;
            });

            var authorUnitOfWork = new Mock<IAuthorUnitOfWork>();

            var authorDataLayer = new Mock<IAuthorDataLayer>();
            authorUnitOfWork.Setup(r => r.AuthorDataLayer).Returns(authorDataLayer.Object);

            _fixture.ServiceCollection.AddTransient(services =>
            {
                return authorUnitOfWork.Object;
            });

            var genreUnitOfWork = new Mock<IGenreUnitOfWork>();

            var genreDataLayer = new Mock<IGenreDataLayer>();

            _fixture.ServiceCollection.AddTransient(services =>
            {
                return genreUnitOfWork.Object;
            });

            var seriesUnitOfWork = new Mock<ISeriesUnitOfWork>();

            var seriesDataLayer = new Mock<ISeriesDataLayer>();
            seriesUnitOfWork.Setup(r => r.SeriesDataLayer).Returns(seriesDataLayer.Object);

            _fixture.ServiceCollection.AddTransient(services =>
            {
                return seriesUnitOfWork.Object;
            });

            var provider = _fixture.ServiceCollection.BuildServiceProvider();
            var mediator = provider.GetRequiredService<IMediator>();

            Func<Task> act = async () => await mediator.Send(command);
            await act.Should().ThrowAsync<FictionTypeNotFoundException>();
        }
    }
}