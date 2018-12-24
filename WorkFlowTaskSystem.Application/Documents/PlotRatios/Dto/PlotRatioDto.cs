
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;
using WorkFlowTaskSystem.Core.Damain.Entities;

namespace WorkFlowTaskSystem.Application.Documents.PlotRatios.Dto
{
    [AutoMap(typeof(PlotRatio))]
    public class PlotRatioDto : EntityDto<string>
    {
        public PlotRatioDto()
        {
            Id= Guid.NewGuid().ToString("N");
        }
        [Required]
        /// <summary>
        /// 通道类型
        /// </summary>
        public string PassageType { get; set; }
        [Required]
        /// <summary>
        /// 容积率限值
        /// </summary>
        public string PlotRatioLimit { get; set; }
        public string Description { get; set; }

    }
}
