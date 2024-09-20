namespace WebApi.Kafka
{
    public interface IKafkaProducer
    {
        Task SendMessageAsync(string operation, string message);
    }
}