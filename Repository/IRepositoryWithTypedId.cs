using CBLibrary.Repository.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CBLibrary.Repository.Repository
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    public interface IRepositoryWithTypedId<TEntity, TPrimaryKey> where TEntity : EntityBase<TPrimaryKey>
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        DbContext Context { get; }

        /// <summary>
        /// 查询(Tracking)
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> QueryAsTracking();

        /// <summary>
        /// 查询(NoTracking)
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> QueryAsNoTracking();

        /// <summary>
        /// 查询(通过Lamda表达式获取实体)
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="isTracking"></param>
        /// <returns></returns>
        Task<TEntity> QueryAsync(Expression<Func<TEntity, bool>> predicate, bool isTracking = false);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Add(TEntity entity);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entities"></param>
        void AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Update(TEntity entity);

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entities"></param>
        void UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// 移除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Remove(TEntity entity);

        /// <summary>
        /// 批量移除
        /// </summary>
        /// <param name="entities"></param>
        void RemoveRange(IEnumerable<TEntity> entities);

        ///// <summary>
        ///// 保存到数据库
        ///// </summary>
        ///// <returns></returns>
        //int SaveChanges();

        ///// <summary>
        ///// 保存到数据库
        ///// </summary>
        ///// <returns></returns>
        //Task<int> SaveChangesAsync();
    }
}
