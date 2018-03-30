using System;
using System.Web;
using System.Web.Caching;

namespace Project.Common.Caching
{
    public class Caching : ICaching
    {
        
       const double CacheDuration = 2.0;
       public object GetCacheItem(string rawKey, string[] MasterCacheKeyArray)
        {
            return HttpRuntime.Cache[GetCacheKey(rawKey, MasterCacheKeyArray)];
        }

        public string GetCacheKey(string cacheKey, string[] MasterCacheKeyArray)
        {
            return string.Concat(MasterCacheKeyArray[0], "-", cacheKey);
        }
       public void AddCacheItem(string rawKey, object value, string[] MasterCacheKeyArray)
        {
            Cache DataCache = HttpRuntime.Cache;
            if (DataCache[MasterCacheKeyArray[0]] == null)
                DataCache[MasterCacheKeyArray[0]] = DateTime.Now;
            CacheDependency dependency =
                new CacheDependency(null, MasterCacheKeyArray);
            DataCache.Insert(GetCacheKey(rawKey, MasterCacheKeyArray), value, dependency,
                DateTime.Now.AddSeconds(CacheDuration),
             Cache.NoSlidingExpiration);
        }

        public void InvalidateCache(string[] MasterCacheKeyArray)
        {
          
            HttpRuntime.Cache.Remove(MasterCacheKeyArray[0]);
        }
    }
}
