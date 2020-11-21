using Vop.Api.DynamicApiController;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vop.Api.Authentication;
using Vop.Api.Caching;
using Vop.Api.DependencyInjection;
using Vop.Web.Models;

namespace Vop.Web.Services
{
    public class CacheTestService : IDynamicApiController, ITransientDependency
    {
        private readonly IDistributedCache<BookCacheItem> _cache;
        private readonly IJwtTokenHandler _jwtTokenHandler;

        public CacheTestService(IDistributedCache<BookCacheItem> cache, IJwtTokenHandler jwtTokenHandler)
        {
            _cache = cache;
            _jwtTokenHandler = jwtTokenHandler;
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

        public async Task<BookCacheItem> Get2Async(string bookId, string bookName)
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

        public async Task<TokenInfo> CreateToken()
        {
            var sod = new Dictionary<string, object>();
            sod.Add("userId", "111111111");
            return _jwtTokenHandler.CreateToken(sod); ;
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
