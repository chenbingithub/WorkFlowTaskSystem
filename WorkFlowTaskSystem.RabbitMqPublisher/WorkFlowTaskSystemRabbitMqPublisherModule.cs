using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Modules;
using Abp.MqMessages.Publishers;
using Abp.Net.Mail.Smtp;
using System;
using System.Configuration;
using System.Reflection;

namespace WorkFlowTaskSystem.RabbitMqPublisher
{
    [DependsOn(typeof(RebusRabbitMqPublisherModule))]
    public class WorkFlowTaskSystemRabbitMqPublisherModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.UseAbplusRebusRabbitMqPublisher()
                .UseLogging(c => c.ColoredConsole())
                .ConnectTo("amqp://guest:guest@127.0.0.1/");//set your own connection string of rabbitmq
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }

        public override void PostInitialize()
        {
            //Abp.Dependency.IocManager.Instance.IocContainer.AddFacility<LoggingFacility>(f => f.LogUsing<NLogFactory>().WithConfig("nlog.config"));

            //var workManager = IocManager.Resolve<IBackgroundWorkerManager>();
            //workManager.Add(IocManager.Resolve<TestWorker>());
        }
    }
}
