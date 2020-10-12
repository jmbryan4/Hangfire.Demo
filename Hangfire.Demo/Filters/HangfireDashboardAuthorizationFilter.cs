using Hangfire.Dashboard;

namespace Hangfire.Demo.Filters
{
    public class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            // var httpContext = context.GetHttpContext();
            return true;
        }
    }
}
