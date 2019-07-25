using AutoMapper;
using CBLibrary.Repository.Auditing;
using CBLibrary.Repository.Cache;
using CBLibrary.Repository.Dto;
using CBLibrary.Repository.Entity;
using CBLibrary.Repository.Extensions;
using CBLibrary.Repository.Repository;
using CBLibrary.Repository.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CBLibrary.Repository.Application
{
    /// <summary>
    /// 应用服务基类+删除审计+软删除
    /// </summary>
    public abstract class AppServiceSoftDeletionAudited<TService, TPrimaryKey, TEntity, TDto> : AppServiceBase<TService, TPrimaryKey, TEntity, TDto>, IAppServiceSoftDeletionAudited<TPrimaryKey, TEntity, TDto>
        where TEntity : EntityBase<TPrimaryKey>, ISoftDeletionAudited
        where TDto : DtoBase<TPrimaryKey>, ISoftDeletionAudited
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="accessor"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="cacheService"></param>
        /// <param name="repository"></param>
        protected AppServiceSoftDeletionAudited(
            ILogger<TService> logger,
            IMapper mapper,
            IHttpContextAccessor accessor,
            IUnitOfWork unitOfWork,
            ICacheService cacheService,
            IRepositoryWithTypedId<TEntity, TPrimaryKey> repository) : base(logger, mapper, accessor, unitOfWork, cacheService, repository)
        {

        }


        /// <summary>
        /// 根据条件表达式查询
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <param name="includeDeleted">是否包含已(软)删除数据(默认:包含)</param>
        /// <returns></returns>
        public async Task<IEnumerable<TDto>> GetByPredicateAsync(Func<TDto, bool> predicate, bool includeDeleted = true)
        {
            var dtos = await GetAllAsync();

            if (false == includeDeleted)
            {
                dtos = dtos.Where(p => p.IsDeleted == false);
            }

            return dtos.Where(predicate);
        }

        /// <summary>
        /// 根据条件表达式查询:FromCache
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeDeleted"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TDto>> GetByPredicateFromCacheAsync(Func<TDto, bool> predicate, bool includeDeleted = true)
        {
            var dtos = await GetAllAsync();

            if (false == includeDeleted)
            {
                dtos = dtos.Where(p => p.IsDeleted == false);
            }

            return dtos.Where(predicate);
        }

        /// <summary>
        /// 根据主键id查询
        /// </summary>
        /// <param name="id">主键id</param>
        /// <param name="includeDeleted">是否包含已(软)删除数据(默认:包含)</param>
        /// <returns></returns>
        public async Task<TDto> GetByIdAsync(TPrimaryKey id, bool includeDeleted = true)
        {
            return (await GetByPredicateAsync(p => p.Id.Equals(id), includeDeleted))?.SingleOrDefault();
        }

        /// <summary>
        /// 根据主键id查询:FromCache
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includeDeleted"></param>
        /// <returns></returns>
        public async Task<TDto> GetByIdFromCacheAsync(TPrimaryKey id, bool includeDeleted = true)
        {
            return (await GetByPredicateFromCacheAsync(p => p.Id.Equals(id), includeDeleted))?.SingleOrDefault();
        }

        /// <summary>
        /// 根据主键id查询
        /// </summary>
        /// <param name="id">主键id</param>
        /// <param name="isTracking">是否跟踪</param>
        /// <param name="includeDeleted">是否包含已(软)删除数据(默认:包含)</param>
        /// <returns></returns>
        public async Task<TEntity> GetEntityByIdAsync(TPrimaryKey id, bool isTracking, bool includeDeleted = true)
        {
            Expression<Func<TEntity, bool>> whereLambda;
            if (false == includeDeleted)
            {
                whereLambda = d => (d.Id.Equals(id)) && (d.IsDeleted == false);
            }
            else
            {
                whereLambda = d => (d.Id.Equals(id));
            }

            return await this.Repository.QueryAsync(whereLambda, isTracking);
        }

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
        public async Task<ResultMessage<IEnumerable<TDto>>> GetPaginationAsync<TOrder>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TOrder>> orderLambda, bool includeDeleted = false,
            bool desc = true, int pageIndex = 1, int pageSize = 10)
        {
            var tuple = await GetPaginationTupleAsync(whereLambda, orderLambda, includeDeleted, desc, pageIndex, pageSize);

            return new ResultMessage<IEnumerable<TDto>>(tuple.Item1, tuple.Item2);
        }

        /// <summary>
        /// 获取分页数据:FromCache
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="whereLambda"></param>
        /// <param name="orderLambda"></param>
        /// <param name="includeDeleted"></param>
        /// <param name="desc"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<ResultMessage<IEnumerable<TDto>>> GetPaginationFromCacheAsync<TOrder>(Expression<Func<TDto, bool>> whereLambda, Expression<Func<TDto, TOrder>> orderLambda, bool includeDeleted = false,
            bool desc = true, int pageIndex = 1, int pageSize = 10)
        {
            var tuple = await GetPaginationTupleFromCacheAsync(whereLambda, orderLambda, includeDeleted, desc, pageIndex, pageSize);

            return new ResultMessage<IEnumerable<TDto>>(tuple.Item1, tuple.Item2);
        }

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
        public async Task<(IEnumerable<TDto>, int)> GetPaginationTupleAsync<TOrder>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TOrder>> orderLambda, bool includeDeleted = false,
            bool desc = true, int pageIndex = 1, int pageSize = 10)
        {
            var queryResult = this.Repository.QueryAsNoTracking().Where(whereLambda);

            if (false == includeDeleted)
            {
                queryResult = queryResult.Where(p => p.IsDeleted == false);
            }

            var totalCount = await queryResult.CountAsync();

            IQueryable<TEntity> data = null;
            if (null != orderLambda)
            {
                data = true == desc ? queryResult.OrderByDescending(orderLambda) : queryResult.OrderBy(orderLambda);
            }

            var entities = await data.Skip((pageIndex - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToListAsync();

            IEnumerable<TDto> dtos = null;
            if (null != entities)
            {
                dtos = Mapper.Map<IEnumerable<TDto>>(entities);
            }

            return (dtos, totalCount);
        }

        /// <summary>
        /// 获取分页数据:FromCache
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="whereLambda"></param>
        /// <param name="orderLambda"></param>
        /// <param name="includeDeleted"></param>
        /// <param name="desc"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<(IEnumerable<TDto>, int)> GetPaginationTupleFromCacheAsync<TOrder>(Expression<Func<TDto, bool>> whereLambda, Expression<Func<TDto, TOrder>> orderLambda,
            bool includeDeleted = false, bool desc = true, int pageIndex = 1, int pageSize = 10)
        {
            var dtosFromCache = (await GetAllFromCacheAsync()) as IQueryable<TDto>;
            var queryResult = dtosFromCache.Where(whereLambda);

            if (false == includeDeleted)
            {
                queryResult = queryResult.Where(p => p.IsDeleted == false);
            }

            var totalCount = await queryResult.CountAsync();

            IQueryable<TDto> data = null;
            if (null != orderLambda)
            {
                data = true == desc ? queryResult.OrderByDescending(orderLambda) : queryResult.OrderBy(orderLambda);
            }

            var dtos = await data.Skip((pageIndex - 1) * pageSize)
                                 .Take(pageSize)
                                 .ToListAsync();

            return (dtos, totalCount);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="input">实体</param>
        /// <returns></returns>
        public async Task<ResultMessage> SoftDeleteAsync(TDto input)
        {
            var entity = Mapper.Map<TEntity>(input);

            return await SoftDeleteAsync(entity);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="input">实体</param>
        /// <returns></returns>
        public async Task<ResultMessage> SoftDeleteAsync(TEntity input)
        {
            input.IsDeleted = true;
            input.DeleterUserId = Accessor.GetUserId();
            input.DeletionTime = DateTime.Now;

            this.Repository.Update(input);
            var result = await CommitAsync();

            if (result > 0)
            {
                if (UseCache)
                {
                    _ = RemoveCache();
                }

                return new ResultMessage(result, AppConstants.M_DELETE_SUCCESS);
            }

            return new ResultMessage(AppConstants.C_FAILURE, AppConstants.M_DELETE_FAILURE);
        }

        /// <summary>
        /// 批量逻辑删除
        /// </summary>
        /// <param name="input">实体集合</param>
        /// <returns></returns>
        public async Task<ResultMessage> SoftDeleteRangeAsync(IEnumerable<TDto> input)
        {
            var entities = Mapper.Map<IEnumerable<TEntity>>(input);

            return await SoftDeleteRangeAsync(entities);
        }

        /// <summary>
        /// 批量逻辑删除
        /// </summary>
        /// <param name="input">实体集合</param>
        /// <returns></returns>
        public async Task<ResultMessage> SoftDeleteRangeAsync(IEnumerable<TEntity> input)
        {
            foreach (var item in input)
            {
                item.IsDeleted = true;
                item.DeleterUserId = Accessor.GetUserId();
                item.DeletionTime = DateTime.Now;
            }

            this.Repository.UpdateRange(input);
            var result = await CommitAsync();

            if (result > 0)
            {
                if (UseCache)
                {
                    _ = RemoveCache();
                }

                return new ResultMessage(result, AppConstants.M_DELETE_SUCCESS);
            }

            return new ResultMessage(AppConstants.C_FAILURE, AppConstants.M_DELETE_FAILURE);
        }

    }
}
