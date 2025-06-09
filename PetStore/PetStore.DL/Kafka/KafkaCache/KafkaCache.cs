using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using PetStore.Models.Serialization;

namespace PetStore.DL.Kafka.KafkaCache
{
    public class KafkaCache<TKey, TValue> : BackgroundService
    {
        private readonly ConsumerConfig _config;

        public KafkaCache()
        {
            _config = new ConsumerConfig
            {
                BootstrapServers = "d10730b9s3jfi4njq8c0.any.eu-central-1.mpx.prd.cloud.redpanda.com:9092",
                GroupId = $"KafkaChat-{Guid.NewGuid}",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.ScramSha256,
                SaslUsername = "user",
                SaslPassword = "B7GS01Gel7qnatOcGiuGRAYG7eEkCA"
            };
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Run(() => ConsumeMessages(stoppingToken), stoppingToken);

            return Task.CompletedTask;
        }

        private void ConsumeMessages(CancellationToken stoppingToken)
        {
            using (var consumer = new ConsumerBuilder<TKey, TValue>(_config)
              .SetValueDeserializer(new MessagePackDeserializer<TValue>())
              .Build())
            {
                consumer.Subscribe("pet");

                while (!stoppingToken.IsCancellationRequested)
                {
                    var consumeResult = consumer.Consume();

                    if (consumeResult.IsPartitionEOF)
                    {
                        continue;
                    }

                    if (consumeResult != null)
                    {
                        Console.WriteLine($"Error: {consumeResult.Message.Key}");
                    }
                }
            }
        }
    }
}
