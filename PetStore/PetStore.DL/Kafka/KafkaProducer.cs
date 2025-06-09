using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PetStore.Models.Configurations.CachePopulator;
using PetStore.Models.DTO;
using PetStore.Models.Serialization;
using Serilog.Core;

namespace PetStore.DL.Kafka
{

    internal class KafkaProducer<TKey, TData, TConfiguration> : IKafkaProducer<TData> where TData : ICacheItem<TKey> where TKey : notnull
          where TConfiguration : CacheConfiguration
    {
        private readonly ProducerConfig _config;
        private readonly IProducer<TKey, TData> _producer;
        private readonly IOptionsMonitor<TConfiguration> _kafkaConfig;
        private readonly ILogger<KafkaProducer<TKey, TData, TConfiguration>> _logger;
        public KafkaProducer(
            IOptionsMonitor<TConfiguration> kafkaConfig,
             ILogger<KafkaProducer<TKey, TData, TConfiguration>> logger)
        {

            _config = new ProducerConfig()
            {
                BootstrapServers = "d10730b9s3jfi4njq8c0.any.eu-central-1.mpx.prd.cloud.redpanda.com:9092",
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.ScramSha256,
                SaslUsername = "user",
                SaslPassword = "B7GS01Gel7qnatOcGiuGRAYG7eEkCA",
                EnableSslCertificateVerification = false
            };

            _producer = new ProducerBuilder<TKey, TData>(_config)
                .SetValueSerializer(new MsgPackSerializer<TData>())
                .Build();
            _kafkaConfig = kafkaConfig;
            _logger = logger;
        }

        public async Task Produce(TData message)
        {
            try
            {
                var topic = _kafkaConfig.CurrentValue.Topic;
                _logger.LogInformation("Producing message to topic {Topic} with key {Key}", topic, message.GetKey());

                await _producer.ProduceAsync(topic, new Message<TKey, TData>
                {
                    Key = message.GetKey(),
                    Value = message
                });

                _logger.LogInformation("Successfully produced message with key {Key} to topic {Topic}", message.GetKey(), topic);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error producing message with key {Key}", message.GetKey());
                throw;
            }
        }


        public async Task ProduceAll(IEnumerable<TData> messages)
        {

            await ProduceBatches(messages);
        }

        public async Task ProduceBatches(IEnumerable<TData> messages)
        {
            const int batchSize = 50;
            var batch = new List<Task>();

            foreach (var message in messages)
            {
                batch.Add(Produce(message));

                if (batch.Count == batchSize)
                {
                    await Task.WhenAll(batch);
                    batch.Clear();
                }
            }

            if (batch.Count > 0)
            {
                await Task.WhenAll(batch);
            }
        }

    }
}
