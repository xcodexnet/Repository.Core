using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CBLibrary.Repository.Cache
{
    /// <summary>
    /// 缓存服务接口
    /// </summary>
    public interface ICacheService
    {
        #region 验证缓存项是否存在
        /// <summary>
        /// 验证缓存项是否存在
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        bool Exists(string key);

        /// <summary>
        /// 验证缓存项是否存在-异步
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        Task<bool> ExistsAsync(string key);
        #endregion

        #region 添加缓存
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <returns></returns>
        bool Add(string key, object value);

        /// <summary>
        /// 添加缓存-异步
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <returns></returns>
        Task<bool> AddAsync(string key, object value);

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key<</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expiration">缓存时间</param>
        /// <param name="isSliding">是否滑动过期(如果在过期时间内有操作,则以当前时间点延长过期时间)</param>
        /// <returns></returns>
        bool Add(string key, object value, TimeSpan expiration, bool isSliding = false);

        /// <summary>
        /// 添加缓存-异步
        /// </summary>
        /// <param name="key">缓存Key<</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expiration">缓存时间</param>
        /// <param name="isSliding">是否滑动过期(如果在过期时间内有操作,则以当前时间点延长过期时间)</param>
        /// <returns></returns>
        Task<bool> AddAsync(string key, object value, TimeSpan expiration, bool isSliding = false);
        #endregion

        #region 获取缓存
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// 获取缓存-异步
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key);

        /// <summary>
        /// 获取缓存集合
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="keys">缓存Key集合</param>
        /// <returns></returns>
        IDictionary<string, T> Get<T>(IEnumerable<string> keys);

        /// <summary>
        /// 获取缓存集合-异步
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="keys">缓存Key集合</param>
        /// <returns></returns>
        Task<IDictionary<string, T>> GetAsync<T>(IEnumerable<string> keys);
        #endregion

        #region 修改缓存
        /// <summary>
        /// 修改缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <returns></returns>
        bool Update(string key, object value);

        /// <summary>
        /// 修改缓存-异步
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(string key, object value);

        /// <summary>
        /// 修改缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expiration">缓存时长</param>
        /// <param name="isSliding">是否滑动过期(如果在过期时间内有操作,则以当前时间点延长过期时间)</param>
        /// <returns></returns>
        bool Update(string key, object value, TimeSpan expiration, bool isSliding = false);

        /// <summary>
        /// 修改缓存-异步
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expiration">缓存时长</param>
        /// <param name="isSliding">是否滑动过期(如果在过期时间内有操作,则以当前时间点延长过期时间)</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(string key, object value, TimeSpan expiration, bool isSliding = false);
        #endregion

        #region 删除缓存
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        bool Remove(string key);

        /// <summary>
        /// 删除缓存-异步
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        Task<bool> RemoveAsync(string key);

        /// <summary>
        /// 批量删除缓存
        /// </summary>
        /// <param name="keys">缓存Key集合</param>
        /// <returns></returns>
        void Remove(IEnumerable<string> keys);

        /// <summary>
        /// 批量删除缓存-异步
        /// </summary>
        /// <param name="keys">缓存Key集合</param>
        /// <returns></returns>
        Task RemoveAsync(IEnumerable<string> keys);
        #endregion
    }
}
