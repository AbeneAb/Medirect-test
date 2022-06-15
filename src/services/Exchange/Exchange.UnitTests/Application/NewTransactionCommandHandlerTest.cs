using Moq;
using Xunit;

namespace Exchange.UnitTests.Application
{
    public class NewTransactionCommandHandlerTest
    {
        private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
        public NewTransactionCommandHandlerTest()
        {
            _transactionRepositoryMock = new Mock<ITransactionRepository>();
        }
        [Fact]
        public void Handle_throws_exception_when_no_Buyer() 
        {

        }
    }
}
