using Abp.MqMessages;
using Castle.Core.Logging;
using Rebus.Handlers;
using System.Threading.Tasks;
using WorkFlowTaskSystem.MqMessage;

namespace WorkFlowTaskSystem.RabbitMqConsumer.Host.Handlers
{
    public class TestHandler : IHandleMessages<TestMessage>
    {
        public ILogger Logger { get; set; }
        public IMqMessagePublisher Publisher { get; set; }
        public TestHandler()
        {
            Publisher = NullMqMessagePublisher.Instance;
            Logger = NullLogger.Instance;
        }

        public async Task Handle(TestMessage message)
        {
            var msg = $"{Logger.GetType()}:{message.Value},{message.Time}";
            Logger.Debug(msg);
            //await Publisher.PublishAsync(msg);//send it again!
            await Task.Run(()=> { });
        }
    }
}
