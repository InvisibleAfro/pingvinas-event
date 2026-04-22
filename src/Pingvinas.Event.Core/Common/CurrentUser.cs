using Microsoft.AspNetCore.Http;

namespace Pingvinas.Event.Core.Common
{
    public class CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public string Id => _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "oid")?.Value
            ?? throw new Exception("User not authenticated");
        public string Name => _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "name")?.Value
            ?? throw new Exception("User not authenticated");
        public string Email => _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "email")?.Value
                ?? throw new Exception("User not authenticated");
    }
}
