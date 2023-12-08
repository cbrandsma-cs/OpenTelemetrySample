using OpenTelemetry;
using System.Diagnostics;

namespace Sample.WebApi.Middleware;

public class TenantIdProcessor: BaseProcessor<Activity>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public TenantIdProcessor(IHttpContextAccessor httpContextAccessorAccessor)
    {
        _httpContextAccessor = httpContextAccessorAccessor;
    }


    public override void OnStart(Activity data)
    {
        var clientId = _httpContextAccessor.HttpContext?.Request.RouteValues["clientId"] as string;
        var accountId = _httpContextAccessor.HttpContext?.Request.RouteValues["accountId"] as string;

        if (!string.IsNullOrEmpty(clientId)) 
        {
            data.SetTag("ClientId", clientId);
        }

        if (!string.IsNullOrEmpty(accountId))
        {
            data.SetTag("AccountId", accountId);
        }
    }
}