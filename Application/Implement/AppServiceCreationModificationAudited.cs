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
    /// 应用服务基类+添加审计,修改审计
    /// </summary>
    public abstract class AppServiceCreationModificationAudited<TService, TPrimaryKey, TEntity, TDto> : AppServiceCreationAudited<TService, TPrimaryKey, TEntity, TDto>, IAppServiceCreationModificationAudited<TPrimaryKey, TEntity, TDto>
        where TEntity : EntityBase<TPrimaryKey>, ICreationAudited, IModificationAudited
        where TDto : DtoBase<TPrimaryKey>, ICreationAudited, IModificationAudited
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
        protected AppServiceCreationModificationAudited(
            ILogger<TService> logger,
            IMapper mapper,
            IHttpContextAccessor accessor,
            IUnitOfWork unitOfWork,
            ICacheService cacheService,
            IRepositoryWithTypedId<TEntity, TPrimaryKey> repository) : base(logger, mapper, accessor, unitOfWork, cacheService, repository)
        {

        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input">实体</param>
        /// <returns></returns>
        public override async Task<ResultMessage> UpdateAsync(TDto input)
        {
            var entity = Mapper.Map<TEntity>(input);

            return await UpdateAsync(entity);
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="input">实体</param>
        /// <returns></returns>
        public override async Task<ResultMessage> UpdateAsync(TEntity input)
        {
            input.LastModifierUserId = Accessor.GetUserId();
            input.LastModificationTime = DateTime.Now;

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
        public override async Task<ResultMessage> UpdateRangeAsync(IEnumerable<TDto> input)
        {
            var entities = Mapper.Map<IEnumerable<TEntity>>(input);

            return await UpdateRangeAsync(entities);
        }
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="input">实体集合</param>
        public override async Task<ResultMessage> UpdateRangeAsync(IEnumerable<TEntity> input)
        {
            foreach (var item in input)
            {
                item.LastModifierUserId = Accessor.GetUserId();
                item.LastModificationTime = DateTime.Now;
            }

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
    }
}
