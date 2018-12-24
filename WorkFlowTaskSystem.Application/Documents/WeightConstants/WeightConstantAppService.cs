using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Aspose.Cells;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using WorkFlowTaskSystem.Application.Documents.WeightConstants.Dto;
using WorkFlowTaskSystem.Core.Damain.Entities;
using WorkFlowTaskSystem.Core.ViewModel;
using WorkFlowTaskSystem.MongoDb;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using Abp.UI;
using Microsoft.Extensions.Configuration;

namespace WorkFlowTaskSystem.Application.Documents.WeightConstants
{
    public class WeightConstantAppService : WorkFlowTaskSystemAppServiceBase<WeightConstant, WeightConstantDto, WeightConstantDto>, IWeightConstantAppService
    {
      private IHostingEnvironment _hostingEnvironment;
        private IRepository<BridgeConstant, string> _bridgeConstantrepository;
        public WeightConstantAppService(IRepository<WeightConstant,string> repository, IHostingEnvironment hostingEnvironment, IRepository<BridgeConstant, string> bridgeConstantrepository) : base(repository)
      {
        _hostingEnvironment = hostingEnvironment;
            _bridgeConstantrepository = bridgeConstantrepository;
        }


      public override Task Delete(EntityDto<string> input)
      {
            Repository.RealDelete(input.Id);
            return Task.CompletedTask;
      }

      public void RealDeleteAll()
      {
        Repository.RealDeleteAll();
      }
        public override Task<PagedResultDto<WeightConstantDto>> GetAll(PagedAndSortedResultRequestDto input)
        {
            return base.GetAll(input);
        }
       
        public override Task<WeightConstantDto> Create(WeightConstantDto input)
        {

            var data = Repository.GetAll().Where(u => u.WeightDecimal == input.WeightDecimal).ToList();
            if (data.Count()>0) {
                throw new UserFriendlyException("添加失败", "重量限制：" + input.WeightDecimal + " 已存在");
            }
            var list = _bridgeConstantrepository.GetAll().ToList();
            foreach (var item in list.GroupBy(u => u.BridgeCode))
            {
                decimal sum = item.Sum(u => u.Weight);
                if (input.WeightDecimal == sum)
                {
                    foreach (var a in item)
                    {
                        a.WeightLimit = input.WeightLimit;
                        _bridgeConstantrepository.Update(a);
                    }
                }
            }
            return base.Create(input);
        }
       
        public override Task<WeightConstantDto> Update(WeightConstantDto input)
        {
            var data = Repository.GetAll().Where(u => u.WeightDecimal == input.WeightDecimal && u.Id!=input.Id).ToList();
            if (data.Count() > 0)
            {
                throw new UserFriendlyException("修改失败", "重量限制：" + input.WeightDecimal +  " 已存在");
            }
            var list = _bridgeConstantrepository.GetAll().ToList();
            foreach (var item in list.GroupBy(u => u.BridgeCode))
            {
                decimal sum = item.Sum(u => u.Weight);
                if (input.WeightDecimal == sum) {
                    foreach (var a in item) {
                        a.WeightLimit = input.WeightLimit;
                        _bridgeConstantrepository.Update(a);
                    }
                }
            }
            
            return base.Update(input);
        }
        
    }
}
