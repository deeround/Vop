using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vop.Api.Caching;
using Vop.Api.DependencyInjection;
using Vop.Api.DynamicApiController;
using Vop.Api.FluentException;
using Vop.Web.Dtos;
using Vop.Web.Models;

namespace Vop.Web.Services
{
    public class CacheTestService : IDynamicApiController, ITransientDependency
    {
        private readonly IDistributedCache<BookCacheItem> _cache;

        public CacheTestService(IDistributedCache<BookCacheItem> cache)
        {
            _cache = cache;
        }

        public async Task<BookCacheItem> GetAsync(string bookId)
        {
            return await _cache.GetOrAddAsync(
                bookId.ToString(), //Cache key
                async () => await GetBookFromDatabaseAsync(bookId),
                () => new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddHours(1)
                }
            );
        }

        private async Task<BookCacheItem> GetBookFromDatabaseAsync(string bookId)
        {
            //TODO: get from database
            return new BookCacheItem()
            {
                BookId = Guid.NewGuid().ToString(),
                BookName = "测试"
            };
        }

    }
}
