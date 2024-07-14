using Microsoft.Extensions.Options;
using OrchardCore.ResourceManagement;

namespace Kharaei.Theme;

public class ResourceManagementOptionConfiguration: IConfigureOptions<ResourceManagementOptions>
{
    private static readonly ResourceManifest _manifest;

    static ResourceManagementOptionConfiguration()
    {
        _manifest = new ResourceManifest();

        _manifest
            .DefineStyle("OrchardTheme-styling")
            .SetUrl("~/css/main.css");
    }

    public void Configure(ResourceManagementOptions options)
    {
        options.ResourceManifests.Add(_manifest);
    }
}
