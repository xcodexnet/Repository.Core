using CBLibrary.Repository.Dto;
using CBLibrary.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CBLibrary.Repository.Application
{
    /// <summary>
    /// 应用层基类接口
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    public interface IAppServiceBase<in TPrimaryKey, TEntity, TDto>
        where TEntity : EntityBase<TPrimaryKey>
        where TDto : DtoBase<TPrimaryKey>
    {
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TDto>> GetAllAsync();
        /// <summary>
        /// 获取所有数据:FromCache
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TDto>> GetAllFromCacheAsync();

        /// <summary>
        /// 根据条件表达式查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<TDto>> GetByPredicateAsync(Func<TDto, bool> predicate);
        /// <summary>
        /// 根据条件表达式查询:FromCache
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<IEnumerable<TDto>> GetByPredicateFromCacheAsync(Func<TDto, bool> predicate);

        /// <summary>
        /// 根据主键id查询
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns></returns>
        Task<TDto> GetByIdAsync(TPrimaryKey id);
        /// <summary>
        /// 根据主键id查询:FromCache
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns></returns>
        Task<TDto> GetByIdFromCacheAsync(TPrimaryKey id);
        /// <summary>
        /// 根据主键id查询
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="isTracking">是否跟踪</param>
        /// <returns></returns>
        Task<TEntity> GetEntityByIdAsync(TPrimaryKey id, bool isTracking);

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input">实体</param>
        /// <returns></returns>
        Task<ResultMessage> AddAsync(TDto input);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input">实体</param>
        /// <returns></returns>
        Task<ResultMessage> AddAsync(TEntity input);
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="input">实体集合</param>
        /// <returns></returns>
        Task<ResultMessage> AddRangeAsync(IEnumerable<TDto> input);
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="input">实体集合</param>
        /// <returns></returns>
        Task<ResultMessage> AddRangeAsync(IEnumerable<TEntity> input);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input">实体</param>
        /// <returns></returns>
        Task<ResultMessage> UpdateAsync(TDto input);
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input">实体</param>
        /// <returns></returns>
        Task<ResultMessage> UpdateAsync(TEntity input);
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="input">实体集合</param>
        /// <returns></returns>
        Task<ResultMessage> UpdateRangeAsync(IEnumerable<TDto> input);
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="input">实体集合</param>
        /// <returns></returns>
        Task<ResultMessage> UpdateRangeAsync(IEnumerable<TEntity> input);

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="input">实体</param>
        /// <returns></returns>
        Task<ResultMessage> DeleteAsync(TDto input);
        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="input">实体</param>
        /// <returns></returns>
        Task<ResultMessage> DeleteAsync(TEntity input);
        /// <summary>
        /// 批量物理删除
        /// </summary>
        /// <param name="input">实体集合</param>
        /// <returns></returns>
        Task<ResultMessage> DeleteRangeAsync(IEnumerable<TDto> input);
        /// <summary>
        /// 批量物理删除
        /// </summary>
        /// <param name="input">实体集合</param>
        /// <returns></returns>
        Task<ResultMessage> DeleteRangeAsync(IEnumerable<TEntity> input);

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <typeparam name="TOrder">排序字段</typeparam>
        /// <param name="whereLambda">条件表达式</param>
        /// <param name="orderLambda">排序表达式</param>
        /// <param name="desc">排序类型(默认:降序)</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns></returns>
        Task<ResultMessage<IEnumerable<TDto>>> GetPaginationAsync<TOrder>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TOrder>> orderLambda, bool desc = true, int pageIndex = 1, int pageSize = 10);
        /// <summary>
        /// 获取分页数据:FromCache
        /// </summary>
        /// <typeparam name="TOrder">排序字段</typeparam>
        /// <param name="whereLambda">条件表达式</param>
        /// <param name="orderLambda">排序表达式</param>
        /// <param name="desc">排序类型(默认:降序)</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns></returns>
        Task<ResultMessage<IEnumerable<TDto>>> GetPaginationFromCacheAsync<TOrder>(Expression<Func<TDto, bool>> whereLambda, Expression<Func<TDto, TOrder>> orderLambda, bool desc = true, int pageIndex = 1, int pageSize = 10);
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <typeparam name="TOrder">排序字段</typeparam>
        /// <param name="whereLambda">条件表达式</param>
        /// <param name="orderLambda">排序表达式</param>
        /// <param name="desc">排序类型(默认:降序)</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns></returns>
        Task<(IEnumerable<TDto>, int)> GetPaginationTupleAsync<TOrder>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TOrder>> orderLambda, bool desc = true, int pageIndex = 1, int pageSize = 10);
        /// <summary>
        /// 获取分页数据:FromCache
        /// </summary>
        /// <typeparam name="TOrder">排序字段</typeparam>
        /// <param name="whereLambda">条件表达式</param>
        /// <param name="orderLambda">排序表达式</param>
        /// <param name="desc">排序类型(默认:降序)</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns></returns>
        Task<(IEnumerable<TDto>, int)> GetPaginationTupleFromCacheAsync<TOrder>(Expression<Func<TDto, bool>> whereLambda, Expression<Func<TDto, TOrder>> orderLambda, bool desc = true, int pageIndex = 1, int pageSize = 10);

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
