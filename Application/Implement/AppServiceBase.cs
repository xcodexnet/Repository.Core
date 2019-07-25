using AutoMapper;
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
    /// 应用服务基类
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    public abstract class AppServiceBase<TService, TPrimaryKey, TEntity, TDto> : IAppServiceBase<TPrimaryKey, TEntity, TDto>
        where TEntity : EntityBase<TPrimaryKey>
        where TDto : DtoBase<TPrimaryKey>
    {
        /// <summary>
        /// ILogger
        /// </summary>
        protected readonly ILogger Logger;

        /// <summary>
        /// IMapper
        /// </summary>
        protected readonly IMapper Mapper;

        /// <summary>
        /// IHttpContextAccessor
        /// </summary>
        protected readonly IHttpContextAccessor Accessor;

        /// <summary>
        /// IRepository
        /// </summary>
        protected readonly IRepositoryWithTypedId<TEntity, TPrimaryKey> Repository;

        /// <summary>
        /// 缓存服务
        /// </summary>
        protected readonly ICacheService CacheService;

        /// <summary>
        /// IUnitOfWork
        /// </summary>
        private readonly IUnitOfWork UnitOfWork;


        /// <summary>
        /// 缓存Key
        /// </summary>
        protected abstract string Cachekey { get; set; }

        /// <summary>
        /// 是否使用缓存
        /// </summary>
        protected bool UseCache => false == Cachekey.IsNullOrWhiteSpace();


        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="mapper"></param>
        /// <param name="accessor"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="cacheService"></param>
        /// <param name="repository"></param>
        protected AppServiceBase(
            ILogger<TService> logger,
            IMapper mapper,
            IHttpContextAccessor accessor,
            IUnitOfWork unitOfWork,
            ICacheService cacheService,
            IRepositoryWithTypedId<TEntity, TPrimaryKey> repository)
        {
            this.Logger = logger;
            this.Mapper = mapper;
            this.Accessor = accessor;
            this.UnitOfWork = unitOfWork;
            this.CacheService = cacheService;
            this.Repository = repository;
        }


        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TDto>> GetAllAsync()
        {
            var entities = await this.Repository.QueryAsNoTracking().ToListAsync();

            var dtos = Mapper.Map<IEnumerable<TDto>>(entities);

            return dtos;
        }
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TDto>> GetAllFromCacheAsync()
        {
            //1.从缓存中读取
            IEnumerable<TDto> dtos = null;
            if (UseCache)
            {
                dtos = (await this.CacheService.GetAsync<IEnumerable<TDto>>(Cachekey))?.ToList();
            }

            if (null != dtos) return dtos;

            //2.若缓存中不存在,则从数据库中读取,并写入缓存
            dtos = (await GetAllAsync()).ToList();
            if (UseCache && dtos.Any())
            {
                var result = await this.CacheService.AddAsync(Cachekey, dtos);
            }

            return dtos;
        }

        /// <summary>
        /// 根据条件表达式查询
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TDto>> GetByPredicateAsync(Func<TDto, bool> predicate)
        {
            var dtos = await GetAllAsync();

            return dtos.Where(predicate).ToList();
        }
        /// <summary>
        /// 根据条件表达式查询:FromCache
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TDto>> GetByPredicateFromCacheAsync(Func<TDto, bool> predicate)
        {
            var dtos = await GetAllFromCacheAsync();

            return dtos.Where(predicate).ToList();
        }

        /// <summary>
        /// 根据主键id查询
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns></returns>
        public virtual async Task<TDto> GetByIdAsync(TPrimaryKey id)
        {
            return (await GetByPredicateAsync(p => p.Id.Equals(id)))?.SingleOrDefault();
        }
        /// <summary>
        /// 根据主键id查询:FromCache
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TDto> GetByIdFromCacheAsync(TPrimaryKey id)
        {
            return (await GetByPredicateFromCacheAsync(p => p.Id.Equals(id)))?.SingleOrDefault();
        }
        /// <summary>
        /// 根据主键id查询
        /// </summary>
        /// <param name="id">主键id</param>
        /// <param name="isTracking">是否跟踪</param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetEntityByIdAsync(TPrimaryKey id, bool isTracking)
        {
            Expression<Func<TEntity, bool>> whereLambda = d => (d.Id.Equals(id));

            return await this.Repository.QueryAsync(whereLambda, isTracking);
        }

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
        public virtual async Task<ResultMessage<IEnumerable<TDto>>> GetPaginationAsync<TOrder>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TOrder>> orderLambda,
            bool desc = true, int pageIndex = 1, int pageSize = 10)
        {
            var tuple = await GetPaginationTupleAsync(whereLambda, orderLambda, desc, pageIndex, pageSize);

            return new ResultMessage<IEnumerable<TDto>>(tuple.Item1, tuple.Item2);
        }
        /// <summary>
        /// 获取分页数据:FromCache
        /// </summary>
        /// <typeparam name="TOrder"></typeparam>
        /// <param name="whereLambda"></param>
        /// <param name="orderLambda"></param>
        /// <param name="desc"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<ResultMessage<IEnumerable<TDto>>> GetPaginationFromCacheAsync<TOrder>(Expression<Func<TDto, bool>> whereLambda, Expression<Func<TDto, TOrder>> orderLambda, bool desc = true,
            int pageIndex = 1, int pageSize = 10)
        {
            var tuple = await GetPaginationTupleFromCacheAsync(whereLambda, orderLambda, desc, pageIndex, pageSize);

            return new ResultMessage<IEnumerable<TDto>>(tuple.Item1, tuple.Item2);
        }

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
        public virtual async Task<(IEnumerable<TDto>, int)> GetPaginationTupleAsync<TOrder>(Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, TOrder>> orderLambda,
            bool desc = true, int pageIndex = 1, int pageSize = 10)
        {
            var queryResult = this.Repository.QueryAsNoTracking().Where(whereLambda);

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
        /// <param name="desc"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<(IEnumerable<TDto>, int)> GetPaginationTupleFromCacheAsync<TOrder>(Expression<Func<TDto, bool>> whereLambda, Expression<Func<TDto, TOrder>> orderLambda, bool desc = true,
            int pageIndex = 1, int pageSize = 10)
        {
            var dtosFromCache = (await GetAllFromCacheAsync()) as IQueryable<TDto>;
            var queryResult = dtosFromCache.Where(whereLambda);

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
        /// 是否存在
        /// </summary>
        /// <param name="predicate">条件表达式</param>
        /// <returns></returns>
        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.Repository.AnyAsync(predicate);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input">实体</param>
        /// <returns></returns>
        public virtual async Task<ResultMessage> AddAsync(TDto input)
        {
            var entity = Mapper.Map<TEntity>(input);

            return await AddAsync(entity);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input">实体</param>
        /// <returns></returns>
        public virtual async Task<ResultMessage> AddAsync(TEntity input)
        {
            this.Repository.Add(input);
            var result = await CommitAsync();

            if (result > 0)
            {
                if (UseCache)
                {
                    _ = RemoveCache();
                }

                return new ResultMessage(result, AppConstants.M_ADD_SUCCESS);
            }

            return new ResultMessage(AppConstants.C_FAILURE, AppConstants.M_ADD_FAILURE);
        }
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="input">实体集合</param>
        /// <returns></returns>
        public virtual async Task<ResultMessage> AddRangeAsync(IEnumerable<TDto> input)
        {
            var entities = Mapper.Map<IEnumerable<TEntity>>(input);

            return await AddRangeAsync(entities);
        }
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="input">实体集合</param>
        /// <returns></returns>
        public virtual async Task<ResultMessage> AddRangeAsync(IEnumerable<TEntity> input)
        {
            this.Repository.AddRange(input);
            var result = await CommitAsync();

            if (result > 0)
            {
                if (UseCache)
                {
                    _ = RemoveCache();
                }

                return new ResultMessage(result, AppConstants.M_ADD_SUCCESS);
            }

            return new ResultMessage(AppConstants.C_FAILURE, AppConstants.M_ADD_FAILURE);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input">实体</param>
        /// <returns></returns>
        public virtual async Task<ResultMessage> UpdateAsync(TDto input)
        {
            var entity = Mapper.Map<TEntity>(input);

            return await UpdateAsync(entity);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input">实体</param>
        /// <returns></returns>
        public virtual async Task<ResultMessage> UpdateAsync(TEntity input)
        {
            this.Repository.Update(input);
            var result = await CommitAsync();

            if (result > 0)
            {
                if (UseCache)
                {
                    _ = RemoveCache();
                }

                return new ResultMessage(result, AppConstants.M_UPDATE_SUCCESS);
            }

            return new ResultMessage(AppConstants.C_FAILURE, AppConstants.M_UPDATE_FAILURE);
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="input">实体集合</param>
        /// <returns></returns>
        public virtual async Task<ResultMessage> UpdateRangeAsync(IEnumerable<TDto> input)
        {
            var entities = Mapper.Map<IEnumerable<TEntity>>(input);

            return await UpdateRangeAsync(entities);
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="input">实体集合</param>
        /// <returns></returns>
        public virtual async Task<ResultMessage> UpdateRangeAsync(IEnumerable<TEntity> input)
        {
            this.Repository.UpdateRange(input);
            var result = await CommitAsync();

            if (result > 0)
            {
                if (UseCache)
                {
                    _ = RemoveCache();
                }

                return new ResultMessage(result, AppConstants.M_UPDATE_SUCCESS);
            }

            return new ResultMessage(AppConstants.C_FAILURE, AppConstants.M_UPDATE_FAILURE);
        }

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="input">实体</param>
        /// <returns></returns>
        public async Task<ResultMessage> DeleteAsync(TDto input)
        {
            var entity = Mapper.Map<TEntity>(input);

            return await DeleteAsync(entity);
        }
        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="input">实体</param>
        /// <returns></returns>
        public async Task<ResultMessage> DeleteAsync(TEntity input)
        {
            this.Repository.Remove(input);
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
        /// 批量物理删除
        /// </summary>
        /// <param name="input">实体集合</param>
        /// <returns></returns>
        public async Task<ResultMessage> DeleteRangeAsync(IEnumerable<TDto> input)
        {
            var entities = Mapper.Map<IEnumerable<TEntity>>(input);

            return await DeleteRangeAsync(entities);
        }
        /// <summary>
        /// 批量物理删除
        /// </summary>
        /// <param name="input">实体集合</param>
        /// <returns></returns>
        public async Task<ResultMessage> DeleteRangeAsync(IEnumerable<TEntity> input)
        {
            this.Repository.RemoveRange(input);
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
        /// 清除缓存
        /// </summary>
        /// <returns></returns>
        protected async Task RemoveCache()
        {
            await this.CacheService.RemoveAsync(Cachekey);
        }


        /// <summary>
        /// 提交到数据库(异步)
        /// </summary>
        /// <returns></returns>
        protected async Task<int> CommitAsync()
        {
            return await this.UnitOfWork.CommitAsync();
        }
    }
}


// 8900 1980     
// 6230 3060  9300