using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Localization;
using Abp.Runtime.Security;
using WorkFlowTaskSystem.Application.TreeNodes.Dto;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.Core.Damain.Entities;
using WorkFlowTaskSystem.Core.Damain.Repositories;
using WorkFlowTaskSystem.Core.Damain.Services.Basics;

namespace WorkFlowTaskSystem.Application.TreeNodes
{
    public class DocumentTreeNodeAppService : WorkFlowTaskSystemAppServiceBase<DocumentTreeNode, DocumentTreeNodeDto, DocumentTreeNodeDto>, IDocumentTreeNodeAppService
    {
        public DocumentTreeNodeAppService(IDocumentTreeNodeRepository repository) : base(repository)
        {
        }

        public List<AsyncIviewTree> GetNodesByParentId(string parentId)
        {
            if (Repository is IDocumentTreeNodeRepository repository)
                return repository.GetNodesByParentId(parentId).Select(MapToEntityDto)
                    .Select(u=>new AsyncIviewTree
                    {
                        Title = u.Name,
                        Id =u.Id,
                        Data = u
                    }).ToList();
            return null;
        }
        public List<DocumentTreeNodeDto> GetAllChildreNodesByParentId(string parentId)
        {
            if (Repository is IDocumentTreeNodeRepository repository)
                return repository.GetAllChildreNodesByParentId(parentId).Select(MapToEntityDto).ToList();
            return null;
        }
        public override Task<DocumentTreeNodeDto> Create(DocumentTreeNodeDto input)
        {
            //input.Id = Guid.NewGuid().ToString("N");
            return base.Create(input);
        }

        public override Task<PagedResultDto<DocumentTreeNodeDto>> GetAll(PagedAndSortedResultRequestDto input)
        {
            return base.GetAll(input);
        }
    }
}
