using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Extensions;
using Abp.MongoDb;
using Abp.MongoDb.Repositories;
using MongoDB.Bson;
using WorkFlowTaskSystem.Core;
using WorkFlowTaskSystem.Core.Damain.Entities;
using WorkFlowTaskSystem.Core.Damain.Repositories;

namespace WorkFlowTaskSystem.MongoDb.Repositories
{
   public class DocumentTreeNodeRepository : WorkFlowTaskRepositoryBase<DocumentTreeNode, string>,IDocumentTreeNodeRepository
    {
        public DocumentTreeNodeRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            
        }

        public List<DocumentTreeNode> GetNodesByParentId(string parentId)
        {
            var regtext = parentId.IsNullOrEmpty() ? @"/^$/" : $@"/,{parentId},$/";
            var query = MongoDB.Driver.Builders.Query<DocumentTreeNode>.Matches(t => t.Path, regtext);
            query = ApplySoftDeleteFilter(query);
           return Collection.Find(query).ToList();
        }

        public List<DocumentTreeNode> GetAllChildreNodesByParentId(string parentId)
        {
            var regtext =$@"/,{parentId},/";
            var query = MongoDB.Driver.Builders.Query<DocumentTreeNode>.Matches(t => t.Path, regtext);
            query = ApplySoftDeleteFilter(query);
            return Collection.Find(query).ToList();
        }
    }
}
