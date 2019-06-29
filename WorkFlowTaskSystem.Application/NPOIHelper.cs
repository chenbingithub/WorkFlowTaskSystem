using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WorkFlowTaskSystem.Core.Damain.Entities;
using WorkFlowTaskSystem.Core.ViewModel;

namespace WorkFlowTaskSystem.Application
{
   public  class NPOIHelper
    {
        private IWorkbook workbook { get; set; }
        private string filepath { get; set;}
        
        #region 检验
        public NPOIHelper(string path)
        {
            this.filepath = path;
            if (!File.Exists(path))
            {
                if (path.IndexOf(".xlsx") > 0)
                {//2007版本
                    workbook = new XSSFWorkbook();
                }
                else if (path.IndexOf(".xls") > 0)
                {//2003版本
                    workbook = new HSSFWorkbook();
                }
                Createheader();

            }
            else
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    if (path.IndexOf(".xlsx") > 0)
                    {//2007版本
                        workbook = new XSSFWorkbook(fs);
                    }
                    else if (path.IndexOf(".xls") > 0)
                    {//2003版本
                        workbook = new HSSFWorkbook(fs);
                    }
                    else
                    {
                        //根据文件流创建excel数据结构
                        workbook = WorkbookFactory.Create(fs);
                    }
                    fs.Close();
                }
            }

        }
        private void Createheader()
        {
            var sheet1 = workbook.CreateSheet("桥架计算结果 A");
            var row1 = sheet1.CreateRow(0);
            row1.CreateCell(0).SetCellValue("序号");
            row1.CreateCell(1).SetCellValue("桥架编号");
            row1.CreateCell(2).SetCellValue("通道类型");
            row1.CreateCell(3).SetCellValue("桥架截面积");
            row1.CreateCell(4).SetCellValue("电缆截面积");
            row1.CreateCell(5).SetCellValue("电缆根数");
            row1.CreateCell(6).SetCellValue("容积率");
            row1.CreateCell(7).SetCellValue("容积率限值");
            row1.CreateCell(8).SetCellValue("电缆重量");
            row1.CreateCell(9).SetCellValue("重量限值");
            row1.CreateCell(10).SetCellValue("电缆编码");
            var sheet2 = workbook.CreateSheet("错误提示  B");
            var row = sheet2.CreateRow(0);
            row.CreateCell(0).SetCellValue("序号");
            row.CreateCell(1).SetCellValue("电缆编号");
            row.CreateCell(2).SetCellValue("电缆型号");
            row.CreateCell(3).SetCellValue("电缆规格");
            row.CreateCell(4).SetCellValue("所属安全通道");
            row.CreateCell(5).SetCellValue("电缆路径");
            row.CreateCell(6).SetCellValue("错误提示消息");
            workbook.CreateSheet("sheet3");
            var row3 = sheet1.CreateRow(2);
            row3.CreateCell(0).SetCellValue("序号");
            row3.CreateCell(1).SetCellValue("桥架编号");
            row3.CreateCell(2).SetCellValue("容积率");
            row3.CreateCell(3).SetCellValue("载重量");
        }
        public void InsertCableSummarizedBill(string path, CableSummarizedBill entity, string message, int rownum = 1)
        {
            var sheet = workbook.GetSheetAt(1);
            var row = sheet.CreateRow(rownum);
            row.CreateCell(0).SetCellValue(entity.A);
            row.CreateCell(1).SetCellValue(entity.C);
            row.CreateCell(2).SetCellValue(entity.U);
            row.CreateCell(3).SetCellValue(entity.V);
            row.CreateCell(4).SetCellValue(entity.Q);
            row.CreateCell(5).SetCellValue(entity.Z);
            row.CreateCell(6).SetCellValue(message);
        }
        public void InsertBridge(string path, string code, string area, string weightlimit, int rownum = 1)
        {
            var sheet = workbook.GetSheetAt(2);
            var row = sheet.CreateRow(rownum);
            row.CreateCell(0).SetCellValue(rownum);
            row.CreateCell(1).SetCellValue(code);
            row.CreateCell(2).SetCellValue(area);
            row.CreateCell(3).SetCellValue(weightlimit);
        }
        public void Insert(List<ReportView> source) {
            var sheet = workbook.GetSheetAt(0);
            var style = workbook.CreateCellStyle();
            style.FillPattern = FillPattern.SolidForeground;
            style.FillForegroundColor =NPOI.HSSF.Util.HSSFColor.Red.Index;
            for (int i = 0; i < source.Count(); i++) {
                var item = source[i];
                var row = sheet.CreateRow(i+1);
                row.CreateCell(0).SetCellValue(i + 1);
                row.CreateCell(1).SetCellValue(item.BridgeCode);
                row.CreateCell(2).SetCellValue(item.PassageType);
                row.CreateCell(3).SetCellValue(item.SectionalArea);
                row.CreateCell(4).SetCellValue(item.CableArea);
                row.CreateCell(5).SetCellValue(item.CableNumber);
                row.CreateCell(6).SetCellValue(item.PlotRatio);
                row.CreateCell(7).SetCellValue(item.PlotRatioLimit);
                if (item.PlotRatio > item.PlotRatioLimit) {
                    
                    row.GetCell(6).CellStyle = style;
                    row.GetCell(7).CellStyle = style;
                }
                row.CreateCell(8).SetCellValue(item.CableWeight);
                row.CreateCell(9).SetCellValue(item.WeightLimit);
                if (item.CableWeight > item.WeightLimit)
                {

                    row.GetCell(8).CellStyle = style;
                    row.GetCell(9).CellStyle = style;
                }
                row.CreateCell(10).SetCellValue(item.CableCodes);
            }
            
            
        }
        #endregion
        public NPOIHelper()
        {
            workbook = new XSSFWorkbook();
            workbook.CreateSheet();
        }
        
        public void Save() {
            using (FileStream fs = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                workbook.Write(fs);
                fs.Close();
            }
        }
    }

    public static class NPOIstaticHelper {
        private static IWorkbook workbook { get; set; }
        #region 转换格式
        public static void DesignSource(string path,List<CableLayingDetails> list) {
            CreateCableLayingDetailsHeader(path);
            int lastRowNum = workbook.GetSheetAt(0).LastRowNum;
            for (int i = 0; i< list.Count(); i++) {
                InsertCableLayingDetails(list[i], lastRowNum+ i + 1);
            }
            Save(path);
        }
        private static void CreateCableLayingDetailsHeader(string path)
        {
            if (!File.Exists(path))
            {
                if (path.IndexOf(".xlsx") > 0)
                {//2007版本
                    workbook = new XSSFWorkbook();
                }
                else if (path.IndexOf(".xls") > 0)
                {//2003版本
                    workbook = new HSSFWorkbook();
                }

                workbook.CreateSheet("sheet1");
            }
            else
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    if (path.IndexOf(".xlsx") > 0)
                    {//2007版本
                        workbook = new XSSFWorkbook(fs);
                    }
                    else if (path.IndexOf(".xls") > 0)
                    {//2003版本
                        workbook = new HSSFWorkbook(fs);
                    }
                    else
                    {
                        //根据文件流创建excel数据结构
                        workbook = WorkbookFactory.Create(fs);
                    }
                    fs.Close();
                }
            }

            var row1 = workbook.GetSheetAt(0).CreateRow(0);
            row1.CreateCell(0).SetCellValue("序号");
            row1.CreateCell(1).SetCellValue("系统号");
            row1.CreateCell(2).SetCellValue("电缆编号");
            row1.CreateCell(3).SetCellValue("图纸序号");
            row1.CreateCell(4).SetCellValue("有效性");
            row1.CreateCell(5).SetCellValue("图纸编号");
            row1.CreateCell(6).SetCellValue("图纸名称");
            row1.CreateCell(7).SetCellValue("版本");
            row1.CreateCell(8).SetCellValue("From房间");
            row1.CreateCell(9).SetCellValue("From系统名称");
            row1.CreateCell(10).SetCellValue("From设备名称");
            row1.CreateCell(11).SetCellValue("From设备KKS");
            row1.CreateCell(12).SetCellValue("To房间");
            row1.CreateCell(13).SetCellValue("To系统名称");
            row1.CreateCell(14).SetCellValue("To设备名称");
            row1.CreateCell(15).SetCellValue("To设备KKS");
            row1.CreateCell(16).SetCellValue("所属安全通道");
            row1.CreateCell(17).SetCellValue("压力壳贯穿件编码");
            row1.CreateCell(18).SetCellValue("舱室贯穿件编码");
            row1.CreateCell(19).SetCellValue("穿管编码");
            row1.CreateCell(20).SetCellValue("电缆型号");
            row1.CreateCell(21).SetCellValue("电缆规格");
            row1.CreateCell(22).SetCellValue("长度(m)");
            row1.CreateCell(23).SetCellValue("配管管径");
            row1.CreateCell(24).SetCellValue("护管长度(m)");
            row1.CreateCell(25).SetCellValue("电缆路径");
            row1.CreateCell(26).SetCellValue("其他");
            row1.CreateCell(27).SetCellValue("说明1（变更文件）");
            row1.CreateCell(28).SetCellValue("说明2（采购分交）");
            row1.CreateCell(29).SetCellValue("所属系统号");
            row1.CreateCell(30).SetCellValue("责任人");
            row1.CreateCell(31).SetCellValue("敷设状态");
            row1.CreateCell(32).SetCellValue("电缆盘号");
            row1.CreateCell(33).SetCellValue("电缆盘号");
            row1.CreateCell(34).SetCellValue("相同");
            row1.CreateCell(35).SetCellValue("起端刻度");
            row1.CreateCell(36).SetCellValue("终点刻度");
            row1.CreateCell(37).SetCellValue("实际长度");
            row1.CreateCell(38).SetCellValue("敷设日期");
            row1.CreateCell(39).SetCellValue("备注");
            row1.CreateCell(40).SetCellValue("端接状态");
            row1.CreateCell(41).SetCellValue("起端端接人");
            row1.CreateCell(42).SetCellValue("起端端接时间");
            row1.CreateCell(43).SetCellValue("终端端接人");
            row1.CreateCell(44).SetCellValue("终端端接时间");
            row1.CreateCell(45).SetCellValue("端接图");
            row1.CreateCell(46).SetCellValue("制约");
            row1.CreateCell(47).SetCellValue("备注");
        }
        private static void InsertCableLayingDetails(CableLayingDetails detailse, int rownum = 1)
        {
            var row = workbook.GetSheetAt(0).CreateRow(rownum);
            row.CreateCell(0).SetCellValue(rownum);
            row.CreateCell(1).SetCellValue("");
            row.CreateCell(2).SetCellValue(detailse.CableCode);
            row.CreateCell(8).SetCellValue(detailse.Start.RoomCode);
            row.CreateCell(9).SetCellValue(detailse.Start.SystemName);
            row.CreateCell(10).SetCellValue(detailse.Start.EquitName);
            row.CreateCell(11).SetCellValue(detailse.Start.EquitCode);
            row.CreateCell(12).SetCellValue(detailse.End.RoomCode);
            row.CreateCell(13).SetCellValue(detailse.End.SystemName);
            row.CreateCell(14).SetCellValue(detailse.End.EquitName);
            row.CreateCell(15).SetCellValue(detailse.End.EquitCode);
            row.CreateCell(16).SetCellValue(detailse.SafePassage);
            row.CreateCell(17).SetCellValue(detailse.PressureVesselCode);
            row.CreateCell(18).SetCellValue(detailse.CabinCode);
            row.CreateCell(19).SetCellValue(detailse.PipeCode);
            row.CreateCell(20).SetCellValue(detailse.Version);
            row.CreateCell(21).SetCellValue(detailse.Specification);
            row.CreateCell(22).SetCellValue(detailse.Length);
            row.CreateCell(23).SetCellValue(detailse.PipeSpecification);
            row.CreateCell(24).SetCellValue(detailse.PipeLength);
            row.CreateCell(25).SetCellValue(detailse.CablePath);
            row.CreateCell(26).SetCellValue(detailse.Other);
        }

        private static void Save(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                workbook.Write(fs);
                fs.Close();
            }
        }
        #endregion

        private static void CreateCableLayingCollectHeader(string path)
        {
            if (!File.Exists(path))
            {
                if (path.IndexOf(".xlsx") > 0)
                {//2007版本
                    workbook = new XSSFWorkbook();
                }
                else if (path.IndexOf(".xls") > 0)
                {//2003版本
                    workbook = new HSSFWorkbook();
                }

                workbook.CreateSheet("sheet1");
            }
            else
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    if (path.IndexOf(".xlsx") > 0)
                    {//2007版本
                        workbook = new XSSFWorkbook(fs);
                    }
                    else if (path.IndexOf(".xls") > 0)
                    {//2003版本
                        workbook = new HSSFWorkbook(fs);
                    }
                    else
                    {
                        //根据文件流创建excel数据结构
                        workbook = WorkbookFactory.Create(fs);
                    }
                    fs.Close();
                }
            }
            var style = workbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Center;
            style.VerticalAlignment = VerticalAlignment.Center;
            style.BorderBottom = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            style.BorderTop = BorderStyle.Thin;
            var row = workbook.GetSheetAt(0).CreateRow(0);
            for (int i = 0; i < 20; i++) {
                row.CreateCell(i).CellStyle= style;
            }
            row.GetCell(0).SetCellValue("电缆敷设清单");

            var row1 = workbook.GetSheetAt(0).CreateRow(1);
            for (int i = 0; i < 20; i++)
            {
                row1.CreateCell(i).CellStyle = style;
            }
            row1.GetCell(0).SetCellValue("序号");
            row1.GetCell(1).SetCellValue("电缆编码");
            row1.GetCell(2).SetCellValue("电缆起点");
            row1.GetCell(6).SetCellValue("电缆终点");
            row1.GetCell(10).SetCellValue("电缆敷设路径");
            row1.GetCell(14).SetCellValue("电缆参数");
            row1.GetCell(16).SetCellValue("长度(m)");
            row1.GetCell(17).SetCellValue("护管规格(公称口径)");
            row1.GetCell(18).SetCellValue("护管长度(m)");
            row1.GetCell(19).SetCellValue("其他");
            var row2 = workbook.GetSheetAt(0).CreateRow(2);
            for (int i = 0; i < 20; i++)
            {
                row2.CreateCell(i).CellStyle = style;
            }
            row2.GetCell(2).SetCellValue("房间编码");
            row2.GetCell(3).SetCellValue("系统名称");
            row2.GetCell(4).SetCellValue("设备名称");
            row2.GetCell(5).SetCellValue("设备编码");
            row2.GetCell(6).SetCellValue("房间编码");
            row2.GetCell(7).SetCellValue("系统名称");
            row2.GetCell(8).SetCellValue("设备名称");
            row2.GetCell(9).SetCellValue("设备编码");
            row2.GetCell(10).SetCellValue("所属安全通道");
            row2.GetCell(11).SetCellValue("压力壳贯穿件编码");
            row2.GetCell(12).SetCellValue("舱室贯穿件编码");
            row2.GetCell(13).SetCellValue("穿管编码");
            row2.GetCell(14).SetCellValue("型号");
            row2.GetCell(15).SetCellValue("规格");
            workbook.GetSheetAt(0).AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(0, 0, 0, 19));

            workbook.GetSheetAt(0).AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 0, 0));
            workbook.GetSheetAt(0).AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 1, 1));
            workbook.GetSheetAt(0).AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 16, 16));
            workbook.GetSheetAt(0).AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 17, 17));
            workbook.GetSheetAt(0).AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 18, 18));
            workbook.GetSheetAt(0).AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 2, 19, 19));

            workbook.GetSheetAt(0).AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 2, 5));
            workbook.GetSheetAt(0).AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 6,9));
            workbook.GetSheetAt(0).AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 10, 13));
            workbook.GetSheetAt(0).AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(1, 1, 14, 15));

           
        }

        public static void DesignSourceCollect(string path, List<CableLayingDetails> list)
        {
            CreateCableLayingCollectHeader(path);
            int lastRowNum = workbook.GetSheetAt(0).LastRowNum;
            for (int i = 0; i < list.Count(); i++)
            {
                InsertCableLayingeCollect(list[i], lastRowNum + (i+1)*2);
            }
            Save(path);
        }
        private static void InsertCableLayingeCollect(CableLayingDetails detailse, int rownum = 1)
        {
            var row = workbook.GetSheetAt(0).CreateRow(rownum-1);
            row.CreateCell(0).SetCellValue((rownum-2)/2);
            row.CreateCell(1).SetCellValue(detailse.CableCode);
            row.CreateCell(2).SetCellValue(detailse.Start.RoomCode);
            row.CreateCell(3).SetCellValue(detailse.Start.SystemName);
            row.CreateCell(4).SetCellValue(detailse.Start.EquitName);
            row.CreateCell(5).SetCellValue(detailse.Start.EquitCode);
            row.CreateCell(6).SetCellValue(detailse.End.RoomCode);
            row.CreateCell(7).SetCellValue(detailse.End.SystemName);
            row.CreateCell(8).SetCellValue(detailse.End.EquitName);
            row.CreateCell(9).SetCellValue(detailse.End.EquitCode);
            row.CreateCell(10).SetCellValue(detailse.SafePassage);
            row.CreateCell(11).SetCellValue(detailse.PressureVesselCode);
            row.CreateCell(12).SetCellValue(detailse.CabinCode);
            row.CreateCell(13).SetCellValue(detailse.PipeCode);
            row.CreateCell(14).SetCellValue(detailse.Version);
            row.CreateCell(15).SetCellValue(detailse.Specification);
            row.CreateCell(16).SetCellValue(detailse.Length);
            row.CreateCell(17).SetCellValue(detailse.PipeSpecification);
            row.CreateCell(18).SetCellValue(detailse.PipeLength);
            row.CreateCell(19).SetCellValue(detailse.Other);
           
            var row1 = workbook.GetSheetAt(0).CreateRow(rownum);
            for (int i = 0; i < 20; i++)
            {
                row1.CreateCell(i);
            }
            row1.CreateCell(2).SetCellValue(detailse.CablePath);

            workbook.GetSheetAt(0).AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(rownum-1, rownum, 0, 0));
            workbook.GetSheetAt(0).AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(rownum-1, rownum, 1, 1));

            workbook.GetSheetAt(0).AddMergedRegion(new NPOI.SS.Util.CellRangeAddress(rownum, rownum, 2, 19));
        }
    }
}
