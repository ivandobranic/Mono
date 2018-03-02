using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Common.Caching
{
   public interface ICaching
    {
        object GetCacheItem(string rawKey, string[] MasterCacheKeyArray);
        string GetCacheKey(string cacheKey, string[] MasterCacheKeyArray);
        void AddCacheItem(string rawKey, object value, string[] MasterCacheKeyArray);
        void InvalidateCache(string[] MasterCacheKeyArray);

    }
}
