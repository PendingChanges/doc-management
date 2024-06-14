using Doc.Management;
using Doc.Management.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Journalist.Crm.Api.Infrastructure
{
    public class Context : IContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Context(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UserId UserId => new(_httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "public");
    }
}
