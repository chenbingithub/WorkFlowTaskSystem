using Abp.AspNetCore.OData.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using WorkFlowTaskSystem.Core.Damain.Entities;

namespace WorkFlowTaskSystem.Web.Host.Controllers
{
    public class ProductsController:AbpODataEntityController<WeightConstant,string>, ITransientDependency
    {
        public ProductsController(IRepository<WeightConstant, string> repository) : base(repository)
        {
        }
    }
}
