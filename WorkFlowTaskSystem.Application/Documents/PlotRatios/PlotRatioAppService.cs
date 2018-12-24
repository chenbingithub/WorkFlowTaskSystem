using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Aspose.Cells;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using WorkFlowTaskSystem.Application.Documents.PlotRatios.Dto;
using WorkFlowTaskSystem.Core.Damain.Entities;
using WorkFlowTaskSystem.Core.ViewModel;
using WorkFlowTaskSystem.MongoDb;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using Abp.UI;
using Microsoft.Extensions.Configuration;

namespace WorkFlowTaskSystem.Application.Documents.PlotRatios
{
    public class PlotRatioAppService : WorkFlowTaskSystemAppServiceBase<PlotRatio, PlotRatioDto, PlotRatioDto>, IPlotRatioAppService
    {
      private IHostingEnvironment _hostingEnvironment;
        private IRepository<BridgeConstant, string> _bridgeConstantrepository;
        public PlotRatioAppService(IRepository<PlotRatio,string> repository, IHostingEnvironment hostingEnvironment, IRepository<BridgeConstant, string> bridgeConstantrepository) : base(repository)
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
        public override Task<PagedResultDto<PlotRatioDto>> GetAll(PagedAndSortedResultRequestDto input)
        {
            return base.GetAll(input);
        }
       
        public override Task<PlotRatioDto> Create(PlotRatioDto input)
        {
            input.PassageType = input.PassageType.ToUpper();

            var data = Repository.GetAll().Where(u => u.PassageType == input.PassageType).ToList();
            if (data.Count()>0) {
                throw new UserFriendlyException("添加失败", " 通道类型：" + input.PassageType + " 已存在");
            }
            var all = _bridgeConstantrepository.GetAll().ToList().Where(u => u.PassageType.ToUpper() == input.PassageType).ToList();
            foreach (var item in all)
            {
                item.PlotRatioLimit = input.PlotRatioLimit;
                _bridgeConstantrepository.Update(item);
            }
            return base.Create(input);
        }
       
        public override Task<PlotRatioDto> Update(PlotRatioDto input)
        {
            input.PassageType = input.PassageType.ToUpper();
            var data = Repository.GetAll().Where(u => u.PassageType == input.PassageType && u.Id!=input.Id).ToList();
            if (data.Count() > 0)
            {
                throw new UserFriendlyException("修改失败", "通道类型：" + input.PassageType +  " 已存在");
            }
            var all=_bridgeConstantrepository.GetAll().ToList().Where(u=>u.PassageType.ToUpper()== input.PassageType).ToList();
            foreach (var item in all) {
                item.PlotRatioLimit = input.PlotRatioLimit;
                _bridgeConstantrepository.Update(item);
            }
            return base.Update(input);
        }
        
    }
}
