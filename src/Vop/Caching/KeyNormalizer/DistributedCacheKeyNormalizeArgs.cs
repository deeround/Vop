namespace Vop.Api.Caching
{
    public class DistributedCacheKeyNormalizeArgs
    {
        public string Key { get; }

        public string CacheName { get; }
        
        public DistributedCacheKeyNormalizeArgs(
            string key, 
            string cacheName)
        {
            Key = key;
            CacheName = cacheName;
        }
    }
}