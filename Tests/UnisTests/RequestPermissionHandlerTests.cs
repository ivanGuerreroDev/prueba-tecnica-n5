using WebApi.Models;
using WebApi.Repositories;
using WebApi.Services;
using Moq;
using Xunit;
using WebApi.ElasticSearch;
using WebApi.Kafka;

namespace WebApi.Tests.UnitTests
{
    public class RequestPermissionHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IElasticSearchService> _elasticSearchMock;
        private readonly Mock<IKafkaProducer> _kafkaProducerMock;
        private readonly RequestPermissionHandler _handler;

        public RequestPermissionHandlerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _elasticSearchMock = new Mock<IElasticSearchService>();
            _kafkaProducerMock = new Mock<IKafkaProducer>();

            _handler = new RequestPermissionHandler(_unitOfWorkMock.Object, _elasticSearchMock.Object, _kafkaProducerMock.Object);
        }

        [Fact]
        public async Task ShouldAddPermissionAndCommit()
        {
            var command = new RequestPermissionCommand { UserId = 1, PermissionTypeId = 2 };

            await _handler.Handle(command);

            _unitOfWorkMock.Verify(uow => uow.Permissions.AddPermissionAsync(It.IsAny<Permission>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitAsync(), Times.Once);
            _elasticSearchMock.Verify(es => es.IndexPermissionAsync(It.IsAny<Permission>()), Times.Once);
            _kafkaProducerMock.Verify(kp => kp.SendMessageAsync("request", It.IsAny<string>()), Times.Once);
        }
    }
}
