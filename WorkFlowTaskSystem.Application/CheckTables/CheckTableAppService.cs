using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using Aspose.Cells;
using Microsoft.AspNetCore.Hosting;
using WorkFlowTaskSystem.Application.CheckTables.Dto;
using WorkFlowTaskSystem.Application.Forms.Dto;
using WorkFlowTaskSystem.Core.Damain.Entities;
using WorkFlowTaskSystem.Core.Damain.Repositories;
using WorkFlowTaskSystem.MongoDb;

namespace WorkFlowTaskSystem.Application.CheckTables
{ 
    /// <summary>
    /// 校验电缆清单、计算容积率、计算载重量
    /// </summary>
    public class CheckTableAppService : ApplicationService, ICheckTableAppService
    {
      private IRepository<CableLayingDetails, string> _cableRepository;
      private IRepository<CableSummarizedBill, string> _cableSummarizedBillRepository;
      private IRepository<BridgeInstances, string> _bridgeInstancesRepository;
      private IRepository<BridgeConstant, string> _bridgeConstantRepository;
      private IRepository<CableConstant, string> _cableConstantRepository;
      private IRepository<ReportResult, string> _reportResultRepository;
    private IHostingEnvironment _hostingEnvironment;

    public CheckTableAppService(IRepository<CableLayingDetails, string> cableRepository, IRepository<CableSummarizedBill, string> cableSummarizedBillRepository, IRepository<BridgeInstances, string> bridgeInstancesRepository, IRepository<BridgeConstant, string> bridgeConstantRepository, IRepository<CableConstant, string> cableConstantRepository, IHostingEnvironment hostingEnvironment, IRepository<ReportResult, string> reportResultRepository)
    {
      _cableRepository = cableRepository;
      _cableSummarizedBillRepository = cableSummarizedBillRepository;
      _bridgeInstancesRepository = bridgeInstancesRepository;
      _bridgeConstantRepository = bridgeConstantRepository;
      _cableConstantRepository = cableConstantRepository;
      _hostingEnvironment = hostingEnvironment;
      _reportResultRepository = reportResultRepository;
    }

     

     #region 校验
      /// <summary>
      /// 导入电缆设计清单
      /// </summary>
      /// <param name="numberNo">流水号</param>
      /// <param name="path">文件路径</param>
      public void InsertCableLayingDetails(string numberNo, string path)
      {
        Workbook wb = new Workbook(path);
        var sheet = wb.Worksheets[0];
        for (int i = 4; i < sheet.Cells.MaxRow + 1;)
        {
          CableLayingDetails entity = new CableLayingDetails();
          entity.CableCode = (sheet.Cells[i, 1].Value ?? "").ToString().Replace(" ", "").Trim();
          entity.Start.RoomCode = (sheet.Cells[i, 2].Value ?? "").ToString().Trim();
          entity.Start.SystemName = (sheet.Cells[i, 3].Value ?? "").ToString().Trim();
          entity.Start.EquitName = (sheet.Cells[i, 4].Value ?? "").ToString().Trim();
          entity.Start.EquitCode = (sheet.Cells[i, 5].Value ?? "").ToString().Trim();
          entity.End.RoomCode = (sheet.Cells[i, 6].Value ?? "").ToString().Trim();
          entity.End.SystemName = (sheet.Cells[i, 7].Value ?? "").ToString().Trim();
          entity.End.EquitName = (sheet.Cells[i, 8].Value ?? "").ToString().Trim();
          entity.End.EquitCode = (sheet.Cells[i, 9].Value ?? "").ToString().Trim();
          entity.SafePassage = (sheet.Cells[i, 10].Value ?? "").ToString().Trim();
          entity.PressureVesselCode = (sheet.Cells[i, 11].Value ?? "").ToString().Trim();
          entity.CabinCode = (sheet.Cells[i, 12].Value ?? "").ToString().Trim();
          entity.PipeCode = (sheet.Cells[i, 13].Value ?? "").ToString().Trim();
          entity.Version = (sheet.Cells[i, 14].Value ?? "").ToString().Trim();
          entity.Specification = (sheet.Cells[i, 15].Value ?? "").ToString().Trim();
          entity.Length = (sheet.Cells[i, 16].Value ?? "").ToString().Trim();
          entity.PipeSpecification = (sheet.Cells[i, 17].Value ?? "").ToString().Trim();
          entity.PipeLength = (sheet.Cells[i, 18].Value ?? "").ToString().Trim();
          entity.Other = (sheet.Cells[i, 19].Value ?? "").ToString().Trim();
          entity.CablePath = (sheet.Cells[i + 1, 2].Value ?? "").ToString().Trim().Replace("电缆路径：", "");
          entity.Description = numberNo;
          entity.Id = Guid.NewGuid().ToString("N");
          _cableRepository.Insert(entity);
          i += 2;
        }
      }
      /// <summary>
      /// 获取电缆设计清单
      /// </summary>
      /// <param name="numberNo">流水号</param>
      /// <returns></returns>
      public List<CableLayingDetails> GetCableLayingDetailsListByNumberNo(string numberNo)
      {
        var query = MongoDB.Driver.Builders.Query<CableLayingDetails>.EQ(u => u.Description, numberNo);
        return _cableRepository.GetList(query);
      }
    #endregion

     #region 导入电缆汇总清单、计算容积率、载重量
    /// <summary>
    /// 导入电缆汇总清单
    /// </summary>
    /// <param name="numberNo">流水号</param>
    /// <param name="path">文件路径</param>
    public void InsertCableSummarizedBill(string numberNo, string path)
    {
      var cableConstantall = _cableConstantRepository.GetAll().ToList();
      var bridgeConstantall = _bridgeConstantRepository.GetAll().ToList();


      try
      {
        string file = _hostingEnvironment.WebRootPath + "/upload/"+ numberNo+".xlsx";
        Workbook wb = new Workbook(path);
        Workbook reportwb = new Workbook();
        var sheet = wb.Worksheets[0];
        int errorcol = 1;
        List<CableSummarizedBill> list = new List<CableSummarizedBill>();
        List<BridgeInstances> instances = new List<BridgeInstances>();
        for (int i = 1; i < sheet.Cells.MaxRow + 1; i++)
        {
          CableSummarizedBillDto entityDto = new CableSummarizedBillDto();
          PropertyInfo[] propertys = entityDto.GetType().GetProperties();
          for (int j = 0; j < propertys.Length; j++)
          {
            propertys[j].SetValue(entityDto, (sheet.Cells[i, j].Value ?? "").ToString().Trim());
          }
          var entity = new CableSummarizedBill();
          entity.Id = Guid.NewGuid().ToString("N");
          entity.Description = numberNo;
          ObjectMapper.Map(entityDto, entity);
          try
          {
            var cable = cableConstantall.FirstOrDefault(u => u.Version == entityDto.U && u.Specification == entityDto.V);
            entity.WeightLimit = 999;
            entity.Diameter = 999;
            if (double.TryParse(cable.WeightLimit, out double weightLimit))
            {
              entity.WeightLimit = weightLimit;
            }
            if (double.TryParse(cable.Diameter, out double diameter))
            {
              entity.Diameter = diameter;
            }
            list.Add(entity);
            InsertBridgeInstances(numberNo, entity.Q, entity.Z, bridgeConstantall, instances);
            //同时把电缆路径拆分，插入桥架实例表中
          }
          catch (Exception e)
          {
            reportwb.Worksheets[0].Cells[errorcol, 4].PutValue(entity.A);
            reportwb.Worksheets[0].Cells[errorcol, 5].PutValue(entity.B);
            reportwb.Worksheets[0].Cells[errorcol, 6].PutValue(entity.C);
            reportwb.Worksheets[0].Cells[errorcol, 7].PutValue(entity.Q);
            reportwb.Worksheets[0].Cells[errorcol, 8].PutValue(entity.Z);
            errorcol++;
          }
        }
        _bridgeInstancesRepository.InsertBatch(instances);
        _cableSummarizedBillRepository.InsertBatch(list);
        reportwb.Save(file);
        _reportResultRepository.Insert(new ReportResult() {Id=Guid.NewGuid().ToString("N"),Name = "生成计算容积率报告成功", Url = numberNo+".xlsx", Description = numberNo });
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }
     
    }
    /// <summary>
    /// 导入桥架实例
    /// </summary>
    /// <param name="numberNo">流水号</param>
    /// <param name="Q">电缆汇总清单Q列,实际为通道类型</param>
    /// <param name="Z">电缆汇总清单Z列,实际为电缆路径</param>
    private void InsertBridgeInstances(string numberNo, string Q, string Z,List<BridgeConstant> source,List<BridgeInstances> inserts)
    {
      try
      {
        var codes = Z.Split(',');
        for (int i = 1; i < codes.Length-1; i++)
        {
          var code = codes[i];
          var bridge = source.FirstOrDefault(u=>u.BridgeCode==code&&u.PassageType==Q);
          BridgeInstances entity = new BridgeInstances();
          entity.Id = Guid.NewGuid().ToString("N");
          entity.Description = numberNo;
          entity.BridgeCode = bridge.BridgeCode;
          entity.PassageType = bridge.PassageType;
          entity.WeightLimit = bridge.WeightLimit;
          entity.SectionalArea = bridge.SectionalArea;
          entity.PlotRatioLimit = bridge.PlotRatioLimit;
          if (!inserts.Exists(u=> u.BridgeCode == code && u.PassageType == Q&&u.Description== numberNo))
          {
            inserts.Add(entity);
          }
        }
       
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }
      
    }
    /// <summary>
    /// 根据流水号获取桥架
    /// </summary>
    /// <param name="numberNo">流水号</param>
    /// <returns></returns>
    public List<BridgeInstances> GetBridgeInstancesListByNumberNo(string numberNo)
    {
      var query = MongoDB.Driver.Builders.Query<BridgeInstances>.EQ(u => u.Description, numberNo);
      return _bridgeInstancesRepository.GetList(query);
    }
    /// <summary>
    /// 电缆截面积求和
    /// </summary>
    /// <param name="numberNo">流水号</param>
    /// <param name="bridgeCode">桥架编码</param>
    /// <param name="passageType">通道类型</param>
    /// <returns></returns>
      public double SummationCableSectionalArea(string numberNo, string bridgeCode, string passageType)
      {
        try
        {
          var query = MongoDB.Driver.Builders.Query<CableSummarizedBill>.EQ(u => u.Description, numberNo);
          var query1 = MongoDB.Driver.Builders.Query<CableSummarizedBill>.EQ(u => u.Q, passageType);
          var query2 = MongoDB.Driver.Builders.Query<CableSummarizedBill>.Matches(u => u.Z, $@"/{bridgeCode}/");
          var clist = _cableSummarizedBillRepository.GetList(MongoDB.Driver.Builders.Query.And(query2, query1, query));
          double sum = 0;
          foreach (var bill in clist)
          {
            var diameter = bill.Diameter;
            sum += diameter * diameter;
          }
          return sum;
      }
        catch (Exception e)
        {
          Console.WriteLine(e);
          throw;
        }
        
      }
    /// <summary>
    /// 电缆重量求和
    /// </summary>
    /// <param name="numberNo">流水号</param>
    /// <param name="bridgeCode">桥架编码</param>
    /// <returns></returns>
    public double SummationCableWeight(string numberNo, string bridgeCode)
    {
      try
      {
        var query = MongoDB.Driver.Builders.Query<CableSummarizedBill>.EQ(u => u.Description, numberNo);
        var query2 = MongoDB.Driver.Builders.Query<CableSummarizedBill>.Matches(u => u.Z, $@"/{bridgeCode}/");
        var clist = _cableSummarizedBillRepository.GetList(MongoDB.Driver.Builders.Query.And(query2, query));
        double sum = 0;
        foreach (var bill in clist)
        {
          var weightLimit = bill.WeightLimit;
          sum += weightLimit * weightLimit;
        }
        return sum;
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        throw;
      }
      
    }
    #endregion
  }
}
