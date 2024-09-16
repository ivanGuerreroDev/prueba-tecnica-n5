using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
public class KafkaConsumerService : BackgroundService
{
    private readonly ILogger<KafkaConsumerService> _logger;
    private readonly ConsumerConfig _config;

    public KafkaConsumerService(ILogger<KafkaConsumerService> logger)
    {
        _logger = logger;
        _config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "permissions-group",
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using (var consumer = new ConsumerBuilder<Ignore, string>(_config).Build())
        {
            consumer.Subscribe("permissions");

            _logger.LogInformation("Kafka Consumer started.");

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var cr = consumer.Consume(stoppingToken);
                    _logger.LogInformation($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");
                    // recieve dto from kafka Id, NameOperation: modify, request or get.

                    
                }
            }
            catch (ConsumeException e)
            {
                _logger.LogError($"Error occurred: {e.Error.Reason}");
            }
            finally
            {
                consumer.Close();
            }
        }
    }
}
