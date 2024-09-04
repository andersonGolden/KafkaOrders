using Confluent.Kafka;
using KafkaOrders.Models;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace KafkaOrders.Services
{
    public class CustomerOrdersService: ICustomerOrdersService
    {
        private readonly string _bootstrapServers;
        private readonly string _topicName;
        public CustomerOrdersService()
        {
            _bootstrapServers = "localhost:9092";
            _topicName = "customer-orders";
        }

        public async Task CreateOrderAsync(CustomerOrder order)
        {
            var config = new ProducerConfig { BootstrapServers = _bootstrapServers };

            // Serialize the Order object to JSON format 
            var orderJson = JsonSerializer.Serialize(order);
            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var result = await producer.ProduceAsync(_topicName, 
                        new Message<Null, string> { Value = orderJson }); 
                    
                    Console.WriteLine($"Order produced to topic {result.TopicPartitionOffset}");
                }
                catch (ProduceException<Null, string> e) 
                { 
                    Console.WriteLine($"Failed to deliver message: {e.Error.Reason}"); 
                }
            }
        }
    }

    public interface ICustomerOrdersService
    {
        Task CreateOrderAsync(CustomerOrder order);
    }
}
