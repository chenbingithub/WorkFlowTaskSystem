using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApiClient.Attributes;
using WebApiClient.Contexts;

namespace WebApiClient.Core.Filters
{
   public class ApiEmailApiActionFilterAttribute: ApiActionFilterAttribute
    {
        public override Task OnBeginRequestAsync(ApiActionContext context)
        {
            return base.OnBeginRequestAsync(context);
        }
    }
}
