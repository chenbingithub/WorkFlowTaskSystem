using Abp.Application.Services;
using Abp.Application.Services.Dto;
using WorkFlowTaskSystem.Application.Forms.Dto;

namespace WorkFlowTaskSystem.Application.Forms
{
    public interface IFormAppService : IAsyncCrudAppService<FormDto,string, PagedResultRequestDto,CreateFormDto, FormDto>
    {
    }
}
