

using infrastructure;
using infrastructure.Model;

namespace service;

public class Service
{
    private readonly Reposotory _repository;

    public Service(Reposotory repository)
    {
        _repository = repository;
    }

    public IEnumerable<Article> GetAllArticles()
    {
        return _repository.getAllArticles();
    }

    public Article createArticle(Article article)
    {
        try
        {
            return _repository.createArticle( article.Headline, article.Body, article.Author, article.ArticleImgUrl);
        }
        catch (Exception e)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(e);
            Console.ResetColor();
            throw new Exception("Could not create the given book");
        }
    }

    public void deleteArticleById(int id)
    {
        _repository.deleteArticle(id);
    }

    public Article getFullArticle(int articleId)
    {
        return _repository.getFullArticle(articleId);
    }

    public IEnumerable<SearchArticleItem> searchForArticles(string searchterm, int pageSize)
    {
        return _repository.searchForArticles(searchterm, pageSize);
    }

    public Article updateArticleById(int articleId, Article article)
    {
        return _repository.updateArticleById(articleId, article);
    }

    public IEnumerable<Article> searchForArticlesReturnArticle(string searchterm, int pageSize)
    {
        return _repository.searchForArticlesReturnArticle(searchterm, pageSize);
    }
}