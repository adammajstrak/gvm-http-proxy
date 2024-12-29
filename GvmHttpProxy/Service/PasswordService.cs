using GvmHttpProxy.Models;
using Microsoft.Extensions.Caching.Memory;

namespace GvmHttpProxy.Service
{
    public class PasswordService
    {
        private readonly IConfiguration _configuration;

        public PasswordService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        MemoryCache cache = new MemoryCache(new MemoryCacheOptions());

        public void AddCredentials(string token, Credentials credentials)
        {
            cache.Set(
                token, 
                credentials, 
                TimeSpan.FromMinutes(Convert.ToInt64(_configuration["Authorization:TokenExpirationInMinutes"]!)));
        }

        public Credentials GetCredentials(string token)
        {
            Credentials? credentials;
            if(cache.TryGetValue(token, out credentials))
            {
                return credentials!;
            }

            throw new UnauthorizedAccessException("Token expired");
        }
    }
}
