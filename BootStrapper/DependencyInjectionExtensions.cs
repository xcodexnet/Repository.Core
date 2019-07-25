using CBLibrary.Repository.Repository;
using Microsoft.Extensions.DependencyInjection;
using CBAUnitOfWork = CBLibrary.Repository.UnitOfWork;

namespace CBLibrary.Repository.BootStrapper
{
    /// <summary>
    /// 依赖注入扩展
    /// </summary>
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// 注入仓储和工作单元
        /// </summary>
        /// <param name="services"></param>
        public static void AddRepository(this IServiceCollection services)
        {
            //注入仓储
            services.AddScoped(typeof(IRepositoryWithIntId<>), typeof(RepositoryWithIntId<>));
            services.AddScoped(typeof(IRepositoryWithIntIdSoftDeletionAudited<>), typeof(RepositoryWithIntIdSoftDeletionAudited<>));
            services.AddScoped(typeof(IRepositoryWithIntIdCreationAudited<>), typeof(RepositoryWithIntIdCreationAudited<>));
            services.AddScoped(typeof(IRepositoryWithIntIdCreationModificationAudited<>), typeof(RepositoryWithIntIdCreationModificationAudited<>));
            services.AddScoped(typeof(IRepositoryWithIntIdFullAudited<>), typeof(RepositoryWithIntIdFullAudited<>));

            services.AddScoped(typeof(IRepositoryWithGuid<>), typeof(RepositoryWithGuid<>));
            services.AddScoped(typeof(IRepositoryWithGuidSoftDeletionAudited<>), typeof(RepositoryWithGuidSoftDeletionAudited<>));
            services.AddScoped(typeof(IRepositoryWithGuidCreationAudited<>), typeof(RepositoryWithGuidCreationAudited<>));
            services.AddScoped(typeof(IRepositoryWithGuidCreationModificationAudited<>), typeof(RepositoryWithGuidCreationModificationAudited<>));
            services.AddScoped(typeof(IRepositoryWithGuidFullAudited<>), typeof(RepositoryWithGuidFullAudited<>));

            //注入工作单元
            services.AddScoped<CBAUnitOfWork.IUnitOfWork, CBAUnitOfWork.UnitOfWork>();
        }
    }
}
