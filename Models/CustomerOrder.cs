namespace KafkaOrders.Models
{
    public class CustomerOrder
    {
        public string CustomerName { get; set; } = string.Empty;
        public string Order { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
