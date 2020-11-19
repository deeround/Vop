using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;
using Vop.Api.Caching;
using Vop.Api.DependencyInjection;
using Vop.Api.DynamicApiController;
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

        [Authorize]
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

        public async Task<BookCacheItem> Get2Async(string bookId)
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

        public async Task<string> CreateToken()
        {
            //{"sub": "1234567890", "name": "John Doe", "iat": 1516239022}
            return Api.Authentication.JWTEncryption.Encrypt("U2FsdGVkX1+6H3D8Q//yQMhInzTdRZI9DbUGetbyaag=", new System.Collections.Generic.Dictionary<string, object>()
            {
                { "sub","1234567890" },
                { "name","John Doe" },
                { "iat",1516239022 },
            });
        }






        private async Task<BookCacheItem> GetBookFromDatabaseAsync(string bookId)
        {
            //TODO: get from database
            return new BookCacheItem()
            {
                BookId = bookId,
                BookName = "测试"
            };
        }

    }
}
