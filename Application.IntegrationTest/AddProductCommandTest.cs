using Xunit;

namespace Application.IntegrationTest
{
    public class AddProductCommandTest
    {
        private readonly AddProductCommandValidator _validator;

        public AddProductCommandTest(TestFixture fixture) : base(fixture)
        {
            _validator = new AddBookCommandValidator();
        }


        [Fact]
        public void Test1()
        {

        }
    }
}