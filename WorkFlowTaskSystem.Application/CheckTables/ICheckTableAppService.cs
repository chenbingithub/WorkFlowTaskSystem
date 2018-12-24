using System.Collections.Generic;
using Abp.Application.Services;
using WorkFlowTaskSystem.Application.Forms.Dto;
using WorkFlowTaskSystem.Core.Damain.Entities;
using WorkFlowTaskSystem.Core.ViewModel;

namespace WorkFlowTaskSystem.Application.CheckTables
{
    public interface ICheckTableAppService : IApplicationService
    {
      void InsertCableLayingDetails(string numberNo, string path);
      List<CableLayingDetails> GetCableLayingDetailsListByNumberNo(string numberNo);
      void InsertCableSummarizedBill(string numberNo, string path);
      List<BridgeInstances> GetBridgeInstancesListByNumberNo(string numberNo);
      double SummationCableSectionalArea(string numberNo, string bridgeCode, string passageType);
      double SummationCableWeight(string numberNo, string bridgeCode);
        ReportView SummationCable(string numberNo, string bridgeCode);


    }
}
