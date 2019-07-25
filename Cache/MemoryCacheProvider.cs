using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBLibrary.Repository.Cache
{
    /// <summary>
    /// 内存缓存提供者
    /// </summary>
    public class MemoryCacheProvider : ICacheService, IDisposable
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheProvider(IMemoryCache cache)
        {
            this._cache = cache;
        }


        #region 验证缓存项是否存在
        /// <summary>
        /// 验证缓存项是否存在
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            if (null == key)
            {
                throw new ArgumentNullException(nameof(key));
            }

            object cached;
            return this._cache.TryGetValue(key, out cached);
        }

        /// <summary>
        /// 验证缓存项是否存在-异步
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(string key)
        {
            return await Task.Run(() => Exists(key));
        }
        #endregion

        #region 添加缓存
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <returns></returns>
        public bool Add(string key, object value)
        {
            if (null == key)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (null == value)
            {
                throw new ArgumentNullException(nameof(value));
            }

            this._cache.Set(key, value);

            return Exists(key);
        }

        /// <summary>
        /// 添加缓存-异步
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <returns></returns>
        public async Task<bool> AddAsync(string key, object value)
        {
            return await Task.Run(() => Add(key, value));
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expiration">过期时间</param>
        /// <param name="isSliding">是否滑动过期</param>
        /// <returns></returns>
        public bool Add(string key, object value, TimeSpan expiration, bool isSliding = false)
        {
            if (null == key)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (null == value)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var memoryCacheEntryOptions = new MemoryCacheEntryOptions();
            if (true == isSliding)
            {
                this._cache.Set(key, value, memoryCacheEntryOptions.SetSlidingExpiration(expiration));
            }
            else
            {
                this._cache.Set(key, value, memoryCacheEntryOptions.SetAbsoluteExpiration(expiration));
            }

            return Exists(key);
        }

        /// <summary>
        /// 添加缓存-异步
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expiration">过期时间</param>
        /// <param name="isSliding">是否滑动过期</param>
        /// <returns></returns>
        public async Task<bool> AddAsync(string key, object value, TimeSpan expiration, bool isSliding = false)
        {
            return await Task.Run(() => Add(key, value, expiration, isSliding));
        }
        #endregion

        #region 获取缓存
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            if (null == key)
            {
                throw new ArgumentNullException(nameof(key));
            }
            return (T)this._cache.Get(key);
        }

        /// <summary>
        /// 获取缓存-异步
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string key)
        {
            return await Task.Run(() => Get<T>(key));
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="keys">缓存Key集合</param>
        /// <returns></returns>
        public IDictionary<string, T> Get<T>(IEnumerable<string> keys)
        {
            if (null == keys || keys.Count() < 1)
            {
                throw new ArgumentNullException(nameof(keys));
            }

            var dict = new Dictionary<string, T>();

            keys.ToList().ForEach(item => dict.Add(item, this._cache.Get<T>(item)));

            return dict;
        }

        /// <summary>
        /// 获取缓存-异步
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="keys">缓存Key集合</param>
        /// <returns></returns>
        public async Task<IDictionary<string, T>> GetAsync<T>(IEnumerable<string> keys)
        {
            return await Task.Run(() => Get<T>(keys));
        }
        #endregion

        #region 修改缓存
        /// <summary>
        /// 修改缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <returns></returns>
        public bool Update(string key, object value)
        {
            if (null == key)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (null == value)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (true == Exists(key))
            {
                if (false == Remove(key))
                {
                    return false;
                }
            }

            return Add(key, value);
        }

        /// <summary>
        /// 修改缓存-异步
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(string key, object value)
        {
            return await Task.Run(() => Update(key, value));
        }

        /// <summary>
        /// 修改缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expiration">过期时间</param>
        /// <param name="isSliding">是否滑动过期</param>
        /// <returns></returns>
        public bool Update(string key, object value, TimeSpan expiration, bool isSliding = false)
        {
            if (null == key)
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (null == value)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (true == Exists(key))
            {
                if (false == Remove(key))
                {
                    return false;
                }
            }

            return Add(key, value, expiration, isSliding);
        }

        /// <summary>
        /// 修改缓存-异步
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <param name="value">缓存Value</param>
        /// <param name="expiration">过期时间</param>
        /// <param name="isSliding">是否滑动过期</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(string key, object value, TimeSpan expiration, bool isSliding = false)
        {
            return await Task.Run(() => Update(key, value, expiration, isSliding));
        }
        #endregion

        #region 删除缓存
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            if (null == key)
            {
                throw new ArgumentNullException(nameof(key));
            }

            this._cache.Remove(key);

            return Exists(key) == false;
        }

        /// <summary>
        /// 删除缓存-异步
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public async Task<bool> RemoveAsync(string key)
        {
            return await Task.Run(() => Remove(key));
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="keys">缓存Key集合</param>
        /// <returns></returns>
        public void Remove(IEnumerable<string> keys)
        {
            if (null == keys || keys.Count() < 1)
            {
                throw new ArgumentNullException(nameof(keys));
            }

            keys.ToList().ForEach(item => this._cache.Remove(item));
        }

        /// <summary>
        /// 删除缓存-异步
        /// </summary>
        /// <param name="keys">缓存Key集合</param>
        /// <returns></returns>
        public async Task RemoveAsync(IEnumerable<string> keys)
        {
            await Task.Run(() => Remove(keys));
        }
        #endregion


        #region 释放资源
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (null != this._cache)
            {
                this._cache.Dispose();
            }

            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
