using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Aspose.Cells;
using Microsoft.AspNetCore.Hosting;
using WorkFlowTaskSystem.Application.Documents.BridgeConstants.Dto;

using WorkFlowTaskSystem.Core.Damain.Entities;
using WorkFlowTaskSystem.Core.ViewModel;
using WorkFlowTaskSystem.MongoDb;

namespace WorkFlowTaskSystem.Application.Documents.BridgeConstants
{
    public class BridgeConstantAppService : WorkFlowTaskSystemAppServiceBase<BridgeConstant, BridgeConstantDto, BridgeConstantDto>, IBridgeConstantAppService
    {
      private IHostingEnvironment _hostingEnvironment;
      public BridgeConstantAppService(IRepository<BridgeConstant,string> repository, IHostingEnvironment hostingEnvironment) : base(repository)
      {
        _hostingEnvironment = hostingEnvironment;
      }

    /// <summary>
    /// 导入桥架基本信息
    /// </summary>

    /// <param name="enView"></param>
    public void Insert(ConstantView enView)
    {
      try
      {
        var addrUrl = _hostingEnvironment.WebRootPath + "/upload/" + enView.Path;
        Workbook wb = new Workbook(addrUrl);
        var sheet = wb.Worksheets[0];
        for (int i = 1; i < sheet.Cells.MaxRow + 1; i++)
        {
          BridgeConstant entity = new BridgeConstant();
          entity.BridgeCode = (sheet.Cells[i, 1].Value ?? "").ToString().Trim();
          entity.PassageType = (sheet.Cells[i, 2].Value ?? "").ToString().Trim();
          entity.SectionalArea = (sheet.Cells[i, 3].Value ?? "").ToString().Trim();
          entity.PlotRatioLimit = (sheet.Cells[i, 4].Value ?? "").ToString().Trim();
          entity.WeightLimit = (sheet.Cells[i, 5].Value ?? "").ToString().Trim();

          entity.Description = enView.NumberNo;
          entity.Id = Guid.NewGuid().ToString("N");
          Repository.Insert(entity);
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }

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
    }
}
