using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace Common
{
    /// <summary>
    /// 缓存管理
    /// </summary>
    public class CacheHelper
    {
        private static System.Web.Caching.Cache cache = HttpRuntime.Cache;

        /// <summary>
        /// 添加缓存，如果存在则抛出异常
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expireTime">过期时间</param>
        /// <returns></returns>
        public static void Add(string key, object value, DateTime expireTime)
        {

            cache.Remove(key);
            cache.Add(key, value, null, expireTime,
                System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Default, null);

        }
        /// <summary>
        /// 根据键值获得缓存值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static object Get(string key)
        {
            return cache[key];
        }
        /// <summary>
        /// 获取缓存(通过委托)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="fun"></param>
        /// <returns></returns>
        public static T GetCache<T>(string key, Func<T> fun)
        {
            object cacheValue = Get(key);
            if (cacheValue == null)
            {
                //数据库内获取数据
                cacheValue = fun.Invoke(); // fun();
                Add(key, cacheValue);
            }

            return (T)cacheValue;
        }
        /// <summary>
        /// 添加缓存，如果存在则抛出异常
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static void Add(string key, object value)
        {
            cache.Add(key, value, null, DateTime.MaxValue,
                System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
        }
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void Remove(string key)
        {
            cache.Remove(key);
        }
    }
}
