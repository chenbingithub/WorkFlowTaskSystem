using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using WorkFlowTaskSystem.Core.Damain.Entities;

namespace WorkFlowTaskSystem.Application.TreeNodes.Dto
{
    [AutoMap(typeof(DocumentTreeNode))]
    public class DocumentTreeNodeDto : EntityDto<string>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Path { get; set; }
        public string PathName { get; set; }
        public bool IsLeaf { get; set; }
        public int DateOrderby { get; set; }
        public DefineType DataDefine { get; set; }

        public Urls Url { get; set; }
        public string Description { get; set; }

    }
}
