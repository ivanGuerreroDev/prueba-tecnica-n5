
using Confluent.Kafka;
using webapi.Models;
using webapi.Services;
using webapi.UnitOfWork;
using Xunit;

public class RequestPermissionServiceIntegrationTests
{
    [Fact]
    public void RequestPermission_ServiceCalled_KafkaMessageSent()
    {
        // Arrange
        var unitOfWork = new UnitOfWork(new DbContext());
        var kafkaProducer = new KafkaProducer(new Producer<string, string>(new ProducerConfig { BootstrapServers = "localhost:9092" }));

        var permission = new Permission { EmployeeName = "John", EmployeeLastname = "Doe" };

        var requestPermissionService = new RequestPermissionService(kafkaProducer, unitOfWork);

        // Act
        requestPermissionService.RequestPermission(permission);

        // Assert
        // Verify that the message was sent to Kafka
        // You can use a Kafka consumer to verify that the message was received
    }
}