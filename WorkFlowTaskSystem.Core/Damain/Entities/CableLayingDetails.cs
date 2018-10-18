using System;
using System.Collections.Generic;
using System.Text;
using WorkFlowTaskSystem.Core.Damain.Entities.Basics;

namespace WorkFlowTaskSystem.Core.Damain.Entities
{
  /// <summary>
  /// 电缆敷设清单
  /// </summary>
   public class CableLayingDetails: BaseEntity
  {
    public CableLayingDetails()
    {
      Start=new Cable();
      End = new Cable();
    }
    /// <summary>
    /// 电缆编码
    /// </summary>
    public string CableCode { get; set; }
    /// <summary>
    /// 电缆起点
    /// </summary>
    public Cable Start { get; set; }
    /// <summary>
    /// 电缆终点
    /// </summary>
    public Cable End { get; set; }
    /// <summary>
    /// 电缆路径
    /// </summary>
    public string CablePath { get; set; }
    /// <summary>
    /// 所属安全通道
    /// </summary>
    public string SafePassage { get; set; }
    /// <summary>
    /// 压力壳贯穿件编码
    /// </summary>
    public string PressureVesselCode{ get; set; }
    /// <summary>
    /// 舱室贯穿件编码
    /// </summary>
    public string CabinCode { get; set; }

    /// <summary>
    /// 穿管编码
    /// </summary>
    public string PipeCode { get; set; }
    /// <summary>
    /// 型号
    /// </summary>
    public string Version { get; set; }
    /// <summary>
    /// 规格
    /// </summary>
    public string Specification { get; set; }
    /// <summary>
    /// 长度
    /// </summary>
    public string Length { get; set; }
    /// <summary>
    /// 护管规格（公称口径）
    /// </summary>
    public string PipeSpecification { get; set; }
    /// <summary>
    /// 护管长度
    /// </summary>
    public string PipeLength { get; set; }
    /// <summary>
    /// 其他
    /// </summary>
    public string Other { get; set; }

  }

  public class Cable
  {
    /// <summary>
    /// 房间编码
    /// </summary>
    public string RoomCode { get; set; }
    /// <summary>
    /// 系统名称
    /// </summary>
    public string SystemName { get; set; }
    /// <summary>
    /// 设备名称
    /// </summary>
    public string EquitName { get; set; }
    /// <summary>
    /// 设备编码
    /// </summary>
    public string EquitCode { get; set; }
  }
}
