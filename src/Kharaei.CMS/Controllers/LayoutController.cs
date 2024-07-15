using Kharaei.CMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Kharaei.CMS.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LayoutController(IOptions<SiteSettings> _siteSetting) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LayoutDto))]
    public IActionResult Get()
    {
        var model = new LayoutDto {
            SiteTitle = _siteSetting.Value.LayoutInformationSettings?.SiteTitle,
            Picture = _siteSetting.Value.LayoutInformationSettings?.Picture,
            Bio = _siteSetting.Value.LayoutInformationSettings?.Bio,
            Birthday = _siteSetting.Value.LayoutInformationSettings?.Birthday,
            PostCount = _siteSetting.Value.LayoutInformationSettings?.PostCount,
            SubscriptionCount = _siteSetting.Value.LayoutInformationSettings?.SubscriptionCount,
            Instagram = _siteSetting.Value.LayoutInformationSettings?.Instagram,
            Linkedin = _siteSetting.Value.LayoutInformationSettings?.Linkedin,
            Twitter = _siteSetting.Value.LayoutInformationSettings?.Twitter
        };
        return Ok(model);
    }
}
