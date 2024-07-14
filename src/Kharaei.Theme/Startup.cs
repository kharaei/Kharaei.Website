using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;

namespace Kharaei.Theme;

public class Startup : StartupBase
{
    public override void Configure(IApplicationBuilder app)
    {  
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<IConfigureOptions<ResourceManagementOptions>, ResourceManagementOptionConfiguration>();
    }
}
