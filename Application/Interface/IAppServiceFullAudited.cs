using CBLibrary.Repository.Auditing;
using CBLibrary.Repository.Dto;
using CBLibrary.Repository.Entity;

namespace CBLibrary.Repository.Application
{
    /// <summary>
    /// 应用层接口+添加审计,修改审计,删除审计+软删除
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    public interface IAppServiceFullAudited<in TPrimaryKey, TEntity, TDto> : IAppServiceCreationModificationAudited<TPrimaryKey, TEntity, TDto>, IAppServiceSoftDeletionAudited<TPrimaryKey, TEntity, TDto>
        where TEntity : EntityBase<TPrimaryKey>, ICreationAudited, IModificationAudited, ISoftDeletionAudited
        where TDto : DtoBase<TPrimaryKey>, ICreationAudited, IModificationAudited, ISoftDeletionAudited
    {

    }
}
