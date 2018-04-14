using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace WorkFlowTaskSystem.Web.Core.Configuration
{
    public static class HostingEnvironmentExtensions
    {
        public static IConfigurationRoot GetAppConfiguration(this IHostingEnvironment env)
        {

            //这里创建ConfigurationBuilder，其作用就是加载Congfig等配置文件
            var builder = new ConfigurationBuilder()

                //env.ContentRootPath：获取当前项目的跟路径
                .SetBasePath(env.ContentRootPath)
                //使用AddJsonFile方法把项目中的appsettings.json配置文件加载进来，后面的reloadOnChange顾名思义就是文件如果改动就重新加载
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                //这里关注的是$"{param}"的这种写法，有点类似于string.Format()
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            
            builder.AddEnvironmentVariables();
            //这返回一个配置文件跟节点：IConfigurationRoot
           
            return builder.Build();
        }
    }
}
