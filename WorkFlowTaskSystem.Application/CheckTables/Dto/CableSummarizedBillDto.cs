using System;
using System.Collections.Generic;
using System.Text;
using Abp.AutoMapper;
using WorkFlowTaskSystem.Core.Damain.Entities;

namespace WorkFlowTaskSystem.Application.CheckTables.Dto
{
  /// <summary>
  /// 电缆汇总清单
  /// 根据excel表格的列命名
  /// </summary>
  [AutoMapTo(typeof(CableSummarizedBill))]
  public class CableSummarizedBillDto
  {
    public string A { get; set; }
    public string B { get; set; }
    public string C { get; set; }
    public string D { get; set; }
    public string E { get; set; }
    public string F { get; set; }
    public string G { get; set; }
    public string H { get; set; }
    public string I { get; set; }
    public string J { get; set; }
    public string K { get; set; }
    public string L { get; set; }
    public string M { get; set; }
    public string N { get; set; }
    public string O { get; set; }
    public string P { get; set; }
    public string Q { get; set; }
    public string R { get; set; }
    public string S { get; set; }
    public string T { get; set; }
    public string U { get; set; }
    public string V { get; set; }
    public string W { get; set; }
    public string X { get; set; }
    public string Y { get; set; }
    public string Z { get; set; }
    public string AA { get; set; }
    public string AB { get; set; }
    public string AC { get; set; }
    public string AD { get; set; }
    public string AE { get; set; }
    public string AF { get; set; }
    public string AG { get; set; }
    public string AH { get; set; }
    public string AI { get; set; }
    public string AJ { get; set; }
    public string AK { get; set; }
    public string AL { get; set; }
    public string AM { get; set; }
    public string AN { get; set; }
    public string AO { get; set; }
    public string AP { get; set; }
    public string AQ { get; set; }
    public string AR { get; set; }
    public string AS { get; set; }
    public string AT { get; set; }
    public string AU { get; set; }
    public string AV { get; set; }
  }
}
