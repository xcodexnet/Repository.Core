using System;
using System.Threading.Tasks;

namespace CBLibrary.Repository.UnitOfWork
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// 提交到数据库(异步)
        /// </summary>
        /// <returns></returns>
        Task<int> CommitAsync();

        /// <summary>
        /// 提交到数据库
        /// </summary>
        /// <returns></returns>
        int Commit();
    }
}
