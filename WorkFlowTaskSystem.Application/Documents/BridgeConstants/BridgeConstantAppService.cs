using System;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Aspose.Cells;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using WorkFlowTaskSystem.Application.Documents.BridgeConstants.Dto;
using WorkFlowTaskSystem.Core.Damain.Entities;
using WorkFlowTaskSystem.Core.ViewModel;
using WorkFlowTaskSystem.MongoDb;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using Abp.UI;
using Microsoft.Extensions.Configuration;

namespace WorkFlowTaskSystem.Application.Documents.BridgeConstants
{
    public class BridgeConstantAppService : WorkFlowTaskSystemAppServiceBase<BridgeConstant, BridgeConstantDto, BridgeConstantDto>, IBridgeConstantAppService
    {
      private IHostingEnvironment _hostingEnvironment;
        private IRepository<WeightConstant, string> _weightConstantRepository;
        private IRepository<PlotRatio, string> _plotRatioRepository;

        public BridgeConstantAppService(IRepository<BridgeConstant,string> repository, IHostingEnvironment hostingEnvironment, IRepository<WeightConstant, string> weightConstantRepository, IRepository<PlotRatio, string> plotRatioRepository) : base(repository)
      {
        _hostingEnvironment = hostingEnvironment;
            _weightConstantRepository = weightConstantRepository;
            _plotRatioRepository = plotRatioRepository;
        }

    /// <summary>
    /// 导入桥架基本信息
    /// </summary>

    /// <param name="enView"></param>
    public void Insert(ConstantView enView)
    {
            var addrUrl = _hostingEnvironment.WebRootPath + "/upload/" + enView.Path;
            Workbook wb = new Workbook(addrUrl);
            var sheet = wb.Worksheets[0];
            List<BridgeConstant> list = new List<BridgeConstant>();
            var plotRatioall = _plotRatioRepository.GetAll().ToList();
            List<string> meassages = new List<string>();
            for (int i = 2; i < sheet.Cells.MaxRow + 1; i++)
            {
                var cells = sheet.Cells;
                BridgeConstant entity = new BridgeConstant();
                entity.Number = (cells[i, 0].Value ?? "").ToString().Trim();
                entity.BridgeCode = (cells[i, 1].Value ?? "").ToString().Trim();
                entity.Types = (cells[i, 2].Value ?? "").ToString().Trim();
                entity.Series = (cells[i, 3].Value ?? "").ToString().Trim();
                entity.Version = (cells[i,4].Value ?? "").ToString().Trim();
                entity.Lenght = decimal.Parse(cells[i, 5].StringValue);
                entity.Weight = decimal.Parse(cells[i, 6].StringValue);
                entity.Height = decimal.Parse(cells[i,7].StringValue);
                entity.PassageType = (cells[i, 8].Value ?? "").ToString().Trim();
                entity.SectionalArea = (entity.Weight * entity.Height).ToString();
                var plot = plotRatioall.FirstOrDefault(u => u.PassageType == entity.PassageType.ToUpper());
                if (plot == null)
                {
                    meassages.Add(string.Format("容积率常量库 通道类型：{0} 不存在", entity.PassageType));
                }
                else {
                    entity.PlotRatioLimit = plot.PlotRatioLimit;
                }
                
                entity.WeightLimit = "";

                entity.Description = enView.NumberNo;
                entity.Id = Guid.NewGuid().ToString("N");
                list.Add(entity);
            }
           
            foreach (var item in list.GroupBy(u => new { u.BridgeCode, u.PassageType }))
            {
                if (item.Count() > 1)
                {
                    string message = "序号：";
                    foreach (var t in item)
                    {
                        message += t.Number + ",";
                    }
                    meassages.Add(message + " 桥架编号和线缆类型相同");
                }
            }
            if (meassages.Count() > 0)
            {
                throw new UserFriendlyException("导入失败", string.Join(";<br/>", meassages));
            }
            var query = MongoDB.Driver.Builders.Query<BridgeConstant>.In(u => u.BridgeCode, list.Select(u => u.BridgeCode));
            var query1 = MongoDB.Driver.Builders.Query<BridgeConstant>.In(u => u.PassageType, list.Select(u => u.PassageType));
            var data = Repository.GetList(MongoDB.Driver.Builders.Query.And(query, query1));
            if (data.Count > 0)
            {
                foreach (var item in data)
                {
                    var e = list.FirstOrDefault(u => u.BridgeCode == item.BridgeCode && u.PassageType == item.PassageType);
                    meassages.Add(string.Format("序号：{0} 桥架编号和线缆类型已存在", e.Number));
                }
            }
            if (meassages.Count() > 0)
            {
                throw new UserFriendlyException("导入失败", string.Join(";<br/>", meassages));
            }
            var all=Repository.GetAll().ToList();
            var constlist = _weightConstantRepository.GetAll().ToList();
            List<BridgeConstant> uplist = new List<BridgeConstant>();
            foreach (var item in list.GroupBy(u => u.BridgeCode))
            {
                string weightLimit = "0";
                decimal sum = item.Sum(u => u.Weight);
                var eq=all.Where(u => u.BridgeCode == item.Key).ToList();
                if (eq.Count() >0) {
                    sum += eq.Sum(u => u.Weight);
                }
                var c= constlist.FirstOrDefault(u => u.WeightDecimal == sum);
                if (c == null)
                {
                    meassages.Add(string.Format("桥架编号：{0} 重量限值：{1}  不在规定范围", item.Key, sum));
                }
                else {
                    weightLimit = c.WeightLimit;
                }
                
                foreach (var t in item)
                {
                    t.WeightLimit = weightLimit;
                }
                if (eq.Count() > 0 && weightLimit != "0") {
                    foreach (var m in eq) {
                        m.WeightLimit = weightLimit;
                        uplist.Add(m);
                    }
                    
                }
            }
            if (meassages.Count() > 0)
            {
                throw new UserFriendlyException("导入失败", string.Join(";<br/>", meassages));
            }
            foreach (var item in list)
            {
                if (string.IsNullOrEmpty(item.BridgeCode)) meassages.Add(string.Format("序号：{0} 桥架编码为空", item.Number));
                if (string.IsNullOrEmpty(item.PassageType)) meassages.Add(string.Format("序号：{0} 通道类型为空", item.Number));
            }
            if (meassages.Count() > 0)
            {
                throw new UserFriendlyException("导入失败", string.Join(";<br/>", meassages));
            }
            foreach (var entity in uplist) {
                Repository.Update(entity);
            }
            Repository.InsertBatch(list);

        }

      public override Task Delete(EntityDto<string> input)
      {
        var entity=Repository.Get(input.Id);
        var eDto = MapToEntityDto(entity);
        eDto.Weight = 0;
        Repository.RealDelete(input.Id);
        BridgeCodeToWeightLimit(eDto);
        return Task.CompletedTask;
      }

      public void RealDeleteAll()
      {
        Repository.RealDeleteAll();
      }
        public override Task<PagedResultDto<BridgeConstantDto>> GetAll(PagedAndSortedResultRequestDto input)
        {
            return base.GetAll(input);
        }
        public Task<PagedResultDto<BridgeConstantDto>> GetAllOrSeach(string seachKey,  PagedAndSortedResultRequestDto input) {
            if (!string.IsNullOrEmpty(seachKey))
            {
                var result = Repository.GetAll()
                    .Where(u => u.BridgeCode.Contains(seachKey) || u.Version.Contains(seachKey) ||
                                u.Types.Contains(seachKey) || u.Series.Contains(seachKey));
                var totalCount = result.Count();
                var query = result.PageBy(input);
                var data = new PagedResultDto<BridgeConstantDto>(
                    totalCount,
                    query.AsEnumerable().Select(MapToEntityDto).ToList()
                );
                return Task.FromResult(data);
            }
            return base.GetAll(input);
        }
        public override Task<BridgeConstantDto> Create(BridgeConstantDto input)
        {

            var data = Repository.GetAll().Where(u => u.BridgeCode == input.BridgeCode && u.PassageType == input.PassageType).ToList();
            if (data.Count()>0) {
                throw new UserFriendlyException("添加失败", "桥架编号：" + input.BridgeCode + " 线缆类型：" + input.PassageType + " 已存在");
            }
            input.SectionalArea = (input.Weight * input.Height).ToString();
            var plotRatioall = _plotRatioRepository.GetAll().ToList();
            var plot = plotRatioall.FirstOrDefault(u => u.PassageType == input.PassageType.ToUpper());
            if (plot == null)
            {
                throw new UserFriendlyException("添加失败", " 线缆类型：" + input.PassageType + " 不存在");
            }
            input.PlotRatioLimit = plot.PlotRatioLimit;
            input.WeightLimit = BridgeCodeToWeightLimit(input);
            return base.Create(input);
        }
       
        public override Task<BridgeConstantDto> Update(BridgeConstantDto input)
        {
            var data = Repository.GetAll().Where(u => u.BridgeCode == input.BridgeCode && u.PassageType == input.PassageType&&u.Id!=input.Id).ToList();
            var plotRatioall = _plotRatioRepository.GetAll().ToList();
            if (data.Count() > 0)
            {
                throw new UserFriendlyException("修改失败", "桥架编号：" + input.BridgeCode + " 线缆类型：" + input.PassageType + " 已存在");
            }
            input.SectionalArea = (input.Weight * input.Height).ToString();
            var plot = plotRatioall.FirstOrDefault(u => u.PassageType == input.PassageType.ToUpper());
            if (plot == null) {
                throw new UserFriendlyException("修改失败", " 线缆类型：" + input.PassageType + " 不存在");
            }
            input.PlotRatioLimit = plot.PlotRatioLimit;
            input.WeightLimit = BridgeCodeToWeightLimit(input);
            return base.Update(input);
        }
        private string BridgeCodeToWeightLimit(BridgeConstantDto input)
        {
            var list = Repository.GetAll().Where(u => u.BridgeCode == input.BridgeCode && u.PassageType != input.PassageType).ToList();
            decimal sum = input.Weight;
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    sum += item.Weight;
                }
            }
            string weightLimit = "0";
            var c = _weightConstantRepository.GetAll().FirstOrDefault(u => u.WeightDecimal == sum);
            if (c == null)
            {
                throw new UserFriendlyException("修改失败", string.Format("桥架编号：{0} 重量限值：{1}  不在规定范围", input.BridgeCode, sum));
            }
            else
            {
                weightLimit = c.WeightLimit;
            }
            //switch (sum)
            //{
            //    case 1000M: weightLimit = "90"; break;
            //    case 800M: weightLimit = "90"; break;
            //    case 600M: weightLimit = "90"; break;
            //    case 500M: weightLimit = "90"; break;
            //    case 400M: weightLimit = "45"; break;
            //    case 350M: weightLimit = "30"; break;
            //    case 300M: weightLimit = "30"; break;
            //    case 200M: weightLimit = "20"; break;
            //    case 100M: weightLimit = "10"; break;
            //    case 50M: weightLimit = "10"; break;
            //    default: break;
            //}
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    item.WeightLimit = weightLimit;
                    Repository.Update(item);
                }
            }
            return weightLimit;
        }
        /// <summary>
        /// 返回与数组中所有条件都不匹配的文档
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        public List<BridgeConstant> GetListToNotIn(List<string> codes)
        {
            var query = MongoDB.Driver.Builders.Query<BridgeConstant>.NotIn(u => u.BridgeCode, codes);
            return Repository.GetList(query);
        }
    }
}
