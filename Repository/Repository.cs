using CBLibrary.Repository.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace CBLibrary.Repository.Repository
{
    #region int主键仓储+添加审计,修改审计
    /// <summary>
    /// int主键仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public class RepositoryWithIntId<TEntity> : RepositoryWithTypedId<TEntity, int>, IRepositoryWithIntId<TEntity> where TEntity : EntityIntId
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="context"></param>
        public RepositoryWithIntId(DbContext context) : base(context)
        {

        }
    }

    /// <summary>
    /// int主键仓储+删除审计+软删除
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public class RepositoryWithIntIdSoftDeletionAudited<TEntity> : RepositoryWithIntId<TEntity>, IRepositoryWithIntIdSoftDeletionAudited<TEntity> where TEntity : EntityIntIdWithSoftDeletionAudited
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="context"></param>
        public RepositoryWithIntIdSoftDeletionAudited(DbContext context) : base(context)
        {

        }
    }

    /// <summary>
    /// int主键仓储+添加审计
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public class RepositoryWithIntIdCreationAudited<TEntity> : RepositoryWithIntId<TEntity>, IRepositoryWithIntIdCreationAudited<TEntity> where TEntity : EntityIntIdWithCreationAudited
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="context"></param>
        public RepositoryWithIntIdCreationAudited(DbContext context) : base(context)
        {

        }
    }

    /// <summary>
    /// int主键仓储+添加审计,修改审计
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public class RepositoryWithIntIdCreationModificationAudited<TEntity> : RepositoryWithIntIdCreationAudited<TEntity>, IRepositoryWithIntIdCreationModificationAudited<TEntity> where TEntity : EntityIntIdWithCreationModificationAudited
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="context"></param>
        public RepositoryWithIntIdCreationModificationAudited(DbContext context) : base(context)
        {

        }
    }

    /// <summary>
    /// int主键仓储+添加审计,修改审计,删除审计+软删除
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public class RepositoryWithIntIdFullAudited<TEntity> : RepositoryWithIntIdCreationModificationAudited<TEntity>, IRepositoryWithIntIdFullAudited<TEntity> where TEntity : EntityIntIdWithFullAudited
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="context"></param>
        public RepositoryWithIntIdFullAudited(DbContext context) : base(context)
        {

        }
    }
    #endregion

    #region Guid主键仓储
    /// <summary>
    /// Guid主键仓储
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public class RepositoryWithGuid<TEntity> : RepositoryWithTypedId<TEntity, Guid>, IRepositoryWithGuid<TEntity> where TEntity : EntityGuid
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="context"></param>
        public RepositoryWithGuid(DbContext context) : base(context)
        {

        }
    }

    /// <summary>
    /// Guid主键仓储+删除审计+软删除
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public class RepositoryWithGuidSoftDeletionAudited<TEntity> : RepositoryWithGuid<TEntity>, IRepositoryWithGuidSoftDeletionAudited<TEntity> where TEntity : EntityGuidWithSoftDeletionAudited
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="context"></param>
        public RepositoryWithGuidSoftDeletionAudited(DbContext context) : base(context)
        {

        }
    }

    /// <summary>
    /// Guid主键仓储+添加审计
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public class RepositoryWithGuidCreationAudited<TEntity> : RepositoryWithGuid<TEntity>, IRepositoryWithGuidCreationAudited<TEntity> where TEntity : EntityGuidWithCreationAudited
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="context"></param>
        public RepositoryWithGuidCreationAudited(DbContext context) : base(context)
        {

        }
    }

    /// <summary>
    /// Guid主键仓储+添加审计,修改审计
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public class RepositoryWithGuidCreationModificationAudited<TEntity> : RepositoryWithGuidCreationAudited<TEntity>, IRepositoryWithGuidCreationModificationAudited<TEntity> where TEntity : EntityGuidWithCreationModificationAudited
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="context"></param>
        public RepositoryWithGuidCreationModificationAudited(DbContext context) : base(context)
        {

        }
    }

    /// <summary>
    /// Guid主键仓储+添加审计,修改审计,删除审计+软删除
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public class RepositoryWithGuidFullAudited<TEntity> : RepositoryWithGuidCreationModificationAudited<TEntity>, IRepositoryWithGuidFullAudited<TEntity> where TEntity : EntityGuidWithFullAudited
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="context"></param>
        public RepositoryWithGuidFullAudited(DbContext context) : base(context)
        {

        }
    }
    #endregion
}
