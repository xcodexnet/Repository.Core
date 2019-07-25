namespace CBLibrary.Repository.Auditing
{
    /// <summary>
    /// 添加审计,修改审计,删除审计+软删除
    /// </summary>
    public interface IFullAudited : ICreationAudited, IModificationAudited, ISoftDeletionAudited
    {

    }
}
