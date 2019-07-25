using CBLibrary.Repository.Auditing;
using System;

namespace CBLibrary.Repository.Entity
{
    /// <summary>
    /// 领域模型基类
    /// </summary>
    public abstract class EntityBase
    {

    }

    /// <summary>
    /// 领域模型基类(包含主键Id,LastModifierUser,LastModificationTime)
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public abstract class EntityBase<TPrimaryKey>
    {
        /// <summary>
        /// 主键
        /// </summary>
        public virtual TPrimaryKey Id { get; set; }
    }

    #region 领域模型int主键基类
    /// <summary>
    /// int主键基类
    /// </summary>
    public abstract class EntityIntId : EntityBase<int>
    {

    }


    /// <summary>
    /// int主键基类+删除审计+软删除
    /// </summary>
    public abstract class EntityIntIdWithSoftDeletionAudited : EntityIntId, ISoftDeletionAudited
    {
        /// <summary>
        /// 是否删除(软删除标识)
        /// </summary>
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// 删除人Id
        /// </summary>
        public virtual string DeleterUserId { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public virtual DateTime? DeletionTime { get; set; }
    }

    /// <summary>
    /// int主键基类+添加审计
    /// </summary>
    public abstract class EntityIntIdWithCreationAudited : EntityIntId, ICreationAudited
    {
        /// <summary>
        /// 创建人Id
        /// </summary>
        public virtual string CreatorUserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }
    }

    /// <summary>
    /// int主键基类+添加审计,修改审计
    /// </summary>
    public abstract class EntityIntIdWithCreationModificationAudited : EntityIntIdWithCreationAudited, IModificationAudited
    {
        /// <summary>
        /// 最后修改人Id
        /// </summary>
        public virtual string LastModifierUserId { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public virtual DateTime? LastModificationTime { get; set; }
    }

    /// <summary>
    /// int主键基类+添加审计,修改审计,删除审计+软删除
    /// </summary>
    public abstract class EntityIntIdWithFullAudited : EntityIntIdWithCreationModificationAudited, ISoftDeletionAudited
    {
        /// <summary>
        /// 是否删除(软删除标识)
        /// </summary>
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// 删除人Id
        /// </summary>
        public virtual string DeleterUserId { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public virtual DateTime? DeletionTime { get; set; }
    }
    #endregion


    #region 领域模型Guid主键基类
    /// <summary>
    /// Guid主键基类
    /// </summary>
    public abstract class EntityGuid : EntityBase<Guid>
    {

    }

    /// <summary>
    /// Guid主键基类+删除审计+软删除
    /// </summary>
    public abstract class EntityGuidWithSoftDeletionAudited : EntityGuid, ISoftDeletionAudited
    {
        /// <summary>
        /// 是否删除(软删除)
        /// </summary>
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// 删除人Id
        /// </summary>
        public virtual string DeleterUserId { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public virtual DateTime? DeletionTime { get; set; }
    }

    /// <summary>
    /// Guid主键基类+添加审计
    /// </summary>
    public abstract class EntityGuidWithCreationAudited : EntityGuid, ICreationAudited
    {
        /// <summary>
        /// 创建人Id
        /// </summary>
        public virtual string CreatorUserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }
    }

    /// <summary>
    /// Guid主键基类+添加审计,修改审计
    /// </summary>
    public abstract class EntityGuidWithCreationModificationAudited : EntityGuidWithCreationAudited, IModificationAudited
    {
        /// <summary>
        /// 最后修改人Id
        /// </summary>
        public virtual string LastModifierUserId { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public virtual DateTime? LastModificationTime { get; set; }
    }

    /// <summary>
    /// Guid主键基类+添加审计,修改审计,删除审计+软删除
    /// </summary>
    public abstract class EntityGuidWithFullAudited : EntityGuidWithCreationModificationAudited, ISoftDeletionAudited
    {
        /// <summary>
        /// 是否删除(软删除)
        /// </summary>
        public virtual bool IsDeleted { get; set; }

        /// <summary>
        /// 删除人Id
        /// </summary>
        public virtual string DeleterUserId { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public virtual DateTime? DeletionTime { get; set; }
    }
    #endregion

}
