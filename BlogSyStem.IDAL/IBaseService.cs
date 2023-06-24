using BlogSyStem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSyStem.IDAL
{
    /// <summary>
    /// IDisposable可以被销毁
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseService<T>:IDisposable where T:BaseEntity
    {
        ///这些方法可以不用异步  不用异步Task改为void  Task代表任务
        ///异步性能更快 增加异步就是自动保存 如果要不自动保存可以用bool来当开关控制

        /// <summary>
        /// 异步新增
        /// </summary>
        /// <param name="model">新增的对象</param>
        Task CreateAsync(T model,bool save=true);

        /// <summary>
        /// 异步修改
        /// </summary>
        /// <param name="model">修改的对象</param>
        Task EditAsync(T model, bool save = true);

        /// <summary>
        /// 异步根据Id删除
        /// </summary>
        /// <param name="id">删除对象的Id</param>
        Task RemoveAsync(Guid id, bool save = true);

        /// <summary>
        /// 异步根据对象删除
        /// </summary>
        /// <param name="model">删除的对象</param>
        Task RemoveAsync(T model, bool save = true);

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        Task Save();

        /// <summary>
        /// 更据Id获得对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetModelByIdAsync(Guid id);

        /// <summary>
        /// 根据主键获得对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetModelByPrimaryKey(Guid id);

        #region IQueryable
        /// <summary>
        /// 获取对象集合
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAllAsync();

        /// <summary>
        /// 获取对象集合的分页
        /// </summary>
        /// <param name="pageSize">每页多少行数据</param>
        /// <param name="pageIndex">分页的第几页</param>
        /// <returns></returns>
        IQueryable<T> GetAllByPageAsync(int pageSize = 10,int pageIndex=0);

        /// <summary>
        /// 获取对象集合的排序
        /// </summary>
        /// <param name="asc">默认升序</param>
        /// <returns></returns>
        IQueryable<T> GetAllByOrderAsync(bool asc=true);

        /// <summary>
        /// 获取对象集合的分页排序
        /// </summary>
        /// <param name="pageSize">每页多少行数据</param>
        /// <param name="pageIndex">分页的第几页</param>
        /// <param name="asc">默认升序</param>
        /// <returns></returns>
        IQueryable<T> GetAllByPageOrderAsync(int pageSize = 10, int pageIndex = 0, bool asc = true);
        #endregion

        #region List
        /// <summary>
        /// 获取对象集合
        /// </summary>
        /// <returns></returns>
        List<T> GetModelAllAsync();

        /// <summary>
        /// 获取对象集合的分页
        /// </summary>
        /// <param name="pageSize">每页多少行数据</param>
        /// <param name="pageIndex">分页的第几页</param>
        /// <returns></returns>
        List<T> GetModelAllByPageAsync(int pageSize = 10, int pageIndex = 0);

        /// <summary>
        /// 获取对象集合的排序
        /// </summary>
        /// <param name="asc">默认升序</param>
        /// <returns></returns>
        List<T> GetModelAllOrderAsync(bool asc = true);

        /// <summary>
        /// 获取对象集合的分页排序
        /// </summary>
        /// <param name="pageSize">每页多少行数据</param>
        /// <param name="pageIndex">分页的第几页</param>
        /// <param name="asc">默认升序</param>
        /// <returns></returns>
        List<T> GetModelAllByPageOrderAsync(int pageSize = 10, int pageIndex = 0, bool asc = true);
        #endregion
    }
}
