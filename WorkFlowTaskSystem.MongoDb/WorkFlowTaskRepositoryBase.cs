using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.MongoDb;
using Abp.MongoDb.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Abp.Domain.Entities.Auditing;

namespace WorkFlowTaskSystem.MongoDb
{
    public  class WorkFlowTaskRepositoryBase<TEntity, TPrimaryKey> : MongoDbRepositoryBase<TEntity, TPrimaryKey>
         where TEntity : class, IEntity<TPrimaryKey>
    {
        public WorkFlowTaskRepositoryBase(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
        }

        public override TEntity Get(TPrimaryKey id)
        {
            var query = global::MongoDB.Driver.Builders.Query<TEntity>.EQ(e => e.Id, id);
            if (typeof(ISoftDelete).GetTypeInfo().IsAssignableFrom(typeof(TEntity))) {
                var query1 = global::MongoDB.Driver.Builders.Query<TEntity>.EQ(e => ((ISoftDelete)e).IsDeleted, false);
                query = global::MongoDB.Driver.Builders.Query.And(query, query1);
            }
            var entity = Collection.FindOne(query);
            //if (entity == null)
            //{
            //    throw new EntityNotFoundException("There is no such an entity with given primary key. Entity type: " + typeof(TEntity).FullName + ", primary key: " + id);
            //}

            return entity;
        }
        public override void Delete(TEntity entity)
        {
            Delete(entity.Id);
        }

        public override void Delete(TPrimaryKey id)
        {
            if (typeof(ISoftDelete).GetTypeInfo().IsAssignableFrom(typeof(TEntity)))
            {
                var entity = Get(id);
                if (typeof(IDeletionAudited).GetTypeInfo().IsAssignableFrom(typeof(TEntity)))
                {
                    if (entity is IDeletionAudited)
                    {
                       ( entity as IDeletionAudited).DeletionTime=DateTime.Now;
                    }
                
                }
                if (entity is ISoftDelete)
                {
                    (entity as ISoftDelete).IsDeleted = true;
                    Update(entity);
                }

            }
            else {
                var query = MongoDB.Driver.Builders.Query<TEntity>.EQ(e => e.Id, id);
                Collection.Remove(query);
            }
            
        }

      public void RealDelete(TPrimaryKey id)
      {
        var query = MongoDB.Driver.Builders.Query<TEntity>.EQ(e => e.Id, id);
        Collection.Remove(query);
      }
      public void RealDeleteAll()
      {
        Collection.RemoveAll();
      }
    public override IQueryable<TEntity> GetAll()
        {
            return ApplyFilters(Collection.AsQueryable());
            
        }
        public TEntity Get(IMongoQuery query)
        {
          return  Collection.FindOne(query);
        }
        public List<TEntity> GetList(IMongoQuery query)
        {
            return Collection.Find(query).ToList();
           
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="limit"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public List<TEntity> GetPage(int skip,int limit,out int count)
        {
            var query = MongoDB.Driver.Builders.Query.Empty;
            return GetPage(query, skip, limit, out count).ToList();
           
        }
        
        public IQueryable<TEntity> GetPage(IMongoQuery query,int skip, int limit, out int count)
        {
            query = ApplySoftDeleteFilter(query);
            count = (int)Collection.Find(query).Count();
            return Collection.Find(query).SetSkip(skip).SetLimit(limit).AsQueryable();
        }
        public override TEntity Insert(TEntity entity)
        {
            Collection.Insert(entity);
            return entity;
        }

        public void InsertBatch(IEnumerable<TEntity> list)
        {
            Collection.InsertBatch(list);
        }

      public override TEntity Update(TEntity entity)
        {
            if (typeof(IAudited).GetTypeInfo().IsAssignableFrom(typeof(TEntity)))
            {
                if (entity is IAudited)
                {
                    (entity as IAudited).LastModificationTime = DateTime.Now;
                }

            }
            Collection.Save(entity);
            return entity;
        }

        protected virtual IQueryable<TEntity> ApplyFilters(IQueryable<TEntity> query)
        {
            query = ApplyMultiTenancyFilter(query);
            query = ApplySoftDeleteFilter(query);
            return query;
        }

        protected virtual IQueryable<TEntity> ApplyMultiTenancyFilter(IQueryable<TEntity> query)
        {
            var tenantId = UnitOfWorkManager?.Current?.GetTenantId();

            if (typeof(IMustHaveTenant).GetTypeInfo().IsAssignableFrom(typeof(TEntity)))
            {
                if (UnitOfWorkManager?.Current == null || UnitOfWorkManager.Current.IsFilterEnabled(AbpDataFilters.MustHaveTenant))
                {
                    query = query.Where(e => ((IMustHaveTenant)e).TenantId == tenantId);
                }
            }

            if (typeof(IMayHaveTenant).GetTypeInfo().IsAssignableFrom(typeof(TEntity)))
            {
                if (UnitOfWorkManager?.Current == null || UnitOfWorkManager.Current.IsFilterEnabled(AbpDataFilters.MayHaveTenant))
                {
                    query = query.Where(e => ((IMayHaveTenant)e).TenantId == tenantId);
                }
            }

            return query;
        }

        private IQueryable<TEntity> ApplySoftDeleteFilter(IQueryable<TEntity> query)
        {
            if (typeof(ISoftDelete).GetTypeInfo().IsAssignableFrom(typeof(TEntity)))
            {
                if (UnitOfWorkManager?.Current == null || UnitOfWorkManager.Current.IsFilterEnabled(AbpDataFilters.SoftDelete))
                {
                    query = query.Where(e => !((ISoftDelete)e).IsDeleted);
                }
            }

            return query;
        }
        protected IMongoQuery ApplySoftDeleteFilter(IMongoQuery query)
        {
            if (typeof(ISoftDelete).GetTypeInfo().IsAssignableFrom(typeof(TEntity)))
            {
                if (UnitOfWorkManager?.Current == null || UnitOfWorkManager.Current.IsFilterEnabled(AbpDataFilters.SoftDelete))
                {
                    query = MongoDB.Driver.Builders.Query.And(query, MongoDB.Driver.Builders.Query.EQ("IsDeleted", false));
                }
            }

            return query;
        }
    }
}
