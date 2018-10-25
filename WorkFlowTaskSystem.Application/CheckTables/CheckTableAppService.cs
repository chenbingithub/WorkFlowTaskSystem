﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Runtime.Caching;
using Aspose.Cells;
using Microsoft.AspNetCore.Hosting;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
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
            int error = 0;
            string  col = "";
            try {
                //根据指定路径读取文件
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    IWorkbook wb;
                    if (path.IndexOf(".xlsx") > 0)
                    {//2007版本
                        wb = new XSSFWorkbook(fs);
                    }
                    else if (path.IndexOf(".xls") > 0)
                    {//2003版本
                        wb = new HSSFWorkbook(fs);
                    }
                    else {
                        //根据文件流创建excel数据结构
                         wb = WorkbookFactory.Create(fs);
                    }
                    
                    // Workbook wb = new Workbook(path);
                    var sheet = wb.GetSheetAt(0);
                    int firstRow = 4;
                    for (int i = 4; i < sheet.LastRowNum; i++)
                    {
                        var v = sheet.GetRow(i);
                        if (sheet.GetRow(i).GetCell(0).ToString() == "1")
                        {
                            firstRow = i;
                            break;
                        }
                    }

                    for (int i = firstRow; i < sheet.LastRowNum;)
                    {
                        error = i + 1 ;
                        
                        IRow row = sheet.GetRow(i);
                        
                        CableLayingDetails entity = new CableLayingDetails();
                        col = "B";
                        entity.CableCode = (row.GetCell(1)).ToString().Replace(" ", "").Trim();
                        col = "C";
                        entity.Start.RoomCode = (row.GetCell(2)).ToString().Trim();
                        col ="D";
                        entity.Start.SystemName = (row.GetCell(3)).ToString().Trim();
                        col = "E";
                        entity.Start.EquitName = (row.GetCell(4)).ToString().Trim();
                        col = "F";
                        entity.Start.EquitCode = (row.GetCell(5)).ToString().Trim();
                        col = "G";
                        entity.End.RoomCode = (row.GetCell(6)).ToString().Trim();
                        col = "H";
                        entity.End.SystemName = (row.GetCell(7)).ToString().Trim();
                        col = "I";
                        entity.End.EquitName = (row.GetCell(8)).ToString().Trim();
                        col = "J";
                        entity.End.EquitCode = (row.GetCell(9)).ToString().Trim();
                        col = "K";
                        entity.SafePassage = (row.GetCell(10)).ToString().Trim();
                        col = "L";
                        entity.PressureVesselCode = (row.GetCell(11)).ToString().Trim();
                        col = "M";
                        entity.CabinCode = (row.GetCell(12)).ToString().Trim();
                        col = "N";
                        entity.PipeCode = (row.GetCell(13)).ToString().Trim();
                        col = "O";
                        entity.Version = (row.GetCell(14)).ToString().Trim();
                        col = "P";
                        entity.Specification = (row.GetCell(15)).ToString().Trim();
                        col = "Q";
                        entity.Length = (row.GetCell(16)).ToString().Trim();
                        col = "R";
                        entity.PipeSpecification = (row.GetCell(17)).ToString().Trim();
                        col = "S";
                        entity.PipeLength = (row.GetCell(18)).ToString().Trim();
                        col = "T";
                        entity.Other = (row.GetCell(19)).ToString().Trim();
                        col = "C";
                        error++;
                        entity.CablePath = (sheet.GetRow(i + 1).GetCell(2)).ToString().Trim().Replace("电缆路径：", "");
                        entity.Description = numberNo;
                        entity.Id = Guid.NewGuid().ToString("N");
                        _cableRepository.Insert(entity);
                        i += 2;
                    }
                }

            } catch (Exception e) {
                Console.WriteLine(e.Message);
                Logger.Debug(path + "  " +"第 " + error + " 行");
                throw new Exception(" 第 "+error+" 行 第 "+col +" 列");
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
                        
            var cable = cableConstantall.FirstOrDefault(u => u.Version == entityDto.U && u.Specification.Replace("×", "x") == entityDto.V.Replace("×", "x"));
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
             _cableSummarizedBillRepository.Insert(entity);
            //InsertBridgeInstances(numberNo, entity.Q, entity.Z, bridgeConstantall, instances);
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
