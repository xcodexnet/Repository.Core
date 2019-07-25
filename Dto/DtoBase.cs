using AutoMapper.Configuration;
using CBLibrary.Repository.Auditing;
using System;

namespace CBLibrary.Repository.Dto
{
    /// <summary>
    /// Dto基类
    /// </summary>
    public abstract class DtoBase
    {
        /// <summary>
        /// AutoMapper映射配置
        /// </summary>
        /// <param name="expression"></param>
        public abstract void CreateMap(MapperConfigurationExpression expression);
    }

    /// <summary>
    /// Dto泛型基类
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public abstract class DtoBase<TPrimaryKey> : DtoBase
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public virtual TPrimaryKey Id { get; set; }
    }


    #region int主键基类
    /// <summary>
    /// int主键基类
    /// </summary>
    public abstract class DtoIntId : DtoBase<int>
    {

    }

    /// <summary>
    /// int主键基类+删除审计+软删除
    /// </summary>
    public abstract class DtoIntIdWithSoftDeletionAudited : DtoIntId, ISoftDeletionAudited
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
    /// int主键基类+添加审计
    /// </summary>
    public abstract class DtoIntIdWithCreationAudited : DtoIntId, ICreationAudited
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
    public abstract class DtoIntIdWithCreationModificationAudited : DtoIntIdWithCreationAudited, IModificationAudited
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
    public abstract class DtoIntIdWithFullAudited : DtoIntIdWithCreationModificationAudited, ISoftDeletionAudited
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


    #region Guid主键基类
    /// <summary>
    /// Guid主键基类
    /// </summary>
    public abstract class DtoGuid : DtoBase<Guid>
    {

    }

    /// <summary>
    /// Guid主键基类+删除审计+软删除
    /// </summary>
    public abstract class DtoGuidWithSoftDeletionAudited : DtoGuid, ISoftDeletionAudited
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
    public abstract class DtoGuidWithCreationAudited : DtoGuid, ICreationAudited
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
    public abstract class DtoGuidWithCreationModificationAudited : DtoGuidWithCreationAudited, IModificationAudited
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
    public abstract class DtoGuidWithFullAudited : DtoGuidWithCreationModificationAudited, ISoftDeletionAudited
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

    /// <summary>
    /// 分页
    /// </summary>
    public abstract class PaginationDto
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 每页数据条数
        /// </summary>
        public int PageSize { get; set; } = 10;
    }
}
