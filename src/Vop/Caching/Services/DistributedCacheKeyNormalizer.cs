using Microsoft.Extensions.Options;

namespace Vop.Api.Caching
{
    public class DistributedCacheKeyNormalizer : IDistributedCacheKeyNormalizer
    {
        protected DistributedCacheOptions DistributedCacheOptions { get; }

        public DistributedCacheKeyNormalizer(
            IOptions<DistributedCacheOptions> distributedCacheOptions)
        {
            DistributedCacheOptions = distributedCacheOptions.Value;
        }

        public virtual string NormalizeKey(DistributedCacheKeyNormalizeArgs args)
        {
            var normalizedKey = $"c:{args.CacheName},k:{DistributedCacheOptions.KeyPrefix}{args.Key}";

            return normalizedKey;
        }
    }
}