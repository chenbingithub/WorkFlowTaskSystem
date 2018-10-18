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

    public CheckTableAppService(IRepository<CableLayingDetails, string> cableRepository, IRepository<CableSummarizedBill, string> cableSummarizedBillRepository, IRepository<BridgeInstances, string> bridgeInstancesRepository, IRepository<BridgeConstant, string> bridgeConstantRepository, IRepository<CableConstant, string> cableConstantRepository)
    {
      _cableRepository = cableRepository;
      _cableSummarizedBillRepository = cableSummarizedBillRepository;
      _bridgeInstancesRepository = bridgeInstancesRepository;
      _bridgeConstantRepository = bridgeConstantRepository;
      _cableConstantRepository = cableConstantRepository;
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
      try
      {
        Workbook wb = new Workbook(path);
        var sheet = wb.Worksheets[0];
        for (int i = 1; i < sheet.Cells.MaxRow + 1; i++)
        {
          CableSummarizedBillDto entityDto = new CableSummarizedBillDto();
          PropertyInfo[] propertys = entityDto.GetType().GetProperties();
          for (int j = 0; j < propertys.Length; j++)
          {
            propertys[j].SetValue(entityDto, (sheet.Cells[i, j].Value ?? "").ToString().Trim());
          }
          CableSummarizedBill entity = new CableSummarizedBill();
          entity.Id = Guid.NewGuid().ToString("N");
          entity.Description = numberNo;
          ObjectMapper.Map(entityDto, entity);
          _cableSummarizedBillRepository.Insert(entity);
          //同时把电缆路径拆分，插入桥架实例表中
          InsertBridgeInstances(numberNo, entity.Q, entity.Z);
        }
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
    private void InsertBridgeInstances(string numberNo, string Q, string Z)
    {
      try
      {
        var codes = Z.Split(',');
        foreach (var code in codes)
        {
          var query = MongoDB.Driver.Builders.Query<BridgeConstant>.EQ(u => u.Description, numberNo);
          var query1 = MongoDB.Driver.Builders.Query<BridgeConstant>.EQ(u => u.BridgeCode, code);
          var query2 = MongoDB.Driver.Builders.Query<BridgeConstant>.EQ(u => u.PassageType, Q);
          var bridge = _bridgeConstantRepository.Get(MongoDB.Driver.Builders.Query.And(query2, query1));
          BridgeInstances entity = new BridgeInstances();
          entity.Id = Guid.NewGuid().ToString("N");
          entity.Description = numberNo;
          entity.BridgeCode = bridge.BridgeCode;
          entity.PassageType = bridge.PassageType;
          entity.WeightLimit = bridge.WeightLimit;
          entity.SectionalArea = bridge.SectionalArea;
          entity.PlotRatioLimit = bridge.PlotRatioLimit;
          if (_bridgeInstancesRepository.Get(MongoDB.Driver.Builders.Query.And(query2, query1, query)) == null)
          {
            _bridgeInstancesRepository.Insert(entity);
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
          var cableConstantall = _cableConstantRepository.GetAll().ToList();
          double sum = 0;
          foreach (var bill in clist)
          {
            var cable = cableConstantall.Single(u => u.Version == bill.U && u.Specification == bill.V);
            var diameter = double.Parse(cable.Diameter);
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
        var cableConstantall = _cableConstantRepository.GetAll().ToList();
        double sum = 0;
        foreach (var bill in clist)
        {
          var cable = cableConstantall.Single(u => u.Version == bill.U && u.Specification == bill.V);
          var weightLimit = double.Parse(cable.WeightLimit);
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
