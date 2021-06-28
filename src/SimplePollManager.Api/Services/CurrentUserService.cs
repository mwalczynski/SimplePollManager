namespace SimplePollManager.Api.Services
{
    using Microsoft.AspNetCore.Http;
    using SimplePollManager.Application.Common.Interfaces;

    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string User => this.httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
    }
}
