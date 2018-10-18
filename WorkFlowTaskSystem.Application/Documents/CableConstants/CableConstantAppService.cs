using System;
using Abp.Domain.Repositories;
using Aspose.Cells;
using Microsoft.AspNetCore.Hosting;
using WorkFlowTaskSystem.Application.Documents.CableConstants.Dto;
using WorkFlowTaskSystem.Core.Damain.Entities;
using WorkFlowTaskSystem.Core.ViewModel;

namespace WorkFlowTaskSystem.Application.Documents.CableConstants
{
    public class CableConstantAppService : WorkFlowTaskSystemAppServiceBase<CableConstant, CableConstantDto, CableConstantDto>, ICableConstantAppService
    {
      private IHostingEnvironment _hostingEnvironment;
    public CableConstantAppService(IRepository<CableConstant,string> repository, IHostingEnvironment hostingEnvironment) : base(repository)
    {
      _hostingEnvironment = hostingEnvironment;
    }

      /// <summary>
      /// 导入电缆型号基本信息
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
          CableConstant entity = new CableConstant();
          entity.Version = (sheet.Cells[i, 1].Value ?? "").ToString().Trim();
          entity.Specification = (sheet.Cells[i, 2].Value ?? "").ToString().Trim();
          entity.Diameter = (sheet.Cells[i, 3].Value ?? "").ToString().Trim();
          entity.WeightLimit = (sheet.Cells[i, 4].Value ?? "").ToString().Trim();

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

  }
}
