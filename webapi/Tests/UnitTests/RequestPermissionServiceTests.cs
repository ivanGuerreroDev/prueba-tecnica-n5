using Moq;
using webapi.Models;
using Xunit;

public class RequestPermissionServiceTests
{
    [Fact]
    public void RequestPermission_ServiceCalled_KafkaMessageSent()
    {
        // Arrange
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var kafkaProducerMock = new Mock<IKafkaProducer>();

        var permission = new Permission { EmployeeName = "John", EmployeeLastname = "Doe" };

        var requestPermissionService = new RequestPermissionService(kafkaProducerMock.Object, unitOfWorkMock.Object);

        // Act
        requestPermissionService.RequestPermission(permission);

        // Assert
        kafkaProducerMock.Verify(k => k.SendMessage("permissions_topic", It.IsAny<object>()), Times.Once);
    }
}