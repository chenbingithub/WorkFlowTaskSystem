using System.Collections.Generic;
using System.Linq;

namespace WorkFlowTaskSystem.Core
{
    /// <summary>
    /// IView tree组件
    /// </summary>
   public class IviewTree
    {
        public string Id { get; set; }
        public string title { get; set; }
        public bool expand { get; set; }
        public bool @checked{get; set; }
        public bool selected { get; set; }
        public object data { get; set; }

        public List<IviewTree> children { get; set; }
        /// <summary>
        /// 由线性结构递归生成树形结构
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="all">源</param>
        /// <param name="currentId">当前id</param>
        /// <returns></returns>
        public static List<IviewTree> RecursiveQueries<T> (List<T> all, string currentId = null) where T : ITree
        {
            if (string.IsNullOrEmpty(currentId))
            {
                currentId = "-1";
            }
            List<IviewTree> datatree = all.Where(u => u.ParentId == currentId).Select(e => new IviewTree() { title = e.Name, Id = e.Id, data = e }).ToList();
            if (datatree == null || datatree.Count <= 0)
            {
                return new List<IviewTree>();
            }
            foreach (var dto in datatree)
            {
                dto.children = new List<IviewTree>();
                dto.children.AddRange(RecursiveQueries(all, dto.Id));
            }
            return datatree;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="all"></param>
        /// <param name="currentId"></param>
        /// <param name="selectIds">默认选中</param>
        /// <returns></returns>
        public static List<IviewTree> RecursiveQueries<T>(List<T> all, List<string> selectIds, string currentId = null) where T : ITree
        {
            if (string.IsNullOrEmpty(currentId))
            {
                currentId = "-1";
            }
            List<IviewTree> datatree = all.Where(u => u.ParentId == currentId).Select(e => new IviewTree() { title = e.Name, Id = e.Id,@checked = selectIds.Contains(e.Id) }).ToList();
            if (datatree == null || datatree.Count <= 0)
            {
                return new List<IviewTree>();
            }
            foreach (var dto in datatree)
            {
                dto.children = new List<IviewTree>();
                dto.children.AddRange(RecursiveQueries(all, selectIds, dto.Id));
            }
            return datatree;
        }

        public static List<IviewTree> LinearQueries<T>(List<T> all,List<string> selectIds)
            where T : ILinear
        {
            List<IviewTree> datatree = all.Select(e => new IviewTree() { title = e.Name, Id = e.Id, @checked = selectIds.Contains(e.Id) }).ToList();
            if (datatree == null || datatree.Count <= 0)
            {
                return new List<IviewTree>();
            }
            return datatree;
        }
    }
    /// <summary>
    /// 树形关系
    /// </summary>
    public interface ITree
    {
        string Id { get; set; }
        string ParentId { get; set; }
        string Name { get; set; }
    }
    /// <summary>
    /// 线性关系
    /// </summary>
    public interface ILinear
    {
        string Id { get; set; }
        string Name { get; set; }

    }
}
