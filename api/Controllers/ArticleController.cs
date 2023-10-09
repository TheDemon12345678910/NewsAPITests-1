using infrastructure;
using infrastructure.Model;
using Microsoft.AspNetCore.Mvc;
using service;

namespace api.Controllers;

[ApiController]
public class ArticleController : ControllerBase
{
    private readonly Service _service;

    public ArticleController(Service service)
    {
        _service = service;
    }

    [HttpPost]
    [Route("/api/articles")]
    public Article createArticle([FromBody]Article article)
    {
        return _service.createArticle(article);
    }

    [HttpPut]
    [Route("/api/articles/{articleId}")]
    public Article updateArticleById([FromRoute] int articleId, [FromBody] Article article)
    {
        return _service.updateArticleById(articleId, article);
    }

    [HttpGet]
    [Route("/api/feed")]
    public IEnumerable<Article> GetArticles()
    {
        return _service.GetAllArticles();
    }

    [HttpGet]
    [Route("/api/articles/{articleId}")]
    public Article getFullArticle([FromRoute] int articleId)
    {
        return _service.getFullArticle(articleId);
    }
    
    
    [HttpDelete]
    [Route("/api/articles/{id}")]
    public void deleteArticle([FromRoute]int id)
    {
        _service.deleteArticleById(id);
    }
    
    [HttpGet]
    [Route("/api/articles/")]
    //public IEnumerable<SearchArticleItem> searchForArticles([FromQuery]string searchterm, int pageSize)
    public IEnumerable<Article> searchForArticles([FromQuery]string searchterm, int pageSize)
    {
        //Validate input
        if (searchterm.Length > 3 && pageSize >= 1)
        {
            //return _service.searchForArticles(searchterm, pageSize);
            return _service.searchForArticlesReturnArticle(searchterm, pageSize);
        }
        throw new Exception("Could not search with the given information");
    }
}
