
using Confluent.Kafka;
using webapi.Models;
using Xunit;

public class RequestPermissionServiceIntegrationTests
{
    [Fact]
    public void RequestPermission_ServiceCalled_KafkaMessageSent()
    {
        // Arrange
        var unitOfWork = new UnitOfWork(context: new DbContext());
        var kafkaProducer = new KafkaProducer(new ProducerBuilder<string, string>(new ProducerConfig { BootstrapServers = "localhost:9092" }).Build());
        var permission = new Permission { EmployeeName = "John", EmployeeLastname = "Doe" };

        var requestPermissionService = new RequestPermissionService(kafkaProducer, unitOfWork);

        // Act
        requestPermissionService.RequestPermission(permission);

        // Assert
        // Verify that the message was sent to Kafka
        // You can use a Kafka consumer to verify that the message was received
    }
}