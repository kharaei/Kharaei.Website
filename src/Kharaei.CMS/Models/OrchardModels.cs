namespace Kharaei.CMS.Models;

public class OrchardInstagramPostList
{
    public string? ContentItemId { get; set; }
    public string? Slug { get; set; }
    public string? Title { get; set; }
    public string? Media { get; set; } 
    public DateTime? CreateDate { get; set; }
}

public class OrchardInstagramPost
{
    public string? ContentItemId { get; set; }
    public string? Slug { get; set; }
    public string? Title { get; set; }
    public string? Media { get; set; } 
    public string? Text { get; set; } 
    public DateTime? CreateDate { get; set; }
}