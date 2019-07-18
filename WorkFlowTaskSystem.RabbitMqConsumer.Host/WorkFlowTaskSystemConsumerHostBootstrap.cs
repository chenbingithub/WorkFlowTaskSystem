using Abp;

namespace WorkFlowTaskSystem.RabbitMqConsumer.Host
{
    public class WorkFlowTaskSystemConsumerHostBootstrap
    {
        private static readonly AbpBootstrapper _bs = AbpBootstrapper.Create<WorkFlowTaskSystemConsumerHostModule>();

        public void Start()
        {
            //LogManager.Configuration = new XmlLoggingConfiguration("nlog.config");
            _bs.Initialize();
        }

        public void Stop()
        {
            _bs.Dispose();
        }
    }
}
