using Abp;
using DotNetCoreConsumerHost;

namespace DotNetCoreConsumerHost
{
    public class DotNetCoreConsumerHostBootstrap
    {
        private static readonly AbpBootstrapper _bs = AbpBootstrapper.Create<DotNetCoreConsumerHostModule>();

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
