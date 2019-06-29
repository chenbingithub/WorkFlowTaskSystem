using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Aspose.Cells;
using Newtonsoft.Json.Linq;

namespace WorkFlowTaskSystem.Core.Reports
{
    public static class  ReportAppService
    {
        public static string WebRootPath { get; set; }

        public static Workbook GenerateReport(object source,string path)
        {
            WorkbookDesigner designer=new WorkbookDesigner(new Workbook(path));
            ForeachProperties(designer, JObject.FromObject(source));
            designer.Process();
            return designer.Workbook;
        }
       
        /// <summary>
        /// 遍历类所有字段
        /// </summary>
        /// <param name="designer">aspose.cell中WorkbookDesigner对象</param>
        /// <param name="jObject">Newtonsoft.Json 中 JObject.FromObject(entity)</param>
        /// <param name="className">当前jObject名称，默认 t</param>
        private static void ForeachProperties(WorkbookDesigner designer, JObject jObject, string className = "t")
        {

          WorkbookDesigner design=new WorkbookDesigner(new Workbook("/"));
            foreach (var item in jObject)
            {
                string propertyName = item.Key;
                var propertyValue = item.Value;
                if (item.Value.Type == JTokenType.String)
                {
                    designer.SetDataSource(className + "." + propertyName, propertyValue);
                }
                else if (item.Value.Type == JTokenType.Integer)
                {
                    designer.SetDataSource(className + "." + propertyName, propertyValue);
                }
                else if (item.Value.Type == JTokenType.Array)
                {
                    var arr = propertyValue as JArray;
                    designer.SetDataSource(ToDataTable(arr, propertyName));
                }
                else if (item.Value.Type == JTokenType.Object)
                {
                    ForeachProperties(designer, propertyValue as JObject, propertyName);
                }

            }

        }
        /// <summary>
        /// 将JArray转换为DataTable
        /// </summary>
        /// <param name="jArray"></param>
        /// <param name="tableName">DataTable表名称</param>
        /// <returns></returns>
        private static DataTable ToDataTable(JArray jArray, string tableName)

        {
            DataTable result = new DataTable(tableName);
            if (jArray != null && jArray.Count > 0)
            {
                result.Columns.Add("Num", typeof(string));//序号列
                foreach (var item in (JObject)jArray[0])
                {
                    result.Columns.Add(item.Key, typeof(string));
                }

                for (var index = 0; index < jArray.Count; index++)
                {
                    var t = jArray[index];
                    ArrayList tempList = new ArrayList {index + 1};//初始化时把序号值写入
                    foreach (var item in (JObject) t)
                    {
                        object obj = item.Value;
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;

        }
        public static DataTable ToDataTable<T>(List<T> source)
        {
            DataTable result = new DataTable("dt");
            if (source != null && source.Count > 0)
            {
                var list = source;
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    if (pi.PropertyType == typeof(string))
                    {
                        result.Columns.Add(pi.Name, pi.PropertyType);
                    }
                    
                    
                }

                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;

        }
        public static void test()
      {
        Aspose.Cells.Style style=new Style();
    }
    }
}
