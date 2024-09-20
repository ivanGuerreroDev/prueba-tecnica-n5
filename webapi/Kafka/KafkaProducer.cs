using Confluent.Kafka;
namespace WebApi.Kafka
{
    public class KafkaProducer : IKafkaProducer
    {
        private readonly IProducer<Null, string> _producer;

        public KafkaProducer()
        {
            var config = new ProducerConfig { BootstrapServers = "localhost:9092" };
            _producer = new ProducerBuilder<Null, string>(config).Build();
        }

        public async Task SendMessageAsync(string operation, string message)
        {
            await _producer.ProduceAsync("permissions_topic", new Message<Null, string> { Value = $"{operation}: {message}" });
        }
    }
}
