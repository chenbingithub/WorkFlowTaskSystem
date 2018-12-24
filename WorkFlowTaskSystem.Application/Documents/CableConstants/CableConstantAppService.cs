using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using Aspose.Cells;
using Microsoft.AspNetCore.Hosting;
using WorkFlowTaskSystem.Application.Documents.CableConstants.Dto;
using WorkFlowTaskSystem.Core.Damain.Entities;
using WorkFlowTaskSystem.Core.ViewModel;
using WorkFlowTaskSystem.MongoDb;

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
            var addrUrl = _hostingEnvironment.WebRootPath + "/upload/" + enView.Path;
            Workbook wb = new Workbook(addrUrl);
            var sheet = wb.Worksheets[0];
            List<CableConstant> list = new List<CableConstant>();
            for (int i = 1; i < sheet.Cells.MaxRow + 1; i++)
            {
                CableConstant entity = new CableConstant();
                entity.Number = (sheet.Cells[i, 0].Value ?? "").ToString().Trim();
                entity.Version = (sheet.Cells[i, 1].Value ?? "").ToString().Trim();
                entity.Specification = (sheet.Cells[i, 2].Value ?? "").ToString().Trim();
                entity.Diameter = (sheet.Cells[i, 3].Value ?? "").ToString().Trim();
                entity.WeightLimit = (sheet.Cells[i, 4].Value ?? "").ToString().Trim();

                entity.Description = enView.NumberNo;
                entity.Id = Guid.NewGuid().ToString("N");
                list.Add(entity);
                //Repository.Insert(entity);          
            }
            List<string> meassages = new List<string>();
            foreach (var item in list.GroupBy(u => new { u.Version, u.Specification }))
            {
                if (item.Count() > 1)
                {
                    string message = "序号：";
                    foreach (var t in item)
                    {
                        message += t.Number + ",";
                    }
                    meassages.Add(message + " 电缆型号和类型相同");
                }
            }
            if (meassages.Count() > 0)
            {
                throw new UserFriendlyException("导入失败", string.Join(";<br/>", meassages));
            }
            var query = MongoDB.Driver.Builders.Query<CableConstant>.In(u => u.Version, list.Select(u => u.Version));
            var query1 = MongoDB.Driver.Builders.Query<CableConstant>.In(u => u.Specification, list.Select(u => u.Specification));
            var data = Repository.GetList(MongoDB.Driver.Builders.Query.And(query, query1));
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    var e = list.FirstOrDefault(u => u.Version == item.Version && u.Specification == item.Specification);
                    meassages.Add(string.Format("序号：{0} 电缆型号和类型已存在", e.Number));
                }
            }
            if (meassages.Count() > 0)
            {
                throw new UserFriendlyException("导入失败", string.Join(";<br/>", meassages));
            }
            foreach (var item in list) {
                if(string.IsNullOrEmpty(item.Diameter)) meassages.Add(string.Format("序号：{0} 电缆外径为空", item.Number));
                if(string.IsNullOrEmpty(item.Version)) meassages.Add(string.Format("序号：{0} 电缆型号为空", item.Number));
                if(string.IsNullOrEmpty(item.WeightLimit)) meassages.Add(string.Format("序号：{0} 电缆重量为空", item.Number));
                if(string.IsNullOrEmpty(item.Specification)) meassages.Add(string.Format("序号：{0} 电缆规格为空", item.Number));
            }
            if (meassages.Count() > 0)
            {
                throw new UserFriendlyException("导入失败", string.Join(";<br/>", meassages));
            }
            Repository.InsertBatch(list);

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
        public Task<PagedResultDto<CableConstantDto>> GetAllOrSeach(string seachKey, PagedAndSortedResultRequestDto input)
        {
            if (!string.IsNullOrEmpty(seachKey))
            {
                var result = Repository.GetAll().Where(u => u.Version.Contains(seachKey));
                var totalCount = result.Count();
                var query = result.PageBy(input);
                var data = new PagedResultDto<CableConstantDto>(
                    totalCount,
                    query.AsEnumerable().Select(MapToEntityDto).ToList()
                );
                return Task.FromResult(data);
            }
            return base.GetAll(input);
        }
        public override Task<PagedResultDto<CableConstantDto>> GetAll(PagedAndSortedResultRequestDto input)
        {
            return base.GetAll(input);
        }

        public override Task<CableConstantDto> Create(CableConstantDto input)
        {
            var data=Repository.GetAll().Where(u => u.Version == input.Version&&u.Specification==input.Specification).ToList();
            if (data.Count > 0) {
                throw new UserFriendlyException("添加失败","型号："+input.Version+" 规格："+ input.Specification+" 已存在");
            }
            return base.Create(input);
        }
        public override Task<CableConstantDto> Update(CableConstantDto input)
        {
            var data = Repository.GetAll().Where(u => u.Version == input.Version && u.Specification == input.Specification&&u.Id!=input.Id).ToList();
            if (data.Count > 0)
            {
                throw new UserFriendlyException("修改失败", "型号：" + input.Version + " 规格：" + input.Specification + " 已存在");
            }
            return base.Update(input);

        }

    }
}
