using CBLibrary.Repository.Auditing;
using CBLibrary.Repository.Dto;
using CBLibrary.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CBLibrary.Repository.Application
{
    /// <summary>
    /// 应用层接口+删除审计+软删除
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    public interface IAppServiceSoftDeletionAudited<in TPrimaryKey, TEntity, TDto> : IAppServiceBase<TPrimaryKey, TEntity, TDto>
        where TEntity : EntityBase<TPrimaryKey>, ISoftDeletionAudited
        where TDto : DtoBase<TPrimaryKey>, ISoftDeletionAudited
    {
        /// <summary>
        /// 根据条件表达式查询
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <param name="includeDeleted">是否包含已(软)删除数据(默认:包含)</param>
        /// <returns></returns>
        Task<IEnumerable<TDto>> GetByPredicateAsync(Func<TDto, bool> predicate, bool includeDeleted = true);
        /// <summary>
        /// 根据条件表达式查询:FromCache
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <param name="includeDeleted">是否包含已(软)删除数据(默认:包含)</param>
        /// <returns></returns>
        Task<IEnumerable<TDto>> GetByPredicateFromCacheAsync(Func<TDto, bool> predicate, bool includeDeleted = true);

        /// <summary>
        /// 根据主键id查询
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="includeDeleted">是否包含已(软)删除数据(默认:包含)</param>
        /// <returns></returns>
        Task<TDto> GetByIdAsync(TPrimaryKey id, bool includeDeleted = true);
        /// <summary>
        /// 根据主键id查询:FromCache
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="includeDeleted">是否包含已(软)删除数据(默认:包含)</param>
        /// <returns></returns>
        Task<TDto> GetByIdFromCacheAsync(TPrimaryKey id, bool includeDeleted = true);
        /// <summary>
        /// 根据主键id查询
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="isTracking">是否跟踪</param>
        /// <param name="includeDeleted">是否包含已(软)删除数据(默认:包含)</param>
        /// <returns></returns>
        Task<TEntity> GetEntityByIdAsync(TPrimaryKey id, bool isTracking, bool includeDeleted = true);

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <typeparam name="TOrder">排序字段</typeparam>
        /// <param name="whereLambda">条件表达式</param>
        /// <param name="orderLambda">排序表达式</param>
        /// <param name="includeDeleted">是否包含已(软)删除数据(默认:不包含)</param>
        /// <param name="desc">排序类型(默认:降序)</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns></returns>
        Task<ResultMessage<IEnumerable<TDto>>> GetPaginationAsync<TOrder>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TOrder>> orderLambda, bool includeDeleted = false, bool desc = true, int pageIndex = 1, int pageSize = 10);
        /// <summary>
        /// 获取分页数据:FromCache
        /// </summary>
        /// <typeparam name="TOrder">排序字段</typeparam>
        /// <param name="whereLambda">条件表达式</param>
        /// <param name="orderLambda">排序表达式</param>
        /// <param name="includeDeleted">是否包含已(软)删除数据(默认:不包含)</param>
        /// <param name="desc">排序类型(默认:降序)</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        Task<ResultMessage<IEnumerable<TDto>>> GetPaginationFromCacheAsync<TOrder>(Expression<Func<TDto, bool>> whereLambda, Expression<Func<TDto, TOrder>> orderLambda, bool includeDeleted = false, bool desc = true, int pageIndex = 1, int pageSize = 10);
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <typeparam name="TOrder">排序字段</typeparam>
        /// <param name="whereLambda">条件表达式</param>
        /// <param name="orderLambda">排序表达式</param>
        /// <param name="includeDeleted">是否包含已(软)删除数据(默认:不包含)</param>
        /// <param name="desc">排序类型(默认:降序)</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns></returns>
        Task<(IEnumerable<TDto>, int)> GetPaginationTupleAsync<TOrder>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TOrder>> orderLambda, bool includeDeleted = false, bool desc = true, int pageIndex = 1, int pageSize = 10);
        /// <summary>
        /// 获取分页数据:FromCache
        /// </summary>
        /// <typeparam name="TOrder">排序字段</typeparam>
        /// <param name="whereLambda">条件表达式</param>
        /// <param name="orderLambda">排序表达式</param>
        /// <param name="includeDeleted">是否包含已(软)删除数据(默认:不包含)</param>
        /// <param name="desc">排序类型(默认:降序)</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        Task<(IEnumerable<TDto>, int)> GetPaginationTupleFromCacheAsync<TOrder>(Expression<Func<TDto, bool>> whereLambda, Expression<Func<TDto, TOrder>> orderLambda, bool includeDeleted = false, bool desc = true, int pageIndex = 1, int pageSize = 10);

        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="input">实体</param>
        /// <returns></returns>
        Task<ResultMessage> SoftDeleteAsync(TDto input);
        /// <summary>
        /// 软删除
        /// </summary>
        /// <param name="input">实体</param>
        /// <returns></returns>
        Task<ResultMessage> SoftDeleteAsync(TEntity input);
        /// <summary>
        /// 批量软删除
        /// </summary>
        /// <param name="input">实体集合</param>
        /// <returns></returns>
        Task<ResultMessage> SoftDeleteRangeAsync(IEnumerable<TDto> input);
        /// <summary>
        /// 批量软删除
        /// </summary>
        /// <param name="input">实体集合</param>
        /// <returns></returns>
        Task<ResultMessage> SoftDeleteRangeAsync(IEnumerable<TEntity> input);
    }
}
