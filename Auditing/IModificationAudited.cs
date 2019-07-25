using System;

namespace CBLibrary.Repository.Auditing
{
    /// <summary>
    /// 修改审计
    /// </summary>
    public interface IModificationAudited
    {
        /// <summary>
        /// 最后修改人Id
        /// </summary>
        string LastModifierUserId { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        DateTime? LastModificationTime { get; set; }
    }
}
