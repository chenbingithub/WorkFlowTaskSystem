using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using WorkFlowTaskSystem.Core.Damain.Entities;

namespace WorkFlowTaskSystem.Core.Damain.Repositories
{
    public interface IDocumentTreeNodeRepository : IRepository<DocumentTreeNode, string>
    {
        /// <summary>
        /// 获取parentId的节点的下一级节点集合，若parentId为空，获取所有根节点
        /// </summary>
        /// <param name="parentId">父节点id</param>
        /// <returns></returns>
         List<DocumentTreeNode> GetNodesByParentId(string parentId);
        /// <summary>
        /// 获取parentId的节点下所有的子节点
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        List<DocumentTreeNode> GetAllChildreNodesByParentId(string parentId);
        List<DocumentTreeNode> GetPage(int skip, int limit, out int count);
    }
}