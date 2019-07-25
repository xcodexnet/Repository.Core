namespace CBLibrary.Repository.Cache
{
    /// <summary>
    /// 缓存前缀
    /// </summary>
    public enum ECachePrefix
    {

    }

    /// <summary>
    /// 缓存前缀扩展
    /// </summary>
    public static class ECachePrefixExtensions
    {
        /// <summary>
        /// 获取缓存key
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetKey(this ECachePrefix prefix, string key)
        {
            return $"{prefix}:{key}";
        }
    }
}
