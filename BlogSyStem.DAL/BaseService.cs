using BlogSyStem.IDAL;
using BlogSyStem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogSyStem.DAL
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity, new()
    {
        //14-18行为绑定上下文
        private readonly BlogContext _db;
        public BaseService(Models.BlogContext db)
        {
            _db = db;
        }

        /// async异步 主线程不会等待 请求如果拿不到结果就不执行后面的东西
        /// await等待 如果 axios用了then catch 后端就可以不用 await 这个代表等待

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">新增的对象</param>
        /// <param name="save"></param>
        /// <returns></returns>
        public async Task CreateAsync(T model, bool save = true)
        {
            _db.Set<T>().Add(model);
            if (save)
            {
                await _db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">修改的对象</param>
        /// <param name="save"></param>
        /// <returns></returns>
        public async Task EditAsync(T model, bool save = true)
        {
            //修改之前关闭校验 否则会增加判断
            _db.Configuration.ValidateOnSaveEnabled = false;
            _db.Entry(model).State = EntityState.Modified;
            if (save)
            {
                await _db.SaveChangesAsync();
                _db.Configuration.ValidateOnSaveEnabled = true;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="save"></param>
        /// <returns></returns>
        public async Task RemoveAsync(Guid id, bool save = true)
        {
            _db.Configuration.ValidateOnSaveEnabled = false;
            var t = new T() { Id = id };
            _db.Entry(t).State = EntityState.Unchanged;
            t.IsRemoved = true;
            if (save)
            {
                await _db.SaveChangesAsync();
                _db.Configuration.ValidateOnSaveEnabled = true;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <param name="save"></param>
        /// <returns></returns>
        public async Task RemoveAsync(T model, bool save = true)
        {
            await RemoveAsync(model.Id, save);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public async Task Save()
        {
            _db.Configuration.ValidateOnSaveEnabled = false;
            await _db.SaveChangesAsync();
            _db.Configuration.ValidateOnSaveEnabled = true;
        }

        /// <summary>
        /// 根据Id获取单个对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetModelByIdAsync(Guid id)
        {
            //return  _db.Set<T>().Find(id);
            return await GetAllAsync().FirstOrDefaultAsync(arg => arg.Id == id);
        }

        /// <summary>
        /// 返回没有被删除的数据（没有真的执行）
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAllAsync()
        {
            return _db.Set<T>().Where(arg => !arg.IsRemoved).AsNoTracking();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="asc"></param>
        /// <returns></returns>
        public IQueryable<T> GetAllByOrderAsync(bool asc = true)
        {
            var data = GetAllAsync();
            data = asc ? data.OrderBy(arg => arg.CreateTime) : data.OrderByDescending(arg => arg.CreateTime);
            return data;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public IQueryable<T> GetAllByPageAsync(int pageSize = 10, int pageIndex = 0)
        {
            //Take连续获取 在剩余的数据获取前十条
            return GetAllAsync().Skip(pageSize * pageIndex).Take(pageSize);
        }

        /// <summary>
        /// 分页排序
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        public IQueryable<T> GetAllByPageOrderAsync(int pageSize = 10, int pageIndex = 0, bool asc = true)
        {
            return GetAllByOrderAsync(asc).Skip(pageSize * pageIndex).Take(pageSize);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        /// <summary>
        /// 返回没有被删除的数据
        /// </summary>
        /// <returns></returns>
        public List<T> GetModelAllAsync()
        {
            return _db.Set<T>().Where(arg => !arg.IsRemoved).ToList();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="asc"></param>
        /// <returns></returns>
        public List<T> GetModelAllOrderAsync(bool asc = true)
        {
            var data = GetModelAllAsync();
            data = asc ? data.OrderBy(arg => arg.CreateTime).ToList() : data.OrderByDescending(arg => arg.CreateTime).ToList();
            return data;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public List<T> GetModelAllByPageAsync(int pageSize = 10, int pageIndex = 0)
        {
            return GetModelAllAsync().Skip(pageSize * pageIndex).Take(pageSize).ToList();
        }

        /// <summary>
        /// 分页排序
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="asc"></param>
        /// <returns></returns>
        public List<T> GetModelAllByPageOrderAsync(int pageSize = 10, int pageIndex = 0, bool asc = true)
        {
            return GetModelAllOrderAsync(asc).Skip(pageSize * pageIndex).Take(pageSize).ToList();
        }

        /// <summary>
        /// 根据主键获得对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<T> GetModelByPrimaryKey(Guid id)
        {
            return _db.Set<T>().FindAsync(id);
        }
    }
}
