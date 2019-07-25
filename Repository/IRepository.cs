using CBLibrary.Repository.Entity;
using System;

namespace CBLibrary.Repository.Repository
{
    #region int主键基类
    /// <summary>
    /// int主键基类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IRepositoryWithIntId<TEntity> : IRepositoryWithTypedId<TEntity, int> where TEntity : EntityIntId
    {

    }

    /// <summary>
    /// int主键基类+删除审计+软删除
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IRepositoryWithIntIdSoftDeletionAudited<TEntity> : IRepositoryWithIntId<TEntity> where TEntity : EntityIntIdWithSoftDeletionAudited
    {

    }

    /// <summary>
    /// int主键基类+添加审计
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IRepositoryWithIntIdCreationAudited<TEntity> : IRepositoryWithIntId<TEntity> where TEntity : EntityIntIdWithCreationAudited
    {

    }

    /// <summary>
    /// int主键基类+添加审计,修改审计
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IRepositoryWithIntIdCreationModificationAudited<TEntity> : IRepositoryWithIntIdCreationAudited<TEntity> where TEntity : EntityIntIdWithCreationModificationAudited
    {

    }

    /// <summary>
    /// int主键基类+添加审计,修改审计,删除审计+软删除
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IRepositoryWithIntIdFullAudited<TEntity> : IRepositoryWithIntIdCreationModificationAudited<TEntity> where TEntity : EntityIntIdWithFullAudited
    {

    }
    #endregion

    #region Guid主键基类
    /// <summary>
    /// Guid主键基类
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IRepositoryWithGuid<TEntity> : IRepositoryWithTypedId<TEntity, Guid> where TEntity : EntityGuid
    {

    }

    /// <summary>
    /// Guid主键基类+删除审计+软删除
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IRepositoryWithGuidSoftDeletionAudited<TEntity> : IRepositoryWithGuid<TEntity> where TEntity : EntityGuidWithSoftDeletionAudited
    {

    }

    /// <summary>
    /// Guid主键基类+添加审计
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IRepositoryWithGuidCreationAudited<TEntity> : IRepositoryWithGuid<TEntity> where TEntity : EntityGuidWithCreationAudited
    {

    }

    /// <summary>
    /// Guid主键基类+添加审计,修改审计
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IRepositoryWithGuidCreationModificationAudited<TEntity> : IRepositoryWithGuidCreationAudited<TEntity> where TEntity : EntityGuidWithCreationModificationAudited
    {

    }


    /// <summary>
    /// Guid主键基类+添加审计,修改审计,删除审计+软删除
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IRepositoryWithGuidFullAudited<TEntity> : IRepositoryWithGuidCreationModificationAudited<TEntity> where TEntity : EntityGuidWithFullAudited
    {

    }
    #endregion
}
