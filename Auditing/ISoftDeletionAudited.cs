using System;

namespace CBLibrary.Repository.Auditing
{
    /// <summary>
    /// 软删除审计
    /// </summary>
    public interface ISoftDeletionAudited
    {
        /// <summary>
        /// 是否删除(软删除标识)
        /// </summary>
        bool IsDeleted { get; set; }

        /// <summary>
        /// 删除人Id
        /// </summary>
        string DeleterUserId { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        DateTime? DeletionTime { get; set; }
    }
}
