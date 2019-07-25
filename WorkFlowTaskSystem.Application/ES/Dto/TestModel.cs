using System;
using System.Collections.Generic;
using System.Text;
using Nest;

namespace WorkFlowTaskSystem.Application.ES.Dto
{
    //[ElasticsearchType(Name = “文档的类型”,IdProperty = “文档的唯一键字段名”)]
    //[Number(NumberType.Long,Name = “Id”)] 数字类型 +名称
    //[Keyword(Name = “Name”, Index = true)] 不需要分词的字符串，name=名称,index=是否建立索引
    //[Text(Name = “Dic”, Index = true, Analyzer = “ik_max_word”)] 需要分词的字符串，name=名称,index=是否建立索引,Analyzer=分词器
    ///
    /// <summary>
    /// 5.x 特性
    /// 
    /// </summary>
    [ElasticsearchType(Name = "testmodel", IdProperty = "Id")]
    public class TestModel
    {
        [Number(NumberType.Long, Name = "Id")]
        public long Id { get; set; }
        /// <summary>
        /// keyword 不分词
        /// </summary>
        [Keyword(Name = "Name", Index = true)]
        public string Name { get; set; }
        /// <summary>
        /// text 分词,Analyzer = "ik_max_word"
        /// </summary>
        [Text(Name = "Dic", Index = true)]
        public string Dic { get; set; }

        [Number(NumberType.Integer, Name = "State")]
        public int State { get; set; }

        [Boolean(Name = "Deleted")]
        public bool Deleted { get; set; }
        [Date(Name = "AddTime")]
        public DateTime AddTime { get; set; }

        [Number(NumberType.Float, Name = "PassingRate")]
        public float PassingRate { get; set; }

        [Number(NumberType.Double, Name = "Dvalue")]
        public double Dvalue { get; set; }
    }
}
