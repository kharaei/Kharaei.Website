using System.Text.Json;
using Kharaei.CMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OrchardCore.Queries;

namespace Kharaei.CMS.Controllers;

[ApiController]
[Route("api/instagram")]
public class InstagramController(IOptions<SiteSettings> _siteSetting, IQueryManager _queryManager) : ControllerBase
{
    [HttpGet("layout")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InstaLayout))]
    public IActionResult Layout()
    {
        var model = new InstaLayout {
            SiteTitle = _siteSetting.Value.InstaLayoutInfo?.SiteTitle,
            Picture = _siteSetting.Value.InstaLayoutInfo?.Picture,
            Bio = _siteSetting.Value.InstaLayoutInfo?.Bio,
            Birthday = _siteSetting.Value.InstaLayoutInfo?.Birthday,
            PostCount = _siteSetting.Value.InstaLayoutInfo?.PostCount,
            SubscriptionCount = _siteSetting.Value.InstaLayoutInfo?.SubscriptionCount,
            Instagram = _siteSetting.Value.InstaLayoutInfo?.Instagram,
            Linkedin = _siteSetting.Value.InstaLayoutInfo?.Linkedin,
            Twitter = _siteSetting.Value.InstaLayoutInfo?.Twitter
        };
        return Ok(model);
    }

    [HttpGet("posts")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InstaPostList))]
    public async Task<IActionResult> Posts()
    {
        var contentQuery = await _queryManager.GetQueryAsync(Keys.QueryNames.InstaPostList);
        var contents = await _queryManager.ExecuteQueryAsync(contentQuery, new Dictionary<string, object>());
        if (contents is null)
            return NotFound();

        var records = contents.Items.Select(x => JsonSerializer.Deserialize<OrchardInstagramPostList>(x.ToString() ?? string.Empty));
        var result = new InstaPostList
        {
            RowCountPerPage = 9,
            CurrentPage = 1,
            LastPage = 1,
            Posts = records
            //.Skip(query.PageIndex.Value * query.PageSize)
            //.Take(query.PageSize)
            .Select(s => new PostItem
            {
                DateTime = s?.CreateDate,
                URL = s?.Slug,
                Title = s?.Title, 
                Image = s?.Media is not null ? $"https://www.kharaei.ir/media/{s.Media}" : null,                
            }).ToList()
        };

        return Ok(result);
    }

    [HttpGet("posts/{slug}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InstaPost))]
    public async Task<IActionResult> Get(string slug)
    {
        var contentQuery = await _queryManager.GetQueryAsync(Keys.QueryNames.InstaPost);
        if (contentQuery is null)
            throw new Exception("query not found");

        var filters = new Dictionary<string, object>
        {
            { "slug", slug }
        };
        var postDetails = await _queryManager.ExecuteQueryAsync(contentQuery, filters)
                                      ?? throw new Exception("query not found");

        var records = postDetails.Items.Select(x => JsonSerializer.Deserialize<OrchardInstagramPost>(x.ToString() ?? string.Empty));
        var result = new InstaPost
        {
            Title = records.FirstOrDefault()?.Title,
            Image = records.FirstOrDefault()?.Media is not null ? $"https://www.kharaei.ir/media/{records.FirstOrDefault()?.Media}" : null,    
            Text = records.FirstOrDefault()?.Text,
            DateTime = records.FirstOrDefault()?.CreateDate,
        };
        return Ok(result);
    }
}
