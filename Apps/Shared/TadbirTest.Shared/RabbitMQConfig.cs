namespace TadbirTest.Shared
{
    public class BaseConfig
    {
        public RabbitMQConfig RabbitMQConfig { get; set; }
        public RedisConfig RedisConfig { get; set; }
    }

    public class RabbitMQConfig
    {
        public string RabbitMqRootUri { get; set; }
        public string RabbitMqUri { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string QueueName { get; set; }
    }

    public class RedisConfig
    {
        public string Host { get; set; }
    }
}
