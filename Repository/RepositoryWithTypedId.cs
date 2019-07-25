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
    /// 仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TPrimaryKey">主键类型</typeparam>
    public class RepositoryWithTypedId<TEntity, TPrimaryKey> : IRepositoryWithTypedId<TEntity, TPrimaryKey> where TEntity : EntityBase<TPrimaryKey>
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        public DbContext Context { get; }

        /// <summary>
        /// 数据集
        /// </summary>
        protected DbSet<TEntity> DbSet { get; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="context"></param>
        public RepositoryWithTypedId(DbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        /// <summary>
        /// 查询(Tracking)
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> QueryAsTracking()
        {
            return DbSet.AsQueryable();
        }

        /// <summary>
        /// 查询(NoTracking)
        /// </summary>
        /// <returns></returns>
        public IQueryable<TEntity> QueryAsNoTracking()
        {
            return DbSet.AsNoTracking();
        }

        /// <summary>
        /// 查询(通过Lamda表达式获取实体)
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="isTracking"></param>
        /// <returns></returns>
        public async Task<TEntity> QueryAsync(Expression<Func<TEntity, bool>> predicate, bool isTracking = false)
        {
            if (true == isTracking)
            {
                return await DbSet.AsQueryable().SingleOrDefaultAsync(predicate);
            }

            return await DbSet.AsNoTracking().SingleOrDefaultAsync(predicate);
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().AnyAsync(predicate);
        }

        /// <summary>
        /// 新增 附加到DbContext上下文
        /// </summary>
        /// <param name="entity"></param>
        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        /// <summary>
        /// 批量新增 附加到DbContext上下文
        /// </summary>
        /// <param name="entities"></param>
        public void AddRange(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        /// <summary>
        /// 更新 附加到DbContext上下文
        /// </summary>
        /// <param name="entity"></param>
        public void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="entities"></param>
        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            DbSet.UpdateRange(entities);
        }

        /// <summary>
        /// 移除 附加到DbContext上下文
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        /// <summary>
        /// 批量移除 附加到DbContext上下文
        /// </summary>
        /// <param name="entities"></param>
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }


        ///// <summary>
        ///// 保存到数据库
        ///// </summary>
        ///// <returns></returns>
        //public int SaveChanges()
        //{
        //    return Context.SaveChanges();
        //}

        ///// <summary>
        ///// 保存到数据库
        ///// </summary>
        ///// <returns></returns>
        //public async Task<int> SaveChangesAsync()
        //{
        //    return await Context.SaveChangesAsync();
        //}

    }
}
