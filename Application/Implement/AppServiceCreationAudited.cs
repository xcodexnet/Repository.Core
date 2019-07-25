using AutoMapper;
using CBLibrary.Repository.Auditing;
using CBLibrary.Repository.Cache;
using CBLibrary.Repository.Dto;
using CBLibrary.Repository.Entity;
using CBLibrary.Repository.Extensions;
using CBLibrary.Repository.Repository;
using CBLibrary.Repository.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CBLibrary.Repository.Application
{
    /// <summary>
    /// 应用服务基类+添加审计
    /// </summary>
    /// <typeparam name="TService"></typeparam>
    /// <typeparam name="TPrimaryKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    public abstract class AppServiceCreationAudited<TService, TPrimaryKey, TEntity, TDto> : AppServiceBase<TService, TPrimaryKey, TEntity, TDto>, IAppServiceCreationAudited<TPrimaryKey, TEntity, TDto>
        where TEntity : EntityBase<TPrimaryKey>, ICreationAudited
        where TDto : DtoBase<TPrimaryKey>, ICreationAudited
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
        protected AppServiceCreationAudited(
            ILogger<TService> logger,
            IMapper mapper,
            IHttpContextAccessor accessor,
            IUnitOfWork unitOfWork,
            ICacheService cacheService,
            IRepositoryWithTypedId<TEntity, TPrimaryKey> repository) : base(logger, mapper, accessor, unitOfWork, cacheService, repository)
        {

        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input">实体</param>
        /// <returns></returns>
        public override async Task<ResultMessage> AddAsync(TDto input)
        {
            var entity = Mapper.Map<TEntity>(input);

            return await AddAsync(entity);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input">实体</param>
        /// <returns></returns>
        public override async Task<ResultMessage> AddAsync(TEntity input)
        {
            input.CreatorUserId = Accessor.GetUserId();
            input.CreationTime = DateTime.Now;

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
        public override async Task<ResultMessage> AddRangeAsync(IEnumerable<TDto> input)
        {
            var entities = Mapper.Map<IEnumerable<TEntity>>(input);

            return await AddRangeAsync(entities);
        }
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="input">实体集合</param>
        /// <returns></returns>
        public override async Task<ResultMessage> AddRangeAsync(IEnumerable<TEntity> input)
        {
            foreach (var item in input)
            {
                item.CreatorUserId = Accessor.GetUserId();
                item.CreationTime = DateTime.Now;
            }

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
    }
}
