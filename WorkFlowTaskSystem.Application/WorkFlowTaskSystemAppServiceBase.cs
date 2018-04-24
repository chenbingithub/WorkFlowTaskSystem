using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using WorkFlowTaskSystem.Application.Forms.Dto;

namespace WorkFlowTaskSystem.Application
{

    public interface IWorkFlowTaskSystemAppServiceBase<TEntityDto, TPrimaryKey,in TGetAllInput,in TCreateInput,in TUpdateInput> : IAsyncCrudAppService<TEntityDto, TPrimaryKey,  TGetAllInput,  TCreateInput,  TUpdateInput>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
    {
    }

    public interface IWorkFlowTaskSystemAppServiceBase<TEntityDto, TCreateInput> : IWorkFlowTaskSystemAppServiceBase<TEntityDto, string, PagedResultRequestDto, TCreateInput, TEntityDto>
        where TEntityDto : IEntityDto<string>
    {
    }

    public class WorkFlowTaskSystemAppServiceBase<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput> : AsyncCrudAppService<TEntity, TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput>
       , IWorkFlowTaskSystemAppServiceBase<TEntityDto, TPrimaryKey, TGetAllInput, TCreateInput, TUpdateInput>
        where TEntity : class, IEntity<TPrimaryKey>
        where TEntityDto : IEntityDto<TPrimaryKey>
        where TUpdateInput : IEntityDto<TPrimaryKey>
    {
        public WorkFlowTaskSystemAppServiceBase(IRepository<TEntity, TPrimaryKey> repository) : base(repository)
        {
        }
        public override async Task<TEntityDto> Update(TUpdateInput input)
        {
            var entity = await Repository.GetAsync(input.Id);
            MapToEntity(input, entity);
            Repository.Update(entity);
            return MapToEntityDto(entity);
        }
        
    }
    public class WorkFlowTaskSystemAppServiceBase<TEntity, TEntityDto, TCreateInput> : WorkFlowTaskSystemAppServiceBase<TEntity, TEntityDto, string, PagedResultRequestDto, TCreateInput, TEntityDto>
        where TEntity : class, IEntity<string>
        where TEntityDto : IEntityDto<string>
    {
        public WorkFlowTaskSystemAppServiceBase(IRepository<TEntity, string> repository) : base(repository)
        {
            
        }
        
    }
}
