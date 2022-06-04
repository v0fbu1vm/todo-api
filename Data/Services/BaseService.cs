using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Todo.Api.Data.Data;

namespace Todo.Api.Data.Services
{
    public abstract class BaseService : IAsyncDisposable
    {
        protected readonly DatabaseContext _dbContext;
        protected readonly IHttpContextAccessor _contextAccessor;

        protected BaseService(IDbContextFactory<DatabaseContext> dbContextFactory, IHttpContextAccessor contextAccessor)
        {
            _dbContext = dbContextFactory.CreateDbContext();
            _contextAccessor = contextAccessor;
        }

        protected string UserId()
        {
            try
            {
                if (_contextAccessor.HttpContext?.User.Identity is not ClaimsIdentity identity) return string.Empty;
                IList<Claim> claim = identity.Claims.ToList();
                return claim[0].Value;
            }
            catch
            {
                return string.Empty;
            }
        }

        public ValueTask DisposeAsync()
        {
            GC.SuppressFinalize(this);
            return _dbContext.DisposeAsync();
        }
    }
}
