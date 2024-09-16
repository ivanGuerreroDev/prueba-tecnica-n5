using Confluent.Kafka;
using Newtonsoft.Json;

public interface IKafkaProducer
{
    void SendMessage(string topic, object message);
}

public class KafkaProducer : IKafkaProducer
{
    private readonly IProducer<string, string> _producer;

    public KafkaProducer(IProducer<string, string> producer)
    {
        _producer = producer;
    }

    public void SendMessage(string topic, object message)
    {
        var jsonMessage = JsonConvert.SerializeObject(message);
        _producer.Produce(topic, new Message<string, string> { Value = jsonMessage });
    }
}