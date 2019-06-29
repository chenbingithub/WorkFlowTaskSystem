//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Reflection;

//using Aspose.Cells;
//using Aspose.Cells.Rendering;
//using System.Drawing;
//using System.IO;
//using System.Data;
//using System.Collections;
//using System.Drawing.Drawing2D;
//using System.Drawing.Imaging;
//using Newtonsoft.Json.Linq;


//namespace System
//{
//    /// <summary>
//    /// 报表库 公共方法类
//    /// 
//    /// ThinkWang
//    /// 2013-8-16
//    /// </summary>
//    public class Common
//    {
//      /// <summary>
//      /// 绑定表头相关信息
//      /// 
//      /// ThinkWang
//      /// </summary>
//      /// <param name="designer"></param>
//      /// <param name="entiry"></param>
//      public static void BindReportHeader(WorkbookDesigner designer, JObject entiry)
//        {
//            //InsertQRCode(designer, entiry);//插入二维码
           
//        }

//    #region  签名、水印、只读保护

//    /// <summary>
//    /// 处理报表公共
//    /// 包括 签名、水印、只读保护
//    /// 
//    /// ThinkWang
//    /// </summary>
//    /// <param name="book"></param>
//    /// <param name="hasSign">是否签名</param>
//    /// <param name="IsProtected">是否只读保护</param>
//    /// <param name="waterType">水印类型</param>
//    public static void ProcessReport(Workbook book, bool hasSign = true, bool IsProtected = true, bool isWater = false, WaterType waterType = WaterType.Txt)
//        {
//            bool isApproved = false;//是否 已经批复
           
//            foreach (Worksheet sheet in book.Worksheets)
//            {
              
//              //电子签名、意见、日期
//                InsertSign(sheet, hasSign);
//              if (isWater)
//              {
//                //添加水印
//                InsertWater(sheet, waterType, isApproved);
//               }
              
//                //只读保护
//                if (!sheet.IsProtected && IsProtected)
//                    sheet.Protect(ProtectionType.All, "kr_pay_kingroad", "");
//            }
//        }

//        /// <summary>
//        /// 电子签名、意见、日期
//        /// </summary>
//        /// <param name="sheet"></param>
//        /// <param name="ideaList"></param>
//        /// <param name="hasSign"></param>
//        private static void InsertSign(Worksheet sheet, bool hasSign)
//        {
//            try
//            {
//                Cell signCell = sheet.Cells.FindStringStartsWith("sign_", null);
//                while (signCell != null)
//                {
                  
//                    signCell = sheet.Cells.FindStringStartsWith("sign_", null);//查找下一个
//                }
//            }
//            catch (Exception ex)
//            {
//              Console.WriteLine(ex.Message);
//            }
//        }

//        /// <summary>
//        /// 添加水印
//        /// </summary>
//        /// <param name="sheet"></param>
//        /// <param name="type"></param>
//        /// <param name="isApproved"></param>
//        private static void InsertWater(Worksheet sheet, WaterType type, bool isApproved)
//        {
//            int top = 0;//水印距离上部距离
//            int left = 0;//水印距离左侧距离
//            if (sheet.PageSetup.Orientation == PageOrientationType.Landscape)//是否是横向
//            {
//                top = 200;
//                left = 350;
//            }
//            else
//            {
//                top = 350;
//                left = 130;
//            }
//            string waterContent = "未批复";
//          string waterImgPath = "/Content/images/ypf.png"; //System.Web.HttpContext.Current.Server.MapPath(isApproved ? "/Content/images/ypf.png" : "/Content/images/wpf.png");//水印图片路径

//            ImageOrPrintOptions opt = new ImageOrPrintOptions();
//            CellArea[] areas = sheet.GetPrintingPageBreaks(opt);
//            foreach (CellArea area in areas)
//            {
//                if (area.StartColumn != 0)
//                    continue;

//                if (File.Exists(waterImgPath))
//                {//是图片水印，且 图片存在，且 已批复
//                    Image img = Image.FromFile(waterImgPath);
//                    MemoryStream stream = new MemoryStream();
//                    img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

//                    var waterImg = sheet.Shapes.AddPicture(area.StartRow, area.StartColumn, stream, img.Width, img.Height);
//                    waterImg.Placement = Aspose.Cells.Drawing.PlacementType.FreeFloating;//自由移动
//                    waterImg.Width = img.Width;
//                    waterImg.Height = img.Height;

//                    waterImg.X = left;
//                    waterImg.Y += top;
//                    stream.Dispose();
//                    img.Dispose();
//                }
//                else
//                {
//                    if (isApproved)
//                        waterContent = "已批复";

//                    Aspose.Cells.Drawing.Shape wordart = sheet.Shapes.AddTextEffect(Aspose.Cells.Drawing.MsoPresetTextEffect.TextEffect1,
//                           waterContent, "宋体", 60, false, false, area.StartRow, top, area.StartColumn, left, 150, 450);//row,top,col,left,height,width
//                    wordart.Placement = Aspose.Cells.Drawing.PlacementType.FreeFloating;//自由移动
//                    wordart.LineFormat.IsVisible = false;
//                    wordart.FillFormat.IsVisible = true;
//                    wordart.FillFormat.Transparency = 0.95;//透明度
//                    wordart.Fill.Transparency = 1;
//                    wordart.TextOptions.Fill.Transparency = 0.65;

//                    wordart.FillFormat.ForeColor = System.Drawing.Color.FromArgb(130, 130, 130);
//                    wordart.RotationAngle = -45;//旋转25°
//                }
//            }
//        }

//        #endregion

//        #region 获取分页数据 的集合

//        /// <summary>
//        /// 添加空白行数据 用于分页
//        /// </summary>
//        /// <param name="data"></param>
//        /// <param name="maxRows"></param>
//        /// <param name="minRows"></param>
//        /// <returns></returns>
//        public static DataTable GetPagerDate(DataTable data, int maxRows, int minRows)
//        {
//            //记录空白行
//            int emptyRows = 0;//需添加的空白行
//            if (data.Rows.Count <= minRows)
//                emptyRows = minRows - data.Rows.Count;
//            else
//                emptyRows = maxRows - (data.Rows.Count - minRows) % maxRows;
//            //添加空白行
//            for (int i = 0; i < emptyRows; i++)
//                data.Rows.Add(data.NewRow());

//            return data;
//        }
//        /// <summary>
//        /// 获取分页数据
//        /// </summary>
//        /// <param name="data">源数据</param>
//        /// <param name="rowCount">每页的数据行数</param>
//        /// <returns>分页数据 的集合</returns>
//        public static List<DataTable> GetPagerDate(DataTable data, int rowCount)
//        {
//            List<DataTable> list = new List<DataTable>();
//            DataTable pagerTable = null;//每个分页数据
//            if (data != null)
//            {
//                int yushu = 0;//余数
//                int pager = Math.DivRem(data.Rows.Count, rowCount, out yushu);//分页数
//                if (yushu != 0)
//                {
//                    pager++;
//                    for (int i = 0; i < rowCount - yushu; i++)
//                    {
//                        data.Rows.Add(data.NewRow());
//                    }
//                }

//                for (int i = 0; i < pager; i++)
//                {
//                    pagerTable = data.//AsEnumerable().Skip(i * rowCount).Take(rowCount).CopyToDataTable();
//                    list.Add(pagerTable);
//                }
//            }
//            if (list.Count == 0)
//            {
//                pagerTable = data.Copy();
//                pagerTable.Clear();
//                for (int i = 0; i < rowCount; i++)
//                {
//                    pagerTable.Rows.Add(pagerTable.NewRow());
//                }
//                list.Add(pagerTable);
//            }
//            return list;
//        }
//        /// <summary>
//        ///  获取分页数据
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="data">源数据</param>
//        /// <param name="rowCount">每页的数据行数</param>
//        /// <returns>分页数据 的集合</returns>
//        public static List<List<T>> GetPagerDate<T>(List<T> data, int rowCount) where T : class,new()
//        {
//            List<List<T>> list = new List<List<T>>();
//            List<T> pagerList = null;//每个分页数据
//            if (data != null && data.Count > 0)
//            {
//                int yushu = 0;//余数
//                int pager = Math.DivRem(data.Count, rowCount, out yushu);//分页数
//                if (yushu != 0)
//                {
//                    pager++;
//                    for (int i = 0; i < rowCount - yushu; i++)
//                    {
//                        data.Add(new T());
//                    }
//                }

//                for (int i = 0; i < pager; i++)
//                {
//                    pagerList = data.GetRange(i * rowCount, rowCount);
//                    list.Add(pagerList);
//                }
//            }
//            else
//            {
//                for (int i = 0; i < rowCount; i++)
//                {
//                    data.Add(new T());
//                }
//                list.Add(data);
//            }
//            return list;
//        }

//        /// <summary>
//        ///  获取分页后的WorkbookDesigner
//        ///  每页都保留表头和表尾，
//        ///  一个sheet一页。
//        /// </summary>
//        /// <param name="designer"></param>
//        /// <param name="dt"></param>
//        /// <param name="rowCount"></param>
//        /// <param name="sheetNamePrefix">sheet名字的前缀</param>
//        /// <returns></returns>
//        public static WorkbookDesigner GetPagerSheet(WorkbookDesigner designer, DataTable dt, int rowCount, string sheetNamePrefix = "")
//        {
//            if (string.IsNullOrEmpty(sheetNamePrefix))
//                sheetNamePrefix = designer.Workbook.Worksheets[0].Name;
//            List<DataTable> pagerCollections = Common.GetPagerDate(dt, rowCount);

//            for (int i = 0; i < pagerCollections.Count; i++)
//            {
//                //不是最后一 个worksheet，就复制模板
//                if (i < pagerCollections.Count - 1)
//                {
//                    designer.Workbook.Worksheets.Add("").Copy(designer.Workbook.Worksheets[i]);
//                }

//                designer.ClearDataSource();
//                pagerCollections[i].TableName = "DT";
//                designer.SetDataSource(pagerCollections[i]);

//                designer.Process(i, true);//处理模板
//                designer.Workbook.Worksheets[i].Name = string.Format("{0}-{1}", sheetNamePrefix, i + 1); //设置sheet名称                
//            }
//            return designer;
//        }
//        #endregion

//        /// <summary>
//        /// 插入草图，并按比例缩放
//        /// ThinkWang
//        /// 2012-10-28 
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="workbook"></param>
//        /// <param name="picPath"></param>
//        public static void InserDraft(string key, Worksheet sheet, string picPath)
//        {
//            Cell signCell = sheet.Cells.FindString(key + "：", null);
//            if (signCell == null)
//                return;
//            //d.插入图片  
//            if (!string.IsNullOrEmpty(picPath))
//            {
//                picPath = System.Web.HttpContext.Current.Server.MapPath(picPath);
//                if (System.IO.File.Exists(picPath))
//                {
//                    Image img = Image.FromFile(picPath);
//                    MemoryStream streamImg = new MemoryStream();
//                    img.Save(streamImg, System.Drawing.Imaging.ImageFormat.Jpeg);

//                    signCell = sheet.Cells[signCell.Row + 1, signCell.Column];
//                    //过大的话，等比例缩放
//                    int cellHeight = sheet.Cells.GetRowHeightPixel(signCell.Row);//单元格高度
//                    int cellWidht = sheet.Cells.GetColumnWidthPixel(signCell.Column);//单元格宽度
//                    int width = img.Width;//缩放后的宽度
//                    int height = img.Height;//缩放后的高度
//                    if (signCell.IsMerged)//如果单元格合并，就取合并区域的宽度
//                    {
//                        for (int i = 1; i < signCell.GetMergedRange().ColumnCount - 1; i++)
//                        {
//                            cellWidht += sheet.Cells.GetColumnWidthPixel(signCell.Column + i);
//                        }
//                        for (int i = 1; i < signCell.GetMergedRange().RowCount - 1; i++)
//                        {
//                            cellHeight += sheet.Cells.GetRowHeightPixel(signCell.Row + i);
//                        }
//                    }
//                    if (img.Width > cellWidht || img.Height > cellHeight)
//                    {
//                        double widthPercent = Convert.ToDouble(img.Width) / Convert.ToDouble(cellWidht);
//                        double heightPercent = Convert.ToDouble(img.Height) / Convert.ToDouble(cellHeight);
//                        double percent = Math.Max(widthPercent, heightPercent);
//                        width = Convert.ToInt32(img.Width / (percent + 0.1));
//                        height = Convert.ToInt32(img.Height / (percent + 0.1));
//                    }

//                    //设置图片显示位置
//                    sheet.Cells[signCell.Row, signCell.Column].PutValue("");//清除单元格内容
//                    int picIndex = sheet.Pictures.Add(signCell.Row, signCell.Column, streamImg);//插入到单元格中
//                    Aspose.Cells.Drawing.Picture pic = sheet.Pictures[picIndex];
//                    pic.Placement = Aspose.Cells.Drawing.PlacementType.Move;
//                    pic.IsLockAspectRatio = false;
//                    pic.Width = (width >= cellWidht ? width - 2 : width);
//                    pic.Height = (height >= cellHeight ? height - 2 : height);
//                    pic.Y = pic.Y + (cellHeight - pic.Height) / 2;//图片垂直居中
//                    pic.X = pic.X + (cellWidht - img.Width) / 1;//图片水平居中

//                }
//            }
//        }

//        /// <summary>
//        /// 根据 编码 获取模板物理路径
//        /// 
//        /// ThinkWang
//        /// 2013-8-19
//        /// </summary>
//        /// <param name="tagCode"></param>
//        /// <returns></returns>
//        public static string GetTemplatePathByTag(string tagCode)
//        {
//            string path = SGBLL.GetTempelatePahtByCode(tagCode);
//            string pathTmp = System.Web.HttpContext.Current.Server.MapPath(path);
//            if (!System.IO.File.Exists(pathTmp))
//                pathTmp = "";
//            return pathTmp;
//        }

//        /// <summary>
//        /// 根据 编码和模板名称 获取模板物理路径
//        /// 
//        /// ThinkWang
//        /// 2016-11-3
//        /// </summary>
//        /// <param name="tagCode"></param>
//        /// <returns></returns>
//        public static string GetTemplatePathByTagAndName(string tagCode, string templateName)
//        {
//            string path = SGBLL.GetTempelatePahtByCodeAndName(tagCode, templateName);
//            string pathTmp = System.Web.HttpContext.Current.Server.MapPath(path);
//            if (!System.IO.File.Exists(pathTmp))
//                pathTmp = "";
//            return pathTmp;
//        }

//        /// <summary>
//        /// 判断文件是否存在
//        /// </summary>
//        /// <param name="path"></param>
//        /// <returns></returns>
//        public static bool isEixtsPath(string path)
//        {
//            bool isEixts = !File.Exists(path);
//            return isEixts;
//        }
//        /// <summary>
//        /// 为单元格增加样式
//        /// </summary>
//        /// <param name="Row">第几行</param>
//        /// <param name="Cell">第几列</param>
//        /// <param name="style">增加的样式</param>
//        /// <param name="sheet">操作的worksheet</param>
//        public static void SetCellStyle(int Row, int Cell, Style style, Worksheet sheet)
//        {
//            sheet.Cells[Row, Cell].SetStyle(style);
//        }

//        /// <summary>
//        /// 为单元格添加内容
//        /// </summary>
//        /// <param name="Row">第几行</param>
//        /// <param name="Cell">第几列</param>
//        /// <param name="HtmlString">添加的内容</param>
//        /// <param name="sheet">操作的worksheet</param>
//        public static void SetCellHtmlString(int Row, int Cell, string HtmlString, Worksheet sheet)
//        {
//            if (HtmlString == "0")
//                HtmlString = "";
//            sheet.Cells[Row, Cell].Value = HtmlString;
//        }

//        /// <summary>
//        /// 设置单元格边框
//        /// </summary>
//        /// <param name="Row"></param>
//        /// <param name="Cell"></param>
//        /// <param name="borderstyle"></param>
//        /// <param name="sheet"></param>
//        public static void SetCellBorder(Style borderStyle)
//        {
//            borderStyle.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
//            borderStyle.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
//            borderStyle.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
//            borderStyle.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
//        }
//        /// <summary>
//        /// 计量支付受理单替换标签
//        /// </summary>
//        /// <param name="wb"></param>
//        /// <param name="id">计量id</param>
//        /// <param name="fillDT"></param>
//        /// <param name="isSign"></param>
//        /// <param name="isApproved">是否批复</param>
//        public static void FillSeizeLebels(Workbook wb, int id, DataTable fillDT, int isSign, bool isApproved, int type = 0)
//        {
//            Worksheet sheet = wb.Worksheets[0];
//            string dutyCode = string.Empty;
//            Cell signIdeaCell = null, signPicCell = null, signDateCell = null;
//            string workflowCods = "htgcs,xmjl,zdjlgcs,htjlgcs,zjlgcs,yzzjbz,yzgcbz,htbjbr,yzhtbz,yzzjl,yzdsz";
//            bool isAddWater = true;
//            switch (type)
//            {
//                case 0:
//                    workflowCods = SGBLL.GetCalDutyCode(EPM.Common.Configuration.WorkFlowSetting.Instance.WorkMeasureWFId);
//                    isAddWater = !SGBLL.IsDutyBySessionId(id, "zjlgcs");
//                    break;
//                case 1:
//                    workflowCods = SGBLL.GetCalDutyCode(EPM.Common.Configuration.WorkFlowSetting.Instance.VisionMeasureWFId);
//                    break;
//                case 3:
//                    workflowCods = SGBLL.GetCalDutyCode(EPM.Common.Configuration.WorkFlowSetting.Instance.VerifyMeasureWFId);
//                    break;
//                case 4:
//                    workflowCods = SGBLL.GetCalDutyCode(EPM.Common.Configuration.WorkFlowSetting.Instance.SupervisingWFId);
//                    break;
//            }
//            ArrayList al = new ArrayList();

//            string[] arrStr = workflowCods.Trim(',').Split(',');
//            for (int i = 0; i < arrStr.Length; i++)
//            {
//                al.Add(arrStr[i]);
//            }

//            foreach (DataRow row in fillDT.Rows)
//            {
//                dutyCode = row["岗位编码"].ToString();
//                signPicCell = sheet.Cells.FindString(dutyCode, null);

//                signIdeaCell = sheet.Cells.FindString(dutyCode + "_idea", null);
//                signDateCell = sheet.Cells.FindString(dutyCode + "_date", null);

//                FillPic(sheet, signPicCell, row["CALIDEAITEM_STAFFID"].ToString(), isSign);
//                FillIdea(sheet, signIdeaCell, row["意见"].ToString(), isSign);
//                FillDate(sheet, signDateCell, Convert.ToDateTime(row["签认时间"]), isSign);
//                //填充下个处理人的受理时间
//                signDateCell = sheet.Cells.FindString(dutyCode + "_date", signDateCell);
//                FillDate(sheet, signDateCell, Convert.ToDateTime(row["签认时间"]), isSign);
//                al.Remove(dutyCode);
//            }

//            //将未替换的标签置空
//            for (int i = 0; i < al.Count; i++)
//            {
//                dutyCode = al[i].ToString();
//                signPicCell = sheet.Cells.FindString(dutyCode, null);
//                signIdeaCell = sheet.Cells.FindString(dutyCode + "_idea", null);
//                signDateCell = sheet.Cells.FindString(dutyCode + "_date", null);

//                FillPic(sheet, signPicCell, "", 0);
//                FillIdea(sheet, signIdeaCell, "", 0);
//                FillDate(sheet, signDateCell, DateTime.Now, 0);
//                //填充下个处理人的受理时间
//                signDateCell = sheet.Cells.FindString(dutyCode + "_date", signDateCell);
//                FillDate(sheet, signDateCell, DateTime.Now, 0);
//            }

//            #region 添加水印
//            if (isAddWater)
//                InsertWater(sheet, 0, isApproved);
//            #endregion

//            #region 只读，禁止修改
//            if (!sheet.IsProtected)
//                sheet.Protect(ProtectionType.All, "kr_pay_kingroad", "");
//            #endregion
//        }
//        //填充电子签名
//        private static void FillPic(Worksheet sheet, Cell signCell, string staffID, int flag)
//        {
//            if (signCell == null)
//                return;

//            if (flag == 1)
//            {
//                DotNet.Model.MyEntity.Base_User userDetail = new UserService().GetUserEntityByID(int.Parse(staffID));
//                if (!string.IsNullOrEmpty(userDetail.SignedPassword))
//                {
//                    try
//                    {//避免没图片文件报异常
//                        //创建图像对象
//                        System.Drawing.Bitmap bm = new System.Drawing.Bitmap(System.Web.HttpContext.Current.Server.MapPath(userDetail.SignedPassword));
//                        System.IO.MemoryStream streamImg = new System.IO.MemoryStream();
//                        bm.Save(streamImg, System.Drawing.Imaging.ImageFormat.Jpeg);
//                        System.Drawing.Image img = System.Drawing.Image.FromStream(streamImg);

//                        //过大的话，等比例缩放
//                        int cellHeight = sheet.Cells.GetRowHeightPixel(signCell.Row);//单元格高度
//                        int cellWidht = sheet.Cells.GetColumnWidthPixel(signCell.Column);//单元格宽度

//                        if (signCell.IsMerged)//如果电子签名单元格合并，就取合并区域的宽度
//                        {
//                            for (int i = 0; i < signCell.GetMergedRange().ColumnCount - 1; i++)
//                                cellWidht += sheet.Cells.GetColumnWidthPixel(signCell.Column + i + 1);
//                        }
//                        if (img.Width > cellWidht || img.Height > cellHeight)
//                        {
//                            double widthPercent = img.Width * 1d / cellWidht;
//                            double heightPercent = img.Height * 1d / cellHeight;
//                            double percent = Math.Max(widthPercent, heightPercent);
//                            int width = Convert.ToInt32(img.Width / percent);
//                            int height = Convert.ToInt32(img.Height * 1d / percent);
//                            img = img.GetThumbnailImage(width, height, null, IntPtr.Zero);
//                            img.Save(streamImg, System.Drawing.Imaging.ImageFormat.Jpeg);
//                        }

//                        //设置图片显示位置
//                        sheet.Cells[signCell.Row, signCell.Column].PutValue("");//清除单元格内容
//                        int picIndex = sheet.Pictures.Add(signCell.Row, signCell.Column, streamImg);//插入到单元格中
//                        Aspose.Cells.Drawing.Picture pic = sheet.Pictures[picIndex];
//                        pic.Placement = Aspose.Cells.Drawing.PlacementType.Move;
//                        pic.IsLockAspectRatio = false;

//                        pic.Top = (cellHeight - img.Height) / 2;//图片垂直居中
//                        try
//                        {
//                            pic.Left = (cellWidht - img.Width) / 2;//图片水平居中
//                        }
//                        catch (Exception)
//                        {
//                            pic.Left = 2;
//                        }
//                        //sheet.Cells[signCell.Row, signCell.Column].PutValue(staffDetail.RealName);
//                    }
//                    catch (Exception)
//                    {

//                    }
//                }
//                else
//                    flag = 0;
//            }
//            if (flag == 0)
//                sheet.Cells[signCell.Row, signCell.Column].PutValue("");//清除单元格内容
//        }
//        //填充意见
//        private static void FillIdea(Worksheet sheet, Cell signCell, string idea, int flag)
//        {
//            if (signCell != null)
//            {
//                sheet.Cells[signCell.Row, signCell.Column].PutValue("");
//                sheet.Cells[signCell.Row, signCell.Column].PutValue(flag == 1 ? idea : "");
//            }
//        }
//        //填充日期
//        private static void FillDate(Worksheet sheet, Cell signCell, DateTime checkDate, int flag)
//        {
//            if (signCell != null)
//            {
//                sheet.Cells[signCell.Row, signCell.Column].PutValue("");
//                sheet.Cells[signCell.Row, signCell.Column].PutValue(flag == 1 ? string.Format("{0:D}", checkDate) : "");
//            }
//        }
//        /// <summary>
//        /// 根据报表模板的编码获取报表模板的 实体
//        /// </summary>
//        /// <param name="code">编码</param>
//        /// <returns>报表模板的 实体</returns>
//        public static COMMON_TEMPLETE GetTempelateByCode(string code)
//        {
//            return SGBLL.GetTempelateByCode(code);
//        }
//        /// <summary>
//        /// 插入二维码
//        /// </summary>
//        /// <param name="designer"></param>
//        public static void InsertQRCode(WorkbookDesigner designer, ReportEntity entiry)
//        {
//            string nr = KingRoad.EPM.Common.Md5.Encrypt(entiry.ProjectName + "[" + entiry.MeasurePhaseCode + "]");
//            string imgName = Guid.NewGuid().ToString();
//            string imgPath = System.Web.HttpContext.Current.Server.MapPath("~/TempleterFilePath/" + imgName + ".png");
//            try
//            {
//                if (entiry != null)
//                {
//                    QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
//                    //二维码背景颜色
//                    qrCodeEncoder.QRCodeBackgroundColor = System.Drawing.Color.White;
//                    //二维码编码方式
//                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
//                    //每个小方格的宽度
//                    qrCodeEncoder.QRCodeScale = 10;
//                    //二维码版本号
//                    qrCodeEncoder.QRCodeVersion = 5;
//                    //纠错等级
//                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
//                    Bitmap bt = qrCodeEncoder.Encode(nr, Encoding.UTF8);
//                    bt = GetThumbnail(bt, 60, 60);
//                    bt.Save(imgPath, ImageFormat.Png);
//                    FileStream inFile = new System.IO.FileStream(imgPath, System.IO.FileMode.Open,
//                        System.IO.FileAccess.Read);
//                    byte[] binaryData = new Byte[inFile.Length];


//                    long bytesRead = inFile.Read(binaryData, 0, (int)inFile.Length);
//                    Aspose.Cells.PageSetup pageSetup = null;
//                    for (int i = 0; i < designer.Workbook.Worksheets.Count; i++)
//                    {
//                        pageSetup = designer.Workbook.Worksheets[i].PageSetup;
//                        pageSetup.SetHeaderPicture(2, binaryData);
//                        pageSetup.SetHeader(1, "&G");
//                        pageSetup.SetHeader(2, "&G");
//                        pageSetup.HeaderMargin = 0.1;
//                    }
//                    inFile.Close();
//                    inFile.Dispose();
//                    bt.Dispose();
//                }
//            }
//            catch (Exception ex)
//            {
//                AddExp(ex);
//            }
//            finally
//            {

//                if (File.Exists(imgPath))
//                {
//                    File.Delete(imgPath);
//                }
//            }
//        }
//        /// <summary>
//        /// 图片压缩
//        /// </summary>
//        /// <param name="b"></param>
//        /// <param name="destHeight"></param>
//        /// <param name="destWidth"></param>
//        /// <returns></returns>
//        public static Bitmap GetThumbnail(Bitmap b, int destHeight, int destWidth)
//        {
//            System.Drawing.Image imgSource = b;
//            System.Drawing.Imaging.ImageFormat thisFormat = imgSource.RawFormat;
//            int sW = 0, sH = 0;
//            // 按比例缩放           
//            int sWidth = imgSource.Width;
//            int sHeight = imgSource.Height;
//            if (sHeight > destHeight || sWidth > destWidth)
//            {
//                if ((sWidth * destHeight) > (sHeight * destWidth))
//                {
//                    sW = destWidth;
//                    sH = (destWidth * sHeight) / sWidth;
//                }
//                else
//                {
//                    sH = destHeight;
//                    sW = (sWidth * destHeight) / sHeight;
//                }
//            }
//            else
//            {
//                sW = sWidth;
//                sH = sHeight;
//            }
//            Bitmap outBmp = new Bitmap(destWidth, destHeight);
//            Graphics g = Graphics.FromImage(outBmp);
//            g.Clear(Color.Transparent);
//            // 设置画布的描绘质量         
//            g.CompositingQuality = CompositingQuality.HighQuality;
//            g.SmoothingMode = SmoothingMode.HighQuality;
//            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
//            g.DrawImage(imgSource, new Rectangle((destWidth - sW) / 2, (destHeight - sH) / 2, sW, sH), 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);
//            g.Dispose();
//            // 以下代码为保存图片时，设置压缩质量     
//            EncoderParameters encoderParams = new EncoderParameters();
//            long[] quality = new long[1];
//            quality[0] = 100;
//            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
//            encoderParams.Param[0] = encoderParam;
//            imgSource.Dispose();
//            return outBmp;
//        }
//    }
//  /// <summary>
//  ///  水印类型
//  /// </summary>
//  public enum WaterType
//  {
//    /// <summary>
//    /// 文字水印
//    /// </summary>
//    Txt = 0,
//    /// <summary>
//    ///  图片水印
//    /// </summary>
//    Img = 1
//  }
//}