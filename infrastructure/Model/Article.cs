using System.ComponentModel.DataAnnotations;

namespace infrastructure.Model;

public class Article
{
    public int ArticleId { get; set; }
    
    [MinLength(5)]
    [MaxLength(30)]
    public string Headline { get; set; }
    
    [MaxLength(1000)]
    public string Body { get; set; }
    
    [EnumDataType(typeof(AuthorsEnums))]
    public string Author { get; set; }

    public string ArticleImgUrl { get; set; }
    
}
public enum AuthorsEnums
{
    Bob,
    Rob,
    Dob, 
    Lob
}
