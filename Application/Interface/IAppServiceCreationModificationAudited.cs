using CBLibrary.Repository.Auditing;
using CBLibrary.Repository.Dto;
using CBLibrary.Repository.Entity;

namespace CBLibrary.Repository.Application
{
    /// <summary>
    /// 应用层接口+添加审计,修改审计
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TDto"></typeparam>
    public interface IAppServiceCreationModificationAudited<in TPrimaryKey, TEntity, TDto> : IAppServiceCreationAudited<TPrimaryKey, TEntity, TDto>
        where TEntity : EntityBase<TPrimaryKey>, ICreationAudited, IModificationAudited
        where TDto : DtoBase<TPrimaryKey>, ICreationAudited, IModificationAudited
    {

    }
}
