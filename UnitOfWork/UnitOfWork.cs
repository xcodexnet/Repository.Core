using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CBLibrary.Repository.UnitOfWork
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// 数据库上下文
        /// </summary>
        protected DbContext DbContext { get; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWork(DbContext context)
        {
            this.DbContext = context;
        }


        /// <summary>
        /// 提交到数据库(异步)
        /// </summary>
        /// <returns></returns>
        public async Task<int> CommitAsync()
        {
            //return await DbContext.SaveChangesAsync() > 0;

            return await DbContext.SaveChangesAsync();
        }

        /// <summary>
        /// 提交到数据库
        /// </summary>
        /// <returns></returns>
        public int Commit()
        {
            //return DbContext.SaveChanges() > 0;

            return DbContext.SaveChanges();
        }

        /// <summary>
        /// 资源释放
        /// </summary>
        public void Dispose()
        {
            this.DbContext?.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}
