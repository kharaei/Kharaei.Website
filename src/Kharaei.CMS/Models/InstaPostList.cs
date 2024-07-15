namespace Kharaei.CMS.Models;

public class InstaPostList
{
    public int CurrentPage { get; set; } 
    public int LastPage { get; set; }
    public int RowCountPerPage { get; set; }
    public List<PostItem> Posts { get; set; } = new List<PostItem>();
}

public class PostItem
{
    public string? Date { get; set; }        
    public string? Title { get; set; }
    public string? Image { get; set; }
    public string? URL { get; set; }
}