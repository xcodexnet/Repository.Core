using System;

namespace CBLibrary.Repository.Auditing
{
    /// <summary>
    /// 创建审计
    /// </summary>
    public interface ICreationAudited
    {
        /// <summary>
        /// 创建人Id
        /// </summary>
        string CreatorUserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        DateTime CreationTime { get; set; }
    }
}
